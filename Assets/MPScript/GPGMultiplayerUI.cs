using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

/// <summary>
/// This demo scene is setup to show how to get a real-time multiplayer game started in a variety of ways (via received invitation, programmatically or via the player selector).
///
/// First, you must have an authenticated player (Authenticate button). Once you do you can pick how you want to start the room (Room Creation section). The demo scene will
/// automatically show the Google Play waiting room UI (showWaitingRoom) when a room is created or joined (note that this is an optional step for when you make your game).
/// Once all players are connected it gets dismissed and we are in a real-time game ready to start sending data.
///
/// For demonstration purposes, you can send data (via the Send Unreliable Message to All button) or leave the room (via the Leave Room button) once connected.
/// </summary>

namespace Prime31
{
	public class GPGMultiplayerUI : MonoBehaviourGUI
	{

	#if UNITY_IPHONE || UNITY_ANDROID
		public Texture2D m_texBack, m_texsign_in, m_texShow_inbox, m_texSelectFrnd, m_texRandomPlayer;
		public GUIStyle m_styleEmpty;
		private string _lastReceivedMessage = string.Empty;
		private string mMyParticipantId = "";
		private Rect RSignIn, RInvite, RInbox, RRandom;

		void Start()
		{	
			PlayGameServices.enableDebugLog( true );
	#if UNITY_IPHONE
			// we always want to call init as soon as possible after launch. Be sure to pass your own clientId to init!
			// This call is not required on Android.
			PlayGameServices.init( "160040154367.apps.googleusercontent.com", true );
	
			// on iOS we will need to register for push notifications and send the device token to Google so they can send out
			// push notifications on your behalf
			Debug.Log( "registering for push notifications" );
			NotificationServices.RegisterForRemoteNotificationTypes( RemoteNotificationType.Alert | RemoteNotificationType.Badge | RemoteNotificationType.Sound );
	#endif


		}
	
		bool B = true;

		void OnGUI()
		{	
			if(B){
				B = false;

				Rect R = Ractangle(0,0,0,0.2f);
				R.width = 1.54f * R.height;
				R.x = (Screen.width/2) - (R.width/2);
				R.y = (Screen.height/2) - (R.height/2);
				RSignIn = R;

				R = Ractangle(0,0,0,0.2f);
				R.width = 2.77f * R.height;
				R.x = (Screen.width/2) - (R.width/2);
				R.y = (Screen.height/2) - (R.height/2);
				RInbox = R;

				Rect R1 = R;
				R1.y = (Screen.height/2)- (R.height + (Screen.height * 0.1f));
				RInvite = R1;

				R1.y = (Screen.height / 2) + (Screen.height * 0.1f);
				RRandom = R1;

				StartCoroutine(AnimatedRect(RSignIn, (x) => RSignIn = x));
				StartCoroutine(AnimatedRect(RInbox, (x) => RInbox = x));
				StartCoroutine(AnimatedRect(RInvite, (x) => RInvite = x));
				StartCoroutine(AnimatedRect(RRandom, (x) => RRandom = x));
			}

			// if there is no game in progress we show buttons to setup a room
			if(!CommonS._isGameInProgress && !PlayGameServices.isSignedIn())
			{
				if( GUI.Button(RSignIn, m_texsign_in, m_styleEmpty)){
					PlayGameServices.authenticate();
				}
	
	
	#if UNITY_IPHONE
				if( GUILayout.Button( "Register Device Token" ) )
				{
					if( NotificationServices.deviceToken != null )
						GPGMultiplayer.registerDeviceToken( NotificationServices.deviceToken, false );
					else
						Debug.LogWarning( "NotificationServices.deviceToken is null so we are not registering with Google" );
				}
	#endif
			}
			else if(!CommonS._isGameInProgress && PlayGameServices.isSignedIn()){
				if( GUI.Button(RInbox, m_texShow_inbox, m_styleEmpty)){
						GPGMultiplayer.showInvitationInbox();
				}
				if( GUI.Button(RInvite, m_texSelectFrnd, m_styleEmpty)){
						GPGMultiplayer.showPlayerSelector( 1, 3 );
				}
				if( GUI.Button(RRandom, m_texRandomPlayer, m_styleEmpty)){
						GPGMultiplayer.startQuickMatch( 1, 3, 0, 1 );
				}
			}
			else if(CommonS._isGameInProgress){
				GUILayout.Space( 40 );
				GUILayout.Label( _lastReceivedMessage );
			}
		}

		Rect Ractangle(float x, float y, float w, float h){
			return new Rect (Screen.width * x, Screen.height * y, Screen.width * w, Screen.height * h);
		}

		IEnumerator AnimatedRect(Rect R, System.Action<Rect> Action){
			Rect Rtemp = R;
			float MulFactor = 0.001f;
		Repeat:
			Action (ConvertRect(Rtemp, MulFactor));
			yield return new WaitForSeconds (.01f);

			MulFactor += 0.05f;
			if(MulFactor < 1.2f)	goto Repeat;

		Repeat1:
			Action (ConvertRect(Rtemp, MulFactor));
			yield return new WaitForSeconds (.015f);
						
			MulFactor -= 0.05f;
			if(MulFactor >= 1f)	goto Repeat1;

			MulFactor = 1f;
			Action (ConvertRect(Rtemp, MulFactor));
		}

		Rect ConvertRect(Rect R, float Factor){
			float X = R.x;
			float Y = R.y;
			float W = R.width * Factor;
			float H = R.height * Factor;
			X = (Screen.width / 2) - (W / 2);
			
			return new Rect (X, Y, W, H);
		}
	#endif
	}


}

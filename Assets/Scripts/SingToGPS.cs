using UnityEngine;
using System.Collections;
using Prime31;

public class SingToGPS : MonoBehaviour {

	#if UNITY_IPHONE || UNITY_ANDROID
	public Texture2D m_texPopUp, m_texClose;
	public GUIStyle m_styleEmpty;
	private string _lastReceivedMessage = string.Empty;

	void Awake(){
		print ("SingToGPS");
		DontDestroyOnLoad (this);
	}

	void Start () {
		if (Application.loadedLevel == 0) {
			Application.LoadLevel ("MainMenu");
		}
		CommonS._isGameInProgress = false;
		CommonS.st_isPopUp = false;
		PlayGameServices.enableDebugLog( true );
		DontDestroyOnLoad (this);
		#if UNITY_IPHONE
			// we always want to call init as soon as possible after launch. Be sure to pass your own clientId to init!
			// This call is not required on Android.
			PlayGameServices.init( "160040154367.apps.googleusercontent.com", true );
			
			// on iOS we will need to register for push notifications and send the device token to Google so they can send out
			// push notifications on your behalf
			Debug.Log( "registering for push notifications" );
			NotificationServices.RegisterForRemoteNotificationTypes( RemoteNotificationType.Alert | RemoteNotificationType.Badge | RemoteNotificationType.Sound );
		#endif
		if (!CommonS._isGameInProgress && !PlayGameServices.isSignedIn ()) {	
			PlayGameServices.authenticate ();
		}
	}

	void OnGUI(){
		if (CommonS.st_isPopUp) {
			Rect R = Ractangle(0,0,0,0.2f);
			R.width = 2.22f * R.height; 
			R.x = (Screen.width) - (R.width);
			R.y = (Screen.height) - (R.height);
			
			GUI.DrawTexture (R, m_texPopUp);
			
			Rect R1 = Ractangle(0,0, 0, 0.1f);
			R1.width = R1.height;
			R1.x = R.x ;
			R1.y = R.y ;
			if(GUI.Button(R1, m_texClose, m_styleEmpty)){
				CommonS.st_isPopUp = false;
			}	
			R = Ractangle(0,0,0,0.2f);
			R.width = 1.6f * R.height; 
			R.x = (Screen.width) - (R.width);
			R.y = (Screen.height) - (R.height);

			if(GUI.Button(R,"", m_styleEmpty)){
				if(PlayerPrefs.GetInt("BuyMP")>0){
					if(Application.loadedLevelName != "SignIn"){
						Application.LoadLevel("SignIn");
					}
				}
				else{
					CommonS.st_isMultyPlayer = true;
				}
				CommonS.st_isPopUp = false;
			}
		}
	}

	Rect Ractangle(float x, float y, float w, float h){
		return new Rect (Screen.width * x, Screen.height * y, Screen.width * w, Screen.height * h);
	}

	#region Event Listeners
	
	void OnEnable(){
		GPGMultiplayerManager.onRoomConnectedEvent += onRoomConnectedEvent;
		GPGMultiplayerManager.onRealTimeMessageReceivedEvent += onRealTimeMessageReceivedEvent;
		GPGMultiplayerManager.onInvitationReceivedEvent += onInvitationReceivedEvent;
	}
	
	void OnDisable(){
		GPGMultiplayerManager.onRoomConnectedEvent -= onRoomConnectedEvent;
		GPGMultiplayerManager.onRealTimeMessageReceivedEvent -= onRealTimeMessageReceivedEvent;
		GPGMultiplayerManager.onInvitationReceivedEvent -= onInvitationReceivedEvent;
	}
	
	string sPlayerPoint;
	private void onRoomConnectedEvent(GPGRoom room, GPGRoomUpdateStatus status ){
		CommonS._isGameInProgress = true;
		var bytes = System.Text.Encoding.UTF8.GetBytes (PlayGameServices.getLocalPlayerInfo ().name);
		Debug.Log ("Player name ::::"+PlayGameServices.getLocalPlayerInfo ().name);
		Debug.Log ("Player ID ::::" + CommonS.st_RoomCreatorID);
		Debug.Log ("Player ID ::::" + GPGMultiplayer.getCurrentPlayerParticipantId ());
		sPlayerPoint = PlayGameServices.getLocalPlayerInfo ().name + " " + PlayerPrefs.GetInt("Points",200);
		CommonS.st_listPlayerPoints.Add(sPlayerPoint);
		SenddMessage("Points"+sPlayerPoint);

//		if (PlayerPrefs.GetString ("PlayerSelect") == "Girl") {
//			CommonS.st_SelectList.Add(GPGMultiplayer.getCurrentPlayerParticipantId () + "Grl");
//			SenddMessage("Player"+GPGMultiplayer.getCurrentPlayerParticipantId () + "Grl");
//		}
//		else{
//			CommonS.st_SelectList.Add(GPGMultiplayer.getCurrentPlayerParticipantId () + "Boy");
//			SenddMessage("Player"+GPGMultiplayer.getCurrentPlayerParticipantId () + "Boy");	
//		}

		CommonS.st_SelectList.Add(GPGMultiplayer.getCurrentPlayerParticipantId () + PlayerPrefs.GetString("PlayerSelect","Boy"));
		SenddMessage("Player"+ GPGMultiplayer.getCurrentPlayerParticipantId ()+PlayerPrefs.GetString("PlayerSelect","Boy"));	

		GPGMultiplayer.sendUnreliableRealtimeMessageToAll( bytes );
		if (GPGMultiplayer.getCurrentPlayerParticipantId () == CommonS.st_RoomCreatorID) {
			if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
				Debug.Log("Msg Send");
				SenddMessage("Electricity");
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
				Debug.Log("Msg Send");
				SenddMessage("Ice");
			}
			else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
				Debug.Log("Msg Send");
				SenddMessage("Fire");
			}
			StartCoroutine ("waits", 2f);
		}
		else{
			StartCoroutine ("waits", .5f);
		}
		Application.LoadLevel("MultyPlayer");
	}
	
	IEnumerator waits(float second){
		yield return new WaitForSeconds (second);
	}
	
	private void onWaitingRoomCompletedEvent( bool didSucceed )
	{
		CommonS._isGameInProgress = didSucceed;
	}

	private void onInvitationReceivedEvent(string invitationId){
		Debug.Log("Invitation.........111111111111111111111");
		CommonS.st_isPopUp = true;
	}

	void onInvitationInboxCompletedEvent( bool didSucceed )
	{
		Debug.Log( "onInvitationInboxCompletedEvent. didSucceed: " + didSucceed );
	}
	
	public void SenddMessage(string Message)
	{
		var bytes = System.Text.Encoding.UTF8.GetBytes (Message);
		GPGMultiplayer.sendUnreliableRealtimeMessageToAll( bytes );
	}
	
	private void onRealTimeMessageReceivedEvent( string senderParticipantId, byte[] message)
	{
		var messageString = System.Text.Encoding.UTF8.GetString( message );
		_lastReceivedMessage = string.Format( "Last Message: " + messageString );
		
		if(messageString.StartsWith("Points")){
			string s = messageString.Substring(6);
//			Debug.Log("Points.. = " + s);
			CommonS.st_listPlayerPoints.Add(s);	
		}
		if(messageString.StartsWith("Player")){
			string s = messageString.Substring(6);
			Debug.Log("Player.. = " + s);
			CommonS.st_SelectList.Add(s);	
		}
		if(messageString == "Electricity"){
			Debug.Log("Electricity");
			CommonS.st_enmCrntTheme = CommonS.GameTheme.Electricity;
		}
		if(messageString == "Ice"){
			Debug.Log("Ice");
			CommonS.st_enmCrntTheme = CommonS.GameTheme.Ice;
		}
		if(messageString == "Fire"){
			Debug.Log("Fire");
			CommonS.st_enmCrntTheme = CommonS.GameTheme.Fire;
		}
	}
	
	#endregion
	#endif
}

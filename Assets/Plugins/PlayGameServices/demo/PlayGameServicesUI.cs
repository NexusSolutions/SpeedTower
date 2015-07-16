using UnityEngine;
using System.Collections.Generic;



namespace Prime31
{
	public class PlayGameServicesUI : Prime31.MonoBehaviourGUI
	{
#if UNITY_IPHONE || UNITY_ANDROID
		void Start()
		{
			PlayGameServices.enableDebugLog( true );
	
			// we always want to call init as soon as possible after launch. Be sure to pass your own clientId to init on iOS!
			// This call is not required on Android.
			PlayGameServices.init( "160040154367.apps.googleusercontent.com", true );
		}
	
	
		/// <summary>
		/// due to the large number of buttons in this demo all sections of buttons are in their own method below for easier readability
		/// </summary>
		void OnGUI()
		{
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			beginColumn();
	
			// toggle to show two different sets of buttons
			if( toggleButtonState( "Achievements/Events/Quests/Snapshots" ) )
				showAuthAndSetttingsButtons();
			else
				achievementsEventsAndQuestsButtons();
			toggleButton( "Achievements/Events/Quests/Snapshots", "Toggle Buttons" );
	
	
			endColumn( true );
	
			// toggle to show two different sets of buttons
			if( toggleButtonState( "Show Cloud Save and Sharing Buttons" ) )
				leaderboardsAndAchievementsButtons();
			else
				cloudSaveButtons();
			toggleButton( "Show Cloud Save and Sharing Buttons", "Toggle Buttons" );
	
			endColumn( false );
	
	
			if( bottomRightButton( "Real Time MP Scene" ) )
				Application.LoadLevel( "PlayGameServicesMultiplayerDemoScene" );
		}
	
	
		#region Buttons: each method here corresponds to a column of buttons
	
		private void showAuthAndSetttingsButtons()
		{
			GUILayout.Label( "Authentication and Settings" );
	
			if( GUILayout.Button( "Set Toasts on Bottom" ) )
			{
				PlayGameServices.setAchievementToastSettings( GPGToastPlacement.Bottom, 50 );
			}
	
	
			#if UNITY_ANDROID
			if( GUILayout.Button( "Authenticate Silently (with no UI)" ) )
			{
				PlayGameServices.attemptSilentAuthentication();
			}
	
	
			if( GUILayout.Button( "Get Auth Token" ) )
			{
				var token = PlayGameServices.getAuthToken( null );
				Debug.Log( "token: " + token );
			}
			#endif
	
	
			if( GUILayout.Button( "Authenticate" ) )
			{
				PlayGameServices.authenticate();
			}
	
	
			if( GUILayout.Button( "Sign Out" ) )
			{
				PlayGameServices.signOut();
			}
	
	
			if( GUILayout.Button( "Is Signed In" ) )
			{
				// Please note that the isSignedIn method is a total hack that was added to work around a current bug where Google
				// does not properly notify an app that the user signed out
				Debug.Log( "is signed in? " + PlayGameServices.isSignedIn() );
			}
	
	
			if( GUILayout.Button( "Get Player Info" ) )
			{
				var playerInfo = PlayGameServices.getLocalPlayerInfo();
				Prime31.Utils.logObject( playerInfo );
	
				// if we are on Android and have an avatar image available, lets download the profile pic
				if( Application.platform == RuntimePlatform.Android && playerInfo.avatarUri != null )
					PlayGameServices.loadProfileImageForUri( playerInfo.avatarUri );
			}
	
	
			if( GUILayout.Button( "Load Remote Player Info" ) )
			{
				PlayGameServices.loadPlayer( "110453866202127902712" );
			}
		}
	
	
		private void achievementsEventsAndQuestsButtons()
		{
			GUILayout.Label( "Achievements" );
	
			if( GUILayout.Button( "Show Achievements" ) )
			{
				PlayGameServices.showAchievements();
			}
	
	
			if( GUILayout.Button( "Increment Achievement" ) )
			{
				PlayGameServices.incrementAchievement( "CgkI_-mLmdQEEAIQAQ", 2 );
			}
	
	
			if( GUILayout.Button( "Unlock Achievement" ) )
			{
				PlayGameServices.unlockAchievement( "CgkI_-mLmdQEEAIQAw" );
			}
	
	
			GUILayout.Label( "Events and Quests" );
	
			if( GUILayout.Button( "Load All Events" ) )
			{
				PlayGameServices.loadAllEvents();
			}
	
	
			if( GUILayout.Button( "Increment Event" ) )
			{
				// you will need to use your own eventId for this to work
				PlayGameServices.incrementEvent( "CgkI_-mLmdQEEAIQCg", 1 );
			}
	
	
			if( GUILayout.Button( "Show Quest List" ) )
			{
				PlayGameServices.showQuestList();
			}
	
	
			if( GUILayout.Button( "Load All Quests" ) )
			{
				PlayGameServices.loadAllQuests();
				PlayGameServices.showStateChangedPopup( "CgkI_-mLmdQEEAIQDw" );
			}
	
	
			GUILayout.Label( "Snapshots" );
	
			if( GUILayout.Button( "Show Snapshot List" ) )
			{
				PlayGameServices.showSnapshotList( 5, "Your Saved Games!", true, true );
			}
	
	
			if( GUILayout.Button( "Save Snapshot" ) )
			{
				var data = System.Text.Encoding.UTF8.GetBytes( "my saved data" );
				PlayGameServices.saveSnapshot( "snappy", true, data, "The description of the data" );
			}
	
	
			if( GUILayout.Button( "Load Snapshot" ) )
			{
				PlayGameServices.loadSnapshot( "snappy" );
			}
	
	
			if( GUILayout.Button( "Delete Snapshot" ) )
			{
				PlayGameServices.deleteSnapshot( "snappy" );
			}
		}
	
	
		private void leaderboardsAndAchievementsButtons()
		{
			GUILayout.Label( "Leaderboards" );
	
			if( GUILayout.Button( "Show Leaderboard" ) )
			{
				PlayGameServices.showLeaderboard( "CgkI_-mLmdQEEAIQBQ" );
			}
	
	
			if( GUILayout.Button( "Show All Leaderboards" ) )
			{
				PlayGameServices.showLeaderboards();
			}
	
	
			if( GUILayout.Button( "Submit Score" ) )
			{
				PlayGameServices.submitScore( "CgkI_-mLmdQEEAIQBQ", 566 );
			}
	
	
			if( GUILayout.Button( "Load Raw Score Data" ) )
			{
				PlayGameServices.loadScoresForLeaderboard( "CgkI_-mLmdQEEAIQBQ", GPGLeaderboardTimeScope.AllTime, false, false );
			}
	
	
			if( GUILayout.Button( "Get Leaderboard Metadata" ) )
			{
				var info = PlayGameServices.getAllLeaderboardMetadata();
				Prime31.Utils.logObject( info );
			}
	
	
			if( GUILayout.Button( "Get Achievement Metadata" ) )
			{
				var info = PlayGameServices.getAllAchievementMetadata();
				Prime31.Utils.logObject( info );
			}
	
	
			if( GUILayout.Button( "Reload All Metadata" ) )
			{
				PlayGameServices.reloadAchievementAndLeaderboardData();
			}
		}
	
	
		private void cloudSaveButtons()
		{
			GUILayout.Label( "Cloud Data" );
	
	
			if( GUILayout.Button( "Load Cloud Data" ) )
			{
				PlayGameServices.loadCloudDataForKey( 0 );
			}
	
	
			if( GUILayout.Button( "Set Cloud Data" ) )
			{
				PlayGameServices.setStateData( "I'm some data. I could be JSON or XML.", 0 );
			}
	
	
			if( GUILayout.Button( "Update Cloud Data" ) )
			{
				PlayGameServices.updateCloudDataForKey( 0 );
			}
	
	
			if( GUILayout.Button( "Get Cloud Data" ) )
			{
				var data = PlayGameServices.stateDataForKey( 0 );
				Debug.Log( data );
			}
	
	
			if( GUILayout.Button( "Delete Cloud Data" ) )
			{
				PlayGameServices.deleteCloudDataForKey( 0 );
			}
	
	
			if( GUILayout.Button( "Clear Cloud Data" ) )
			{
				PlayGameServices.clearCloudDataForKey( 0 );
			}
	
	
			if( GUILayout.Button( "Show Share Dialog" ) )
			{
				PlayGameServices.showShareDialog( "I LOVE this game!", "http://prime31.com" );
			}
		}
	
		#endregion
	
#endif
	}

}

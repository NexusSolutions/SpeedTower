using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class TowerRunManager : MonoBehaviour {

//	string[] PlayerName= new string[] {"PlayerContainer", "PlayerContainer2", "PlayerContainer3", "PlayerContainer4" };//to get controller Script and hide object
	string[] PlayerName= new string[] {"JumpObject", "JumpObject2", "JumpObject3", "JumpObject4" };//to get controller Script and hide object

	public static int st_totalPlayer = 0;
	List<string> PlayerType = new List<string>();
	void Start () {

		if(CommonS._isGameInProgress){
			Debug.Log("_isGameInProgress = " + CommonS._isGameInProgress);
			StartCoroutine("wait",1);
		}
	}

	IEnumerator wait(float t)
	{
		yield return new WaitForSeconds (t);
		SetupPlayers ();
	}

	private void SetupPlayers() {
		List<GPGMultiplayerParticipant> AllPlayers = GPGMultiplayer.getParticipants (false);
		st_totalPlayer = AllPlayers.Count;
		Debug.Log ("Total Plyars ::::" + st_totalPlayer);
		foreach (GPGMultiplayerParticipant Player in AllPlayers) {
				Debug.Log (Player.participantId + "\t" + Player.displayName + "\t" + Player.status + "\t" + Player.iconImageUrl);
		}	

		for (int i=0; i<PlayerName.Length; i++) {
			GameObject PlayerContainer = GameObject.Find (PlayerName [i]);	
			Debug.Log ("Containername::::" + PlayerContainer.name);
			GPGMultiplayerParticipant Player = i < AllPlayers.Count ? AllPlayers [i] : null; 
			if (Player != null) {
				Debug.Log ("player is not null!");
				BehaviorUtils.MakeVisible (PlayerContainer, true);
				Debug.Log ("Player participentID::::" + Player.participantId);
				for(int j = 0; j < CommonS.st_SelectList.Count; j++){
					if(CommonS.st_SelectList[j].StartsWith(Player.participantId)){
						Debug.Log(CommonS.st_SelectList[j] + " ====== " + Player.participantId);
						if(CommonS.st_SelectList[j].EndsWith("Girl")){
							Debug.Log("GIRL" + PlayerContainer.name);
							PlayerContainer.transform.GetChild(0).gameObject.SetActive(false);
						}
						else{
							Debug.Log("BOY" + PlayerContainer.name);
							PlayerContainer.transform.GetChild(1).gameObject.SetActive(false);
						}
					}
				}
				SetPlayer Controller = PlayerContainer.GetComponentInChildren<SetPlayer> ();
				Controller.SetParticipantId (Player.participantId);
				Controller.SetParticipantName (Player.displayName);
				Debug.Log ("SetParticipantName::::" + Controller.ParticipantName);
			}
			else{
				Debug.Log("DESTROY" + PlayerContainer.name);
				Destroy(PlayerContainer);
			}
		}
	}





	public GPGPlayerInfo GetSelf()
	{
		return PlayGameServices.getLocalPlayerInfo ();
	}
}
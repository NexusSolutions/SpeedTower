using UnityEngine;
using System.Collections;
using Prime31;

public class SetPlayer : MonoBehaviour {

	private string mParticipantId = null;
	private string mParticipantName = null;
	
	public void SetParticipantId(string id) {
		mParticipantId = id;
		print (mParticipantId);
	}

	public void SetParticipantName(string pName) {
		mParticipantName = pName;
		print (mParticipantName);
	}
	
	public string ParticipantId {
		get {
			return mParticipantId;
		}
	}

	public string ParticipantName {
		get {
			return mParticipantName;
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

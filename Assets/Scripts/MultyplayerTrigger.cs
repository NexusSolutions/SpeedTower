using UnityEngine;
using System.Collections;
using Prime31;

public class MultyplayerTrigger : MonoBehaviour {
	
	AudioSource m_AudioSource;
	AudioClip m_clipBeam,  m_clipMetalBar;
	public static bool IsFreezed = false;
	bool IsCollider = false;
	MultyPlayerController P1 = new MultyPlayerController();
	bool isStart1 = false;

	public bool IsSelf(){	
		return GPGMultiplayer.getCurrentPlayerParticipantId () == GetComponent<SetPlayer>().ParticipantId ? true : false;	
	}

	void Start(){
		m_AudioSource = GameObject.FindWithTag ("ClickSound").GetComponent<AudioSource> () as AudioSource;
		if (tag == "Player"){
			m_clipMetalBar = Resources.Load ("Clip/MatelBar")as AudioClip;
		}
		else if(tag=="Beam"){
			m_clipBeam = Resources.Load ("Clip/Beam")as AudioClip;	
		}
	}	
	
	void OnCollisionEnter(Collision collision) {
		if (gameObject.tag == "Player") {
			if (GPGMultiplayer.getCurrentPlayerParticipantId () == GetComponent<SetPlayer>().ParticipantId) {
				if ((collision.gameObject.tag == "MetalBar" || collision.gameObject.tag == "Beam") && !IsCollider) {
					IsCollider = true;
					m_AudioSource.clip = m_clipMetalBar;
					m_AudioSource.Play ();
					Debug.Log ("TiggerMetalBar");
					P1.SenddMessage ("GoBack");
					StartCoroutine (CallGoBack ());
				}
			}
		}
	}

	IEnumerator CallGoBack(){
//		Handheld.Vibrate ();
//		StartCoroutine (DoBlinks());
//		for (int i = 0; i < NumofTimes; i++) {
		if (PlayerPrefs.GetString ("PlayerSelect") == "Girl") {
			transform.parent.SendMessage("ChkIsJumping");
		}
		else{
			transform.parent.parent.SendMessage("ChkIsJumping");
		}
//		}
		yield return new WaitForSeconds (0.5f);
		IsCollider = false;

//		transform.parent.GetChild(0).renderer.material.SetColor("_Color",Color.white);
	}
	
//	IEnumerator DoBlinks() {
//		float duration = 0.2f;
//		while (duration > 0f) {
//			duration -= Time.deltaTime;
//			//toggle renderer
//			transform.parent.GetChild (0).renderer.enabled = !transform.parent.GetChild (0).renderer.enabled;
//			yield return new WaitForSeconds(0.05f);
//		}
//		transform.parent.GetChild (0).renderer.enabled = true;
//	}

//	void OnParticleCollision(GameObject other) {
//		if(other.gameObject.tag == "Player" && !IsCollider) {
//			if(GPGMultiplayer.getCurrentPlayerParticipantId () == other.gameObject.GetComponent<SetPlayer> ().ParticipantId) {
//				IsCollider = true;
//				m_AudioSource.clip = m_clipBeam;
//				m_AudioSource.Play ();
//				Debug.Log ("TiggerMetalBeam");
//				P1.SenddMessage ("GoBack1");
//				StartCoroutine (CallGoBack ());
//			}
//		}
//	}

}
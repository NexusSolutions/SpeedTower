using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class MultyplayerInit : MonoBehaviour {

	public static bool st_isPlay;
	MultyPlayerController P1 = new MultyPlayerController();

	public GameObject m_PrfbMtlBar ,m_objWonBtn, m_objLifeAddMenu;
	public TextMesh m_meshCountDown;
	public GUIStyle m_StyleEmpty;
	
	GameObject m_ObjBeam;
	List<GameObject> l_listObj = new List<GameObject>();
	Vector3 PosMetalbar; 
	float Speed1, l_fScecond,l_fTimer, l_RndmRange4time;
	bool st_IsMusicOn, IsLifeAdded;
	float[] l_arrObjFall =new float[]{1.3f, .4f, 1f};
	bool IsStart;

	public bool IsSelf(){
		if (GPGMultiplayer.getCurrentPlayerParticipantId ()!= null && GetPlayerId() != null) {
			return (GPGMultiplayer.getCurrentPlayerParticipantId () == GetComponentInChildren<SetPlayer>().ParticipantId)? true : false;
		}
		else
			return false;
	}
	
	public string GetPlayerId(){
		print (GetComponentInChildren<SetPlayer> ().ParticipantId);
		return GetComponentInChildren<SetPlayer>().ParticipantId;
	}

	List<string> second2 = new List<string>();
	List<string> random1 = new List<string>();
	List<string> random2 = new List<string>();
	
	float timer2 = 0;
	int Inc3 = 0, Inc4 = 0;
	
	float F1;
	string S1;
	string BarString(){
		float[] l_arrObjFall2 =new float[]{.8f, 1.3f, 2f};
		
	Repeat:
			F1 += l_arrObjFall2 [Random.Range (0, 3)];
		S1 += F1 + " ; ";
		if(F1 < 60){
			goto Repeat;
		}
		return S1;
	}
	
	private string ss1 = null;
	private string ss2 = null;
	private string RandomValue1 = " 0 ; 1 ; 0 ; 1 ; 0 ; 1 ; 0 ; 1 ; 0 ; 0 ; 1 ; 0 ; 1 ; 0 ; 0 ;";
	private string RandomValue2 = " 1 ; 0 ; 1 ; 0 ; 0 ; 0 ; 1 ; 1 ; 0 ; 1 ; 0 ; 0 ; 1 ; 0 ; 1 ;";

	void SendMessageToOther(){
		Debug.Log ("SendMessageToOther");
		string s2 = BarString ();
		SetString2 (s2);
		Debug.Log ("before Send passbar = " +s2);
		P1.SenddMessage ("Passbar"+s2);
		
	}
	public void SetString2(string id) {
		ss2 = id;
		Debug.Log ("ss2" + ss2);
		SpliteString2 (ss2);
	}
	
	void SpliteString2(string k1){
		Debug.Log("SpliteString2");
		string[] words = k1.Split (';');
		foreach (string word in words){
			second2.Add(word);
		}
		Camera.main.SendMessage ("Lstart");
		IsStart = true;	
	}
	
	void SpliteString(){
		string[] words = RandomValue1.Split (';');
		foreach (string word in words){
			random1.Add(word);
		}

		words = RandomValue2.Split (';');
		foreach (string word in words){
			random2.Add(word);
		}
	}

	void Start() {
		IsStart = false;
		ResetGame ();
		l_RndmRange4time = 0f;
		Speed1 = 2f;
	}

	void Update(){
		if(!st_isPlay)  return;
		if(!IsStart)    return;
			timer2 += Time.deltaTime;
			if (timer2 > double.Parse (second2 [Inc4])) {
			DestroyObjects();
			if (LevelSetting.st_iCurrentLevel <= 4){
				if(random1 [Inc3].Contains ("1")){
					IntBar(random2 [Inc3]);
				}
			} 
			else if (LevelSetting.st_iCurrentLevel <= 8) {
				if (random1 [Inc3].Contains ("0")){
					IntBar(random2 [Inc3]);
				}
				else{
					IntBeam();
				}
			}
			else if (LevelSetting.st_iCurrentLevel <= 12) {
				IntBeam();
			}
			Inc3++;
			Inc4++;
			if (Inc4 >= second2.Count -1) {
				Inc4 = 0;
				timer2 = 0;
			}
			if (Inc3 >= random1.Count - 1) {
				Inc3 = 0;
			}
		}
	}

	void DestroyObjects(){
		for (int i = 0; i < l_listObj.Count; i++) {
			if ((l_listObj [i].transform.position.y ) < 0) {
				Destroy (l_listObj [i]);
				l_listObj.RemoveAt (i);
			}
		}
	}

	void IntBar(string s){
		GameObject ObjMetalbar = (GameObject)Instantiate (m_PrfbMtlBar);
		l_listObj.Add (ObjMetalbar);
		if(s.Contains("0")){
			ObjMetalbar.transform.localPosition = CommonS.PosMtlBarDwn;
		}
		else{
			ObjMetalbar.transform.localPosition = CommonS.PosMtlBarUp;
		}
		ObjMetalbar.transform.localRotation = Quaternion.Euler (0f, 167.5f, 0f);
		
		ObjMetalbar.transform.parent =  GameObject.Find("MainPath").transform;
	}
	
	void IntBeam(){
		GameObject ObjBeam = (GameObject)Instantiate (m_ObjBeam);
		l_listObj.Add (ObjBeam);
		ObjBeam.transform.parent =  GameObject.Find("MainPath").transform;
	}

	void ResetGame(){
		Time.timeScale = 1f;
		
		if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			PlayerPrefs.SetInt("SpiralTex", 2);
			m_ObjBeam = (GameObject) Resources.Load("Prefab/" + "BeamElectric") as GameObject;
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			PlayerPrefs.SetInt("SpiralTex", 3);
			m_ObjBeam = (GameObject) Resources.Load("Prefab/" + "BeamIce") as GameObject;
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			PlayerPrefs.SetInt("SpiralTex", 4);
			m_ObjBeam = (GameObject) Resources.Load("Prefab/" + "BeamFire") as GameObject;
		}

		RotateImageFinal.st_fRotateMainspeed = 0.3f;
		Speed1 = 2f;
		m_objWonBtn.SetActive (false);
		
		CommonS.st_IsCollider = false;
		IsLifeAdded = false;
		
		LevelSetting.st_iCurrentLevel = 1;

		StartCoroutine (CountDown());
	}

	IEnumerator CountDown(){
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = false;
		st_isPlay = false;
		m_meshCountDown.text = 3 + "";
		m_meshCountDown.animation ["InCountDown"].speed = 1f;
		m_meshCountDown.animation.Play ("InCountDown");
		yield return new WaitForSeconds (0.5f);
		
		for (int i = 3; i >= 1; i--) {
			m_meshCountDown.animation ["CountDown"].speed = 1f;
			m_meshCountDown.animation.Play ("CountDown");
			
			m_meshCountDown.text = i + "";
			yield return new WaitForSeconds (1f);	
		}
		
		m_meshCountDown.text = "Go";
		m_meshCountDown.animation ["OutCountDown"].speed = 1f;
		m_meshCountDown.animation.Play ("OutCountDown");

		SpliteString ();
//		SendMessageToOther ();
		st_isPlay = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = true;
		
		print ("Com = " + GetComponentInChildren<SetPlayer> ().ParticipantId + " = " + CommonS.st_RoomCreatorID + " = " + GPGMultiplayer.getCurrentPlayerParticipantId ());
		if (IsSelf ()) {
			if (GetComponentInChildren<SetPlayer>().ParticipantId == CommonS.st_RoomCreatorID) {
				SendMessageToOther();
				Debug.Log ("@@@@@...StartGame");
			}
		}
//		if (GPGMultiplayer.getCurrentPlayerParticipantId () == CommonS.st_RoomCreatorID){
//			if(IsSelf())
//			Debug.Log("@@@@" + A);
//			SendMessageToOther();

}
	//	void ExtraLife(){
	//		if(MultyPlayerController.st_LifeCnter < 6){
	//			ShowLifeAddMenu();
	//			MultyPlayerController.st_LifeCnter+=2;
	//			if(MultyPlayerController.st_LifeCnter>6)
	//				MultyPlayerController.st_LifeCnter=6;
	//			IsLifeAdded=true;
	//		}
	//	}
	//	
	//	public void ShowLifeAddMenu(){
	//		m_objLifeAddMenu.SetActive(true);
	//		MultyplayerInit.st_isPlay = false;
	//		m_objLifeAddMenu.GetComponent<Animation> ().Play ();
	//		StartCoroutine("Wait", m_objLifeAddMenu.animation ["ZoomOut"].length);
	//	}
	//	
	//	public void ShowWonMenu(){
	//		m_objWonBtn.SetActive(true);
	//		MultyplayerInit.st_isPlay = false;
	//		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = false;
	//		m_objWonBtn.GetComponent<Animation> ().Play ();
	//		StartCoroutine("Wait", m_objWonBtn.animation ["ZoomOut"].length);
	//	}
	//	
	//	void ResumeLifeAdd(){
	//		Time.timeScale = 1f;
	//		MultyplayerInit.st_isPlay = true;
	//		m_objLifeAddMenu.SetActive (false);
	//	}
	//	
	//	IEnumerator Wait(float t){
	//		yield return new WaitForSeconds (t);
	//		Time.timeScale = 0f;
	//	}
	//	
	//
	//	void ResetLevel(){
	//		Time.timeScale = 1;
	//
	//		st_isPlay=true;
	//		IsLifeAdded = false;
	//		
	//		RotateImageFinal.st_fRotateMainspeed = 0.3f;
	//		LevelSetting.st_iCurrentLevel =1;
	//		Speed1 = 2f;
	//	}
}

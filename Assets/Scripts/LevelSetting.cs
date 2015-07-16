using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSetting : MonoBehaviour {
	public static int st_iCurrentLevel;
	public GameObject m_objWonBtn;
	public GUIStyle m_StyleEmpty, m_StyleColor;
//	public TextMesh m_meshWinPoint;
	Texture2D l_texLevel_and_time,l_texLife, l_texLife_bg, l_texPause, l_texMusicOn, l_texMusicOff;
	string Minute, Second;
	float Gametime = 0f, lastUpdate = 0;
	Texture2D l_texRightArrow, l_texLeftArrow, l_texDownArrow, l_textUpArrow;
	
//	float LevelInterval;
	int i1;
	AudioClip ClickClip, BGclip;

	public bool IsGameStart = false;
	bool st_IsMusicOn, isStart0 = false;
	bool isApplause = false; 
	
	private float[] l_arrLevelTime = {10, 20,30,40,50,60,67,74,81 , 88, 95, 102};
	Rect RUp, RDown, RLeft, RRight;
	
	void Start () {
		ClickClip = Resources.Load ("Clip/ClickSound") as AudioClip;

		if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			BGclip = Resources.Load ("Clip/ElectricThem1") as AudioClip;
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			BGclip = Resources.Load ("Clip/IceTheme1") as AudioClip;
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			BGclip = Resources.Load ("Clip/FireTheme1") as AudioClip;
		}
		
		gameObject.audio.clip = BGclip;
		gameObject.audio.Play ();

		l_texLevel_and_time = (Texture2D)Resources.Load ("Texture/level-and-time") as Texture2D;
		l_texLife = (Texture2D)Resources.Load ("Texture/Life") as Texture2D;
		l_texLife_bg =(Texture2D) Resources.Load ("Texture/life-bg") as Texture2D;
		l_texPause = (Texture2D)Resources.Load ("Texture/pause") as Texture2D;
		l_texMusicOn = (Texture2D)Resources.Load ("Texture/music-on") as Texture2D;
		l_texMusicOff = (Texture2D)Resources.Load ("Texture/music-off") as Texture2D;
		l_texRightArrow = (Texture2D)Resources.Load ("Control/Control_Right") as Texture2D;
		l_texLeftArrow = (Texture2D)Resources.Load ("Control/Control_Left") as Texture2D;
		l_textUpArrow = (Texture2D)Resources.Load ("Control/Control_Up") as Texture2D;
		l_texDownArrow = (Texture2D)Resources.Load ("Control/Control_Down") as Texture2D;

		st_iCurrentLevel = 1;
		i1 = 0;

		st_IsMusicOn = (PlayerPrefs.GetInt ("IsMusicOn", 1) == 1) ? true : false;
		AudioListener.volume = (st_IsMusicOn) ? 1 : 0;

//		CommonS.st_listPlayerPoints.Add ("gfdggfgfgggggggggggg");
//		CommonS.st_listPlayerPoints.Add ("gfdggfgfgggggggggggg");
//
//		CommonS.st_listPlayerPoints.Add ("gfdggfgfgggggggggggg");
//		CommonS.st_listPlayerPoints.Add ("gfdggfgfgggggggggggg");
		
		for (int i = 0; i < CommonS.st_listPlayerPoints.Count; i++) {
			Debug.Log( "POINT ::" +CommonS.st_listPlayerPoints[i]);
		}
//		RDown = Ractangle (.01f, .83f, .14f, .19f);
//		RUp = Ractangle (.01f, .64f, .14f, .19f);
//		RLeft = Ractangle (.76f, .78f, .14f, .19f);
//		//		RRight = rectangle (.88f, .001f, .12f, .17f);
//		
//		RLeft.width = RUp.height;
//		RLeft.height = RUp.width;
//		RRight = Ractangle (.88f, .78f, .14f, .19f);
//		RRight.width = RUp.height;
//		RRight.height = RUp.width;

		RDown = Ractangle (.01f, .78f, .17f, .22f);
		RUp = Ractangle (.01f, .55f, .17f, .22f);
		RLeft = Ractangle (.74f, .73f, .17f, .22f);
		//		RRight = rectangle (.88f, .001f, .12f, .17f);
		
		RLeft.width = RUp.height;
		RLeft.height = RUp.width;
		RRight = Ractangle (.87f, .73f, .17f, .22f);
		RRight.width = RUp.height;
		RRight.height = RUp.width;
	}

	void Update () {
		if (!MultyplayerInit.st_isPlay)    return;
		if (!isStart0 ) return;

		Gametime += Time.deltaTime;

		if (Gametime > l_arrLevelTime[i1]) {
			Levels();
		}
	}

	void PlayerRank(){
		isApplause = true;
		MultyplayerInit.st_isPlay = false;
	}

	void Lstart(){
		isStart0 = true;
	}

	void OnGUI(){
		m_StyleColor.fontSize = Screen.width / 28;
		Rect RGroup = Ractangle (0.17f, 0.8f, 0.6f, 0.2f);
		
		
		GUI.BeginGroup (RGroup);
		Rect R = Ractangle (0, 0, 0f, 0.05f);
		for (int i = 0; i < CommonS.st_listPlayerPoints.Count; i++) {
			
			GUI.Label(R, CommonS.st_listPlayerPoints[i], m_StyleColor);
			if(i%2 != 0){
				R.y += (Screen.height* 0.08f);
				R.x -= (RGroup.width/2);
			}
			if(i%2 == 0)    R.x += (RGroup.width/2);
		}
		GUI.EndGroup ();

		if (isApplause) {
		GUILayout.Label ("");
		GUILayout.Label ("");
//			Debug.Log("Player name       COunt:"+CommonS.st_listPlayerName.Count);
			for (int i = 1; i <= CommonS.st_listPlayerName.Count; i++) {
				GUILayout.Label (i + " Rank " + CommonS.st_listPlayerName [CommonS.st_listPlayerName.Count-i], m_StyleEmpty);
		}
	}
//		Rect R = Ractangle (0.01f, 0.002f, 0, 0.1f);
//		R.width = R.height;
//		if(GUI.Button(R, l_texPause, m_StyleEmpty) ){
//			GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().clip = ClickClip;
//			GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().Play ();
//			
//			GameObject.Find("PlayerContainer").SendMessage("GamePause");	
//		}
		
		R = Ractangle (0.01f, 0.002f, 0, 0.1f);
//		R = Ractangle (0.12f, 0.002f, 0, 0.1f);
		R.width = R.height;
		if (st_IsMusicOn) {
			if(GUI.Button(R,l_texMusicOn, m_StyleEmpty) ){
				GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().clip = ClickClip;
				GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().Play ();
				MusicOnOff();
			}	
		}
		else{
			if(GUI.Button(R,l_texMusicOff, m_StyleEmpty) ){
				GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().clip = ClickClip;
				GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().Play ();
				MusicOnOff();
			}	
		}
		
		R = Ractangle (0, 0.002f, 0, 0.09f );
		R.width =  (3.93f) * R.height;
		R.x = (Screen.width / 2) - (R.width/2);
		GUI.DrawTexture (R, l_texLevel_and_time);
		
		m_StyleEmpty.fontSize = Screen.width / 30;
		m_StyleEmpty.alignment = TextAnchor.MiddleCenter;
		GUI.BeginGroup (R);
		Rect R2 = Ractangle (0.0135f, 0.007f, 0, 0.07f);
		R2.width = R2.height;
		GUI.Box (R2, st_iCurrentLevel.ToString (), m_StyleEmpty);
		
		R2 = Ractangle (0, 0.008f, 0, 0.07f);
		R2.width =  3.2f * R2.height;
		R2.x = (R.width * 0.94f) - R2.width ;
		GUI.Box (R2, TimeString (Gametime), m_StyleEmpty);
		GUI.EndGroup ();

		if (!MultyplayerInit.st_isPlay)     return;

		GUI.Button (RLeft, l_texLeftArrow, m_StyleEmpty);
		GUI.Button (RRight, l_texRightArrow, m_StyleEmpty);
		GUI.Button (RUp, l_textUpArrow, m_StyleEmpty);
		GUI.Button (RDown, l_texDownArrow, m_StyleEmpty);
	}

	Rect Ractangle(float x, float y, float w, float h){
		return new Rect (Screen.width * x, Screen.height * y, Screen.width * w, Screen.height * h);
	}

	public string TimeString(float CurrentTime){
		string s;
		if(CurrentTime==0f)
		{
			s="0.0";
		}
		else
		{
			s = CurrentTime.ToString();
		}

		Second = (int.Parse(s.Split('.')[0]) % 60).ToString();
		if(Second.Length == 1){
			Second = "0" + Second;
		}
		
		Minute = (int.Parse(s.Split('.')[0]) / 60).ToString();
		if(Minute.Length == 1){
			Minute = "0" + Minute;
		}
		return Minute + ":" + Second;
	}

	void Levels(){
		i1++;
		st_iCurrentLevel++;


		if (st_iCurrentLevel == 13) {
			i1 = 0;
			st_iCurrentLevel = 12;
			ShowWonMenu();
		}
		GameObject[] G = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] G1 = GameObject.FindGameObjectsWithTag("JumpObject");
		if (st_iCurrentLevel <= 5) {
			RotateImageFinal.st_fRotateMainspeed += 0.03f;
			foreach (GameObject C in G) {
				C.GetComponent<Animator>().speed += 0.005f;	
			}
			foreach (GameObject C1 in G1) {
				C1.GetComponent<Animation>().animation["Jump"].speed += 0.04f;
			}
		}
		else{
			RotateImageFinal.st_fRotateMainspeed += 0.04f;
			foreach (GameObject C in G) {
				C.GetComponent<Animator>().speed += 0.008f;
			}
			foreach (GameObject C1 in G1) {
				C1.GetComponent<Animation>().animation["Jump"].speed += 0.04f;
			}
		}
	}

	void ShowWonMenu(){
		m_objWonBtn.SetActive(true);
		InitSpirel.st_isPlay = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = false;
//		m_meshWinPoint.text = "& you won " + PlayerPrefs.GetInt ("Points", 0) + " points";
		m_objWonBtn.GetComponent<Animation> ().Play ();
		StartCoroutine("Wait",m_objWonBtn.animation ["ZoomOut"].length);
	}
	void MusicOnOff (){
		st_IsMusicOn = !st_IsMusicOn;
		PlayerPrefs.SetInt("IsMusicOn", (st_IsMusicOn) ? 1 : 0);
		AudioListener.volume = ((st_IsMusicOn) ? 1 : 0);
		
		//		GameObject.Find("MusicBtn").renderer.material.mainTexture = (st_IsMusicOn) ? l_texMusicOn : l_texMusicOff;
		//		StartCoroutine( SetVolume());
	}
	
//	IEnumerator SetVolume(){
//		BtnClick.st_boolChkMusic = false;
//		print ("SetVolume");
//		float i, value;
//		i = (st_IsMusicOn) ? 0: 1;
//		value = (st_IsMusicOn) ? 0.05f : -0.05f;
//	repeat :
//			i += value;
//		AudioListener.volume = i;
//		yield return new WaitForSeconds (0.01f);
//		if(st_IsMusicOn){
//			if(i < 1)   goto repeat;
//			AudioListener.volume = 1;
//		}
//		else{
//			if(i > 0)   goto repeat;
//			AudioListener.volume = 0;
//		}
//		GameObject.Find("MusicBtn").renderer.material.mainTexture = (st_IsMusicOn) ? l_texMusicOn : l_texMusicOff;
//		
//		BtnClick.st_boolChkMusic = true;
//	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitSpirel : MonoBehaviour {
	public static  bool IsTrigger = true, st_isArmour,st_isInvisible;

//	public static float NoOfSpilePassed;
//	public static int st_iCurrentLevel;
	public static bool st_isPlay;
	public static float st_fPoints = 0;

	public GameObject m_objNextLevel, m_objWonBtn, m_PrfbMtlBar, m_prfbRock, m_prfbWaterDrop, m_prfbLogs, m_prfbSlipperyIce, m_prfbCracker , m_objLifeAddMenu, m_arrobjParent;//m_PrfbIceBeam, m_prfbElectricBeam, m_prfbFireBeam, m_PrfbSpirel ;
	public GameObject BoyPlayer, GirlPlayer;
	GameObject l_ObjBeam;
	public Texture2D m_texArmour, m_texInvisible;
	Texture2D l_texLevel_and_time, l_texLife, l_texLife_bg, l_texPause, l_texMusicOn, l_texMusicOff;
	
	public GUIStyle m_StyleEmpty, styleColor, styleBlack;
//	public TextMesh txtCrnntTime,txtCurntLevel;
	public TextMesh m_meshCountDown, m_meshWinPoint1,m_meshWinPoint2, m_meshNextLevel;
	
	public float Gametime = 0f, showGametime = 0;

//	GameObject l_ObjBeam;
	List<GameObject> l_listObj = new List<GameObject>();
	Transform[] l_arrTrasChild = new Transform[8];
	Vector3 PosMetalbar, PosWaterDrop,l_v3RockDrops, l_v3CamePos;
	AudioClip ClickClip, BGclip;

	delegate void GameEvent();
	event GameEvent Method;
	bool isLevelPopUp, isWinPopUp;
	string Minute, Second;

	float t = 0f, add ;
	float Speed1, l_fScecond,l_fTimer;
	float SpeedMin, SpeedMax, l_RndmRange4time;
	bool st_IsMusicOn, IsLifeAdded;
	Color l_colcorRock = new Color();
	float[] l_arrObjFall =new float[]{.7f, 1.3f, 2f};
	int itrack;
//	private float[] l_arrLevelTime = {10, 20,30,40,50,60,67,74,81 , 88, 95, 102};
	float Leveltime;
	
//	private float[] l_arrLevelTime = {60, 120, 180, 240, 300, 420, 460, 500, 540, 580, 620, 660};
	
	private float[] l_arrSpeed1 = {0.2f, 0.23f, 0.26f, 0.3f, 0.34f, 0.38f , 0.42f, 0.46f, 0.5f , 0.54f, 0.58f, 0.62f};
//	private float[] l_arrSpeed2 = {0.2, 20,30,40,50,60,67,74,81 , 88, 95, 102};
	int i1;

	void Awake() { 
		Invoke("InitObjects", 3f);
		l_texLevel_and_time = (Texture2D)Resources.Load ("Texture/level-and-time")as Texture2D;
		l_texLife = (Texture2D)Resources.Load ("Texture/Life")as Texture2D;
		l_texLife_bg =(Texture2D) Resources.Load ("Texture/life-bg")as Texture2D;
		l_texPause = (Texture2D)Resources.Load ("Texture/pause")as Texture2D;
		l_texMusicOn = (Texture2D)Resources.Load ("Texture/music-on")as Texture2D;
		l_texMusicOff = (Texture2D)Resources.Load ("Texture/music-off")as Texture2D;

		if (PlayerPrefs.GetString ("PlayerSelect") == "Girl") {
			GameObject.FindGameObjectWithTag("Boy").SetActive(false);		
		}
		else{
			GameObject.FindGameObjectWithTag("Girl").SetActive(false);		
		}
	}

	void Start () {
//		st_iCurrentLevel = CommonS.st_iCurrentLevel;


		ResetGame ();
		ClickClip = Resources.Load ("Clip/ClickSound") as AudioClip;

		if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock || CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
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

		st_IsMusicOn = (PlayerPrefs.GetInt ("IsMusicOn", 1) == 1) ? true : false;
		AudioListener.volume = (st_IsMusicOn) ? 1 : 0;
		//		GameObject.Find("MusicBtn").renderer.material.mainTexture = (st_IsMusicOn) ? l_texMusicOn : l_texMusicOn;
		
//		add =  0.2f;
		l_RndmRange4time = 0f;
//		Speed1 = 2f;
//		l_fScecond = (1f * 60);
		
		
//		t = 0f;
//		SpeedMin = 2.5f;
//		SpeedMax = 2.5f - add;
//		add += 0.2f;

		for (int j = 0; j < 4; j++) {
			l_arrTrasChild [j] = m_arrobjParent.transform.GetChild (j);
		}

//		txtCurntLevel.text = st_iCurrentLevel.ToString();
//		for (int i = 0; i < m_arrobjParent.Length; i++) {
//			for (int j = 0; j < 4; j++) {
//				l_arrTrasChild [j + (i * 4)] = m_arrobjParent[i].transform.GetChild (j);
//			}
//		}
	}

	void Update(){
		if (!InitSpirel.st_isPlay)    return;

//		Speed1 = Mathf.Lerp (SpeedMin, SpeedMax - 0.00001f, t);
//		float[] RndmTime1 = new float[]{Speed1 - 0.2f, Speed1 - 0.4f, Speed1 - 0.6f};
		float[] RndmTime2 = new float[]{Speed1 - 0.1f, Speed1 - 0.5f, Speed1 - 0.8f};

//		if (Speed1 <= SpeedMax) {
//			SpeedMin = SpeedMax;
//			SpeedMax = 2.5f - add;
//			print(" Rate = " + SpeedMin +" "+ SpeedMax);
//			
//			add += 0.2f;
//			
//			t = 0;
//		}
//		if (t < 1) {
//			t += Time.deltaTime / l_fScecond;
//		}
		
		l_fTimer += Time.deltaTime;
		Gametime += Time.deltaTime;
		showGametime +=  Time.deltaTime;

		if (l_fTimer > l_RndmRange4time) {
			DestroyObjects();
			if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock){
//				if(Random.value > 0.2f)
					IntBar(m_PrfbMtlBar);
//					l_RndmRange4time = RndmTime2[Random.Range(0,3)];
				
			}
			else{
				 
				if(Random.value < 0.7f)
					IntBar(m_PrfbMtlBar);
				else
					IntBeam ();
				  
//				if(PlayerPrefs.GetInt ("Level", 1) >= 4) l_RndmRange4time = RndmTime2[Random.Range(0,3)];  
			}
			l_RndmRange4time = RndmTime2[Random.Range(0,3)];
			l_fTimer = 0;
		}

		if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock){
			SetStage("RLevel");
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			SetStage("ELevel");
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			SetStage("ILevel");
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			SetStage("FLevel");
		}

//		if ( (!IsLifeAdded  && st_iCurrentLevel >= 5 &&  (int.Parse(Minute) <= 1)  && (int.Parse(Second) <= 30)) ){
//			ExtraLife();
//		}
	}

	void SetStage(string Level){
		if (PlayerPrefs.GetInt (Level, 1) <= 11) {
			if (Gametime > Leveltime/2f) {
				RotateImageFinal.st_fRotateMainspeed = (l_arrSpeed1 [PlayerPrefs.GetInt (Level, 1) - 1] + l_arrSpeed1 [PlayerPrefs.GetInt (Level, 1)]) / 2f;
				print("RotateImageFinal.st_fRotateMainspee = "+RotateImageFinal.st_fRotateMainspeed);			
			}
		}
		if (Gametime > Leveltime) {
			ChangeLevel();
		}
	}

	void ExtraLife(){
		if(MyPlayer.st_LifeCnter < 6){
			ShowLifeAddMenu();
			MyPlayer.st_LifeCnter+=2;
			if(MyPlayer.st_LifeCnter>6)
				MyPlayer.st_LifeCnter=6;
			IsLifeAdded=true;
		}
	}

	public void ShowLifeAddMenu(){
		m_objLifeAddMenu.SetActive(true);
		InitSpirel.st_isPlay = false;
		m_objLifeAddMenu.GetComponent<Animation> ().Play ();
		StartCoroutine("Wait",m_objLifeAddMenu.animation ["ZoomOut"].length);
	}

	public void ShowWonMenu(){
		m_objWonBtn.SetActive(true);
		InitSpirel.st_isPlay = false;
//		m_meshWinPoint1.text = "You won 200 points.";
		if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock){
			m_meshWinPoint1.text = "Electricity Theme is Unlock. \n You won " + PlayerPrefs.GetInt ("Points", 0) + " points.";
			
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			m_meshWinPoint1.text = "Ice Theme is Unlock. \n You won " + PlayerPrefs.GetInt ("Points", 0) + " points.";
			
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			m_meshWinPoint1.text = "Fire Theme is Unlock. \n You won " + PlayerPrefs.GetInt ("Points", 0) + " points.";
			
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			m_meshWinPoint1.text = "Rock Theme is Unlock. \n You won " + PlayerPrefs.GetInt ("Points", 0) + " points.";
		}
//		m_meshWinPoint1.text = "You won " + PlayerPrefs.GetInt ("Points", 0) + " points.";

		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = false;
		m_objWonBtn.GetComponent<Animation> ().Play ();
		StartCoroutine("Wait",m_objWonBtn.animation ["ZoomOut"].length);
	}

	void ShownextLevelMenu(){
		m_objNextLevel.SetActive(true);
		InitSpirel.st_isPlay = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = false;
//		Time.timeScale = 0;
		if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock){
			m_meshNextLevel.text = "Level: " + PlayerPrefs.GetInt ("RLevel", 1);
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			m_meshNextLevel.text = "Level: " + PlayerPrefs.GetInt ("ELevel", 1);
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			m_meshNextLevel.text = "Level: " + PlayerPrefs.GetInt ("ILevel", 1);
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			m_meshNextLevel.text = "Level: " + PlayerPrefs.GetInt ("FLevel", 1);
		}

		m_objNextLevel.GetComponent<Animation> ().Play ();
		StartCoroutine("Wait1",m_objWonBtn.animation ["ZoomOut"].length);

//		yield return new WaitForSeconds (5);
//		m_objNextLevel.SetActive (false);
//		//		"\n You won " + PlayerPrefs.GetInt ("Points", 0) + " points.";
//		Time.timeScale = 1;
//		InitSpirel.st_isPlay = true;
//		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().enabled = true;

	}

	void ResumeLifeAdd(){
		Time.timeScale = 1f;
		InitSpirel.st_isPlay = true;
		m_objLifeAddMenu.SetActive (false);
	}

	IEnumerator Wait(float t){
		yield return new WaitForSeconds (t);
		Time.timeScale = 0f;
	}
	IEnumerator Wait1(float t){
		yield return new WaitForSeconds (t);
		Time.timeScale = 0.001f;

		if (m_objNextLevel.activeInHierarchy) {
			yield return new WaitForSeconds (0.002f);
			m_objNextLevel.SetActive (false);
			StartCoroutine(CountDown(0.0006f,1000f));
		}

//		Camera.main.SendMessage ("ResetLevel");
	}


	public string TimeString(float CurrentTime){
		string s;
		if(CurrentTime==0f)
		{
			s="0.0";
		}
		else
		{
			s=CurrentTime.ToString();
		}
		
		Second = (int.Parse(s.Split('.')[0]) % 60).ToString();
		if(Second.Length == 1){
			Second = "0" + Second;
		}
		
		Minute = (int.Parse(s.Split('.')[0]) / 60).ToString();
		if(Minute.Length == 1){
			Minute = "0" + Minute;
		}
		return Minute+":"+Second;
	}

	void OnGUI(){
		Rect R = Ractangle (0.01f, 0.002f, 0, 0.1f);
		R.width = R.height;
		if(GUI.Button(R, l_texPause, m_StyleEmpty) ){
			GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().clip = ClickClip;
			GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().Play ();
			
			GameObject.Find("PlayerContainer").SendMessage("GamePause");	
		}
		R = Ractangle (0.12f, 0.002f, 0, 0.1f);
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
		if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock){
			GUI.Box (R2, PlayerPrefs.GetInt ("RLevel", 1).ToString (), m_StyleEmpty);
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			GUI.Box (R2, PlayerPrefs.GetInt ("ELevel", 1).ToString (), m_StyleEmpty);
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			GUI.Box (R2, PlayerPrefs.GetInt ("ILevel", 1).ToString (), m_StyleEmpty);
		}
		else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			GUI.Box (R2, PlayerPrefs.GetInt ("FLevel", 1).ToString (), m_StyleEmpty);
		}
		
		R2 = Ractangle (0, 0.008f, 0, 0.07f);
		R2.width =  3.2f * R2.height;
		R2.x = (R.width * 0.94f) - R2.width ;
		GUI.Box (R2, TimeString (Gametime), m_StyleEmpty);
		GUI.EndGroup ();
		
		R = Ractangle (0f, 0.01f, 0, 0.103f);
		R.width = 2.3f * R.height;
		R.x = (Screen.width * 0.995f) - (R.width);
		GUI.DrawTexture (R, l_texLife_bg);
		
		GUI.BeginGroup (R);
		Rect R1 = Ractangle(0.008f, 0.014f ,0, 0.072f);
		R1.width = 0.5f * R1.height;
		for(int i = 0; i < MyPlayer.st_LifeCnter; i++){
			GUI.DrawTexture(R1, l_texLife);
			R1.x += (R1.width); 
		}
		GUI.EndGroup ();

		if(!st_isPlay)   return;

		R = Ractangle(0.17f, 0.85f ,0, 0.15f);
		R.width = R.height;
		//		Rect Rnum = Ractangle(0.035f, 0.85f ,0, 0f);
		
		styleBlack.fontSize = Screen.width/24;
		//		GUI.contentColor = Color.clear;
		if (PlayerPrefs.GetInt ("Armour") >= 1) {
			if(GUI.Button(R, m_texArmour, m_StyleEmpty) && !st_isArmour){
				print("Armour");
				st_isArmour = true;
				PlayerPrefs.SetInt ("Armour",  PlayerPrefs.GetInt ("Armour") -1);
				GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.SetColor("_Color",Color.red);
			}
			GUI.Label(R, PlayerPrefs.GetInt ("Armour")+"", styleBlack);
		}
		R.x = R.x + R.width + (R.width/4);
		//		Rnum.x = R.x + R.width/2;
		if (PlayerPrefs.GetInt ("Invisible") >= 1) {
			if(GUI.Button(R, m_texInvisible, m_StyleEmpty) && !st_isInvisible){
				print("Invisible");
				
				PlayerPrefs.SetInt ("Invisible",  PlayerPrefs.GetInt ("Invisible") -1);
				st_isInvisible = true;
				GameObject.FindGameObjectWithTag("PlayerMesh").renderer.enabled = false;
				GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>().enabled = false;
				StartCoroutine(Visible());
			}
			GUI.Label(R, PlayerPrefs.GetInt ("Invisible")+"", styleBlack);
		}

//		styleColor.alignment = TextAnchor.MiddleCenter;
//		styleColor.fontSize = Screen.width/30;
//		if (isWinPopUp) {
//			R = Ractangle (0f, 0.7f, 0.27f, 0);
//			R.x = Screen.width/2 - R.width;
//			GUI.contentColor = Color.yellow;
//			GUI.Label (R, "Congratulation!", styleColor);
//			R.x = Screen.width/2;
//			GUI.contentColor = Color.red;
//			GUI.Label (R, " You enter in " + st_iCurrentLevel + " level.", styleColor);
//		}
//
//		if (isLevelPopUp) {
////			R = Ractangle (0.55f, 0.92f, 0.27f, 0);
//////			R.x = Screen.width/2 - R.width;
////			GUI.contentColor = Color.green;
////			GUI.Label (R, "Congratulation!", styleColor);
////			R = Ractangle (0.55f, 0.92f, 0, 0);
////			GUI.Label (R, "Congratulation! You won \n Points : " + PlayerPrefs.GetInt ("Points", 0), styleColor);
//		GUI.contentColor = Color.green;
//		R = Ractangle (0.37f, 0.85f, 0.23f, 0.05f);
//		GUI.Label (R, "Congratulation!", styleColor);
//		GUI.contentColor = Color.red;
//		R = Ractangle (0.6f, 0.85f, 0.14f, 0.05f);
//		GUI.Label (R, "You won", styleColor);
//		R = Ractangle (0.37f, 0.93f, 0.37f, 0.05f);
//		GUI.Button (R, "Points : " + PlayerPrefs.GetInt ("Points", 0),styleColor);
//		}
	}

	Rect Ractangle(float x, float y, float w, float h){
		return new Rect (Screen.width * x, Screen.height * y, Screen.width * w, Screen.height * h);
	}


	IEnumerator Visible(){
		yield return new WaitForSeconds (5);
		st_isInvisible = false;
		GameObject.FindGameObjectWithTag("PlayerMesh").renderer.enabled = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>().enabled = true;
	}

	void ResetGame(){
		Time.timeScale = 1f;

//		RotateImageFinal.st_fRotateMainspeed = 0.2f;
//		st_iCurrentLevel = 1;
//		Speed1 = 2f;
		Gametime = 0f;
//		if (PlayerPrefs.GetInt ("Level", 1) == 1) {
//			Gametime = 0;		
//		}
//		else{
//			Gametime = l_arrLevelTime[PlayerPrefs.GetInt ("Level", 1) -2];
//		}

//		i1 = 0;
		StartCoroutine (CountDown(0.6f, 1f));

		m_objWonBtn.SetActive (false);

		CommonS.st_IsCollider =st_isInvisible = st_isArmour = false;
		IsLifeAdded = false;
		

//		NoOfSpilePassed = 0;
//		RotateImageFinal.st_fRotateMainspeed = l_arrSpeed1[PlayerPrefs.GetInt ("Level", 1) - 1];
//		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().speed = 1.01f +  (PlayerPrefs.GetInt ("Level", 1) / 100f);
//		GameObject.Find("JumpObject").GetComponent<Animation>().animation["Jump"].speed = 1.01f + (PlayerPrefs.GetInt ("Level", 1) / 100f);
//		if (PlayerPrefs.GetInt ("Level", 1) >= 4) {
//			Speed1 = Speed1 - (PlayerPrefs.GetInt ("Level", 1) / 100f);
//		}
//		if (PlayerPrefs.GetInt ("Level", 1) == 1) {
//			Gametime = 0;		
//		}
//		else{
//			Gametime = l_arrLevelTime[PlayerPrefs.GetInt ("Level", 1) -2];
//		}

		Speed1 = 2f;
		if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock) {
			PlayerPrefs.SetInt("SpiralTex", 1);
			l_colcorRock = Color.grey;

			SetSpeed("RLevel");
//			SetTime("RLevel");
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			PlayerPrefs.SetInt("SpiralTex", 2);
			l_ObjBeam = (GameObject) Resources.Load("Prefab/" + "BeamElectric") as GameObject;
			Method += InitWaterDrop;
			l_colcorRock = Color.blue;

			SetSpeed("ELevel");
//			SetTime("ELevel");
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			PlayerPrefs.SetInt("SpiralTex", 3);
			l_ObjBeam = (GameObject) Resources.Load("Prefab/" + "BeamIce") as GameObject;
			Method += InitSlipperyIce;
			l_colcorRock = Color.cyan;

			SetSpeed("ILevel");
//			SetTime("ILevel");
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			PlayerPrefs.SetInt("SpiralTex", 4);
			l_ObjBeam = (GameObject) Resources.Load("Prefab/" + "BeamFire") as GameObject;
			Method += InitLog;
			l_colcorRock = Color.red;

			SetSpeed("FLevel");
//			SetTime("FLevel");
		}
	}

	void CallMethod(){
		if(Method != null){
			Method();
		}
	}

	void ResetLevel(){
		Time.timeScale = 1;

		InitSpirel.st_isPlay=true;
		IsLifeAdded = false;

		if(Trigger.IsFreezed)
		{
			Trigger.IsFreezed = false;
//			GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.shader = Shader.Find("Diffuse");
		}
//		NoOfSpilePassed = 0;
		

//		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().speed = 1f;
//		GameObject.Find("JumpObject").GetComponent<Animation>().animation["Jump"].speed = 1f;

//		st_iCurrentLevel =1;
//		RotateImageFinal.st_fRotateMainspeed = 0.2f;
//		Speed1 = 2f;
		Gametime = 0f;
//		if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock) {
//			SetTime("RLevel");
//		}
//		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
//			SetTime("ELevel");
//		}
//		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
//			SetTime("ILevel");
//		}
//		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
//			SetTime("FLevel");
//		}
//		i1 = 0;
	}

	void MusicOnOff (){
		st_IsMusicOn = !st_IsMusicOn;
		PlayerPrefs.SetInt("IsMusicOn", (st_IsMusicOn) ? 1 : 0);
		AudioListener.volume = ((st_IsMusicOn) ? 1 : 0);

//		GameObject.Find("MusicBtn").renderer.material.mainTexture = (st_IsMusicOn) ? l_texMusicOn : l_texMusicOff;
//		StartCoroutine( SetVolume());
	}

	IEnumerator SetVolume(){
		BtnClick.st_boolChkMusic = false;
		print ("SetVolume");
		float i, value;
		i = (st_IsMusicOn) ? 0: 1;
		value = (st_IsMusicOn) ? 0.05f : -0.05f;
	repeat :
			i += value;
		AudioListener.volume = i;
		yield return new WaitForSeconds (0.01f);
		if(st_IsMusicOn){
			if(i < 1)   goto repeat;
			AudioListener.volume = 1;
		}
		else{
			if(i > 0)   goto repeat;
			AudioListener.volume = 0;
		}
		GameObject.Find("MusicBtn").renderer.material.mainTexture = (st_IsMusicOn) ? l_texMusicOn : l_texMusicOff;
		
		BtnClick.st_boolChkMusic = true;
	}

	IEnumerator CountDown(float wait, float AnimSpeed){
//		if (BoyPlayer.activeInHierarchy) {
//			print("Boyes2");
//			BoyPlayer.GetComponent<Animator> ().enabled = false;
//		}
//		if (GirlPlayer.activeInHierarchy) {
//			GirlPlayer.GetComponent<Animator> ().enabled = false;
//		}
		GameObject.Find("PlayerContainer").SendMessage("ResetPlayer");	
		
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = false;
		st_isPlay = false;
		m_meshCountDown.text = 3 + "";
		m_meshCountDown.animation ["InCountDown"].speed = AnimSpeed;
		m_meshCountDown.animation.Play ("InCountDown");
		yield return new WaitForSeconds (wait);
		
		for (int i = 3; i >= 1; i--) {
			m_meshCountDown.animation ["CountDown"].speed = AnimSpeed;
			m_meshCountDown.animation.Play ("CountDown");
			
			m_meshCountDown.text = i + "";
			//			if(MainMenu.st_IsMuteOn) AudioSource.PlayClipAtPoint(m_clipDengerBeep, transform.position);
			yield return new WaitForSeconds (wait);	
		}
		m_meshCountDown.text = "Go";
		m_meshCountDown.animation ["OutCountDown"].speed = AnimSpeed;
		m_meshCountDown.animation.Play ("OutCountDown");
		st_isPlay = true;
//		if (BoyPlayer.activeInHierarchy) {
//			print("Boyes2");
//			BoyPlayer.GetComponent<Animator> ().enabled = true;
//		}
//		if (GirlPlayer.activeInHierarchy) {
//			GirlPlayer.GetComponent<Animator> ().enabled = true;
//		}
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = true;
		if(AnimSpeed == 1000f){   	
			Time.timeScale =1;
		}
	}

	void IntBar(GameObject PrfbBar){
		GameObject ObjMetalbar = (GameObject)Instantiate (PrfbBar);
		l_listObj.Add (ObjMetalbar);
		if(Random.value>0.5f){
			ObjMetalbar.transform.localPosition = CommonS.PosMtlBarDwn;
		}
		else{
			ObjMetalbar.transform.localPosition = CommonS.PosMtlBarUp;
		}
//		ObjMetalbar.transform.localRotation =Quaternion.Euler (0f, 176.2591f, 0f);
		ObjMetalbar.transform.localRotation = Quaternion.Euler (0f, 167.5f, 0f);
		
		ObjMetalbar.transform.parent =  GameObject.Find("MainPath").transform;
	}
	
	void IntBeam(){
		GameObject ObjBeam = (GameObject)Instantiate (l_ObjBeam);
//		ObjBeam.renderer.material.color = l_colcorRock;
		l_listObj.Add (ObjBeam);
		//		if(Random.Range(0,2)==1)
		//		{
		//			ObjBeam.transform.localPosition = CommonS.PosMtlBarDwn;
		//		}
		//		else
		//		{
		//			ObjBeam.transform.localPosition = CommonS.PosMtlBarUp;
		//		}
		//		ObjBeam.transform.localPosition = CommonS.PosMtlBarUp;
		//		ObjMetalbar.transform.localRotation =Quaternion.Euler (0f, 176.2591f, 0f);
		ObjBeam.transform.parent =  GameObject.Find("MainPath").transform;
	}

	void ChangeLevel(){	
			
		
		Speed1 = 2f;
		Gametime = 0;
		if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock) {
			PlayerPrefs.SetInt ("RLevel", PlayerPrefs.GetInt ("RLevel", 1) + 1);
			
			if(PlayerPrefs.GetInt ("RLevel", 1) == 13){
				PlayerPrefs.SetInt ("Theme", 2);
				PlayerPrefs.SetInt ("RLevel", 1);
//				PlayerPrefs.SetInt ("Lifes", 6);
				ShowWonMenu();
			}  
			else{	
				ShownextLevelMenu();
			}
			SetPoints("RLevel");
			SetSpeed("RLevel");
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			PlayerPrefs.SetInt ("ELevel", PlayerPrefs.GetInt ("ELevel", 1) + 1);
			
			if(PlayerPrefs.GetInt ("ELevel", 1) == 13) {
				PlayerPrefs.SetInt ("Theme", 3);
				PlayerPrefs.SetInt ("ELevel", 1);
//				PlayerPrefs.SetInt ("Lifes", 6);
				ShowWonMenu();
			}
			else{	
				ShownextLevelMenu();
			}
			SetPoints("ELevel");
			SetSpeed("ELevel");
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			PlayerPrefs.SetInt ("ILevel", PlayerPrefs.GetInt ("ILevel", 1) + 1);
			
			if(PlayerPrefs.GetInt ("ILevel", 1) == 13){  
				PlayerPrefs.SetInt ("Theme", 4);
				PlayerPrefs.SetInt ("ILevel", 1);
//				PlayerPrefs.SetInt ("Lifes", 6);
				ShowWonMenu();
			}
			else{	
				ShownextLevelMenu();
			}
			SetPoints("ILevel");
			SetSpeed("ILevel");
			
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			PlayerPrefs.SetInt ("FLevel", PlayerPrefs.GetInt ("FLevel", 1) + 1);
			
			if(PlayerPrefs.GetInt ("FLevel", 1) == 13){  
				PlayerPrefs.SetInt ("FLevel", 1);
				PlayerPrefs.SetInt ("Lifes", 6);
				ShowWonMenu();
			}
			else{	
				ShownextLevelMenu();
			}
			SetPoints("FLevel");
			SetSpeed("FLevel");
		}


//		isWinPopUp = true;
//		StartCoroutine(ClosePopUp());
		
//		RotateEmpty.Mainspeed = RotateEmpty.Mainspeed+ (st_iCurrentLevel / 10);//-(st_iCurrentLevel/1000));//0.1f;
//		if (PlayerPrefs.GetInt ("Level", 1) <= 3) {
//			RotateImageFinal.st_fRotateMainspeed += 0.03f;
//			
////			Speed1 -= 0.01f;
//			GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().speed += 0.008f;
//			GameObject.Find("JumpObject").GetComponent<Animation>().animation["Jump"].speed += 0.004f;
//		}
//		else{
//			RotateImageFinal.st_fRotateMainspeed += 0.04f;
//			
//			Speed1 -= 0.01f;
//			GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().speed += 0.01f;
//			GameObject.Find("JumpObject").GetComponent<Animation>().animation["Jump"].speed += 0.008f;
////			RotateImageFinal.st_fRotateMainspeed = (st_iCurrentLevel / 100);
////			Speed1 -= st_iCurrentLevel / 100;
////			GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().speed = (st_iCurrentLevel / 100);
////			GameObject.Find("JumpObject").GetComponent<Animation>().animation["Jump"].speed = (st_iCurrentLevel / 100);
//		}

//		NoOfSpilePassed=0;
	}

	void SetPoints(string Level){
		if (PlayerPrefs.GetInt (Level, 1) == 4) {
			PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + 200);
			m_meshWinPoint2.text = "You won 200 points.";
		}
		else if (PlayerPrefs.GetInt (Level, 1) == 7) {
			PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + 200);
			m_meshWinPoint2.text = "You won 200 points.";
			
		}
		else if (PlayerPrefs.GetInt (Level, 1) == 10) {
			PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + 200);
			m_meshWinPoint2.text = "You won 200 points.";				
		}
		else if (PlayerPrefs.GetInt (Level, 1) == 13) {
			PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + 200);
		}
		else{
			m_meshWinPoint2.text = " ";
		}
	}

	void SetSpeed(string Level){
		if(PlayerPrefs.GetInt(Level, 1) <= 6)    Leveltime = 60;
		else                                     Leveltime = 40;

		RotateImageFinal.st_fRotateMainspeed = l_arrSpeed1[PlayerPrefs.GetInt (Level, 1) - 1];
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().speed = 1.01f +  (PlayerPrefs.GetInt (Level, 1) / 100f);
		GameObject.Find("JumpObject").GetComponent<Animation>().animation["Jump"].speed = 1.01f + (PlayerPrefs.GetInt (Level, 1) / 100f);
		if (PlayerPrefs.GetInt (Level, 1) >= 4) {
			Speed1 = Speed1 - (PlayerPrefs.GetInt (Level, 1) / 100f);
		}
	}

//	void SetTime(string Level){
//		if (PlayerPrefs.GetInt (Level, 1) == 1)     Gametime = 0;		
//		else       									Gametime = l_arrLevelTime[PlayerPrefs.GetInt (Level, 1) -2];
//	}

	IEnumerator ClosePopUp(){
		yield return new WaitForSeconds (3f);
		if(isLevelPopUp)   isLevelPopUp = false;
		if(isWinPopUp)   isWinPopUp = false;
	}

	void DestroyObjects(){
		for (int i = 0; i < l_listObj.Count; i++) {
			if ((l_listObj [i].transform.position.y ) < l_v3CamePos.y) {
				Destroy (l_listObj [i]);
				l_listObj.RemoveAt (i);
			}
		}
	}

	void InitObjects(){
		float randomTime = l_arrObjFall[Random.Range(0,3)];
		Invoke("InitObjects", randomTime);

		if (!InitSpirel.st_isPlay)    return;

		if(Random.value < 0.3f){
			itrack = MyPlayer.st_itrack;
		}
		else{
			itrack = Random.Range(0,4);
		}
		if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock) {
			if (PlayerPrefs.GetInt ("RLevel", 1) <= 3)
			{
				if(Random.value < 0.7f)
					InitRock();  //rock
			} 
			else if (PlayerPrefs.GetInt ("RLevel", 1) <= 6) {
				if (Random.value < 0.5f)
					InitRock();  //rock
				else
					InitLog();   // Log  	
			}
			else if (PlayerPrefs.GetInt ("RLevel", 1) <= 10) {
				if (Random.value < 0.25f)
					InitRock();  //rock
				else if (Random.value < 0.5f)
					InitLog();   // Log  	
				else
					InitSlipperyIce();   // SlipperyIce 
			}
			else if (PlayerPrefs.GetInt ("RLevel", 1) <= 12) {
				if (Random.value < 0.25f)
					InitCracker();
				else if (Random.value < 0.5f)
					InitLog();   // Log  	
				else
					InitSlipperyIce();   // SlipperyIce 
			}
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity) {
			if (PlayerPrefs.GetInt ("ELevel", 1) >= 4){
				if (PlayerPrefs.GetInt ("ELevel", 1) >= 11) {
					if (Random.value < 0.25f)
						CallMethod (); 
					else if (Random.value < 0.45f)
						InitRock();
					else
						InitCracker();
				}
				else if (PlayerPrefs.GetInt ("ELevel", 1) >= 7) {
					if (Random.value < 0.6f)
						InitRock();
					else
						CallMethod ();
				}
				else{
					if(Random.value < 7.5f)     CallMethod ();
				}
			}
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice) {
			if (PlayerPrefs.GetInt ("ILevel", 1) >= 4){
				if (PlayerPrefs.GetInt ("ILevel", 1) >= 11) {
					if (Random.value < 0.25f)
						CallMethod (); 
					else if (Random.value < 0.45f)
						InitRock();
					else
						InitCracker();
				}
				else if (PlayerPrefs.GetInt ("ILevel", 1) >= 7) {
					if (Random.value < 0.6f)
						InitRock();
					else
						CallMethod ();
				}
				else{
					if(Random.value < 7.5f)     CallMethod ();
				}
			}
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire) {
			if (PlayerPrefs.GetInt ("FLevel", 1) >= 4){
				if (PlayerPrefs.GetInt ("FLevel", 1) >= 11) {
					if (Random.value < 0.25f)
						CallMethod (); 
					else if (Random.value < 0.45f)
						InitRock();
					else
						InitCracker();
				}
				else if (PlayerPrefs.GetInt ("FLevel", 1) >= 7) {
					if (Random.value < 0.6f)
						InitRock();
					else
						CallMethod ();
				}
				else{
					if(Random.value < 7.5f)     CallMethod ();
				}
			}
		}
	}

	void InitRock(){
		GameObject l_objRocks = (GameObject)Instantiate (m_prfbRock);
		l_objRocks.renderer.material.color = l_colcorRock;
		
//		l_listObj.Add (l_objRocks);
//		int i = Random.Range (0, 4);
//		l_v3RockDrops.x = l_arrTrasChild [i].position.x;
//		l_v3RockDrops.y = 0.34f;
//		l_v3RockDrops.z = l_arrTrasChild [i].position.z;
//		l_objRocks.transform.position = l_v3RockDrops;

//		int i = Random.Range (0, 4);
		l_v3RockDrops.x = l_arrTrasChild [itrack].position.x;
		l_v3RockDrops.y = l_arrTrasChild [itrack].position.y;
		l_v3RockDrops.z = l_arrTrasChild [itrack].position.z;

		l_objRocks.transform.position = l_v3RockDrops;
	
		l_objRocks.transform.parent =  GameObject.Find("MainPath").transform.GetChild(0);
		
		l_listObj.Add (l_objRocks);
	}

	void InitCracker(){
		GameObject l_objCracker = (GameObject)Instantiate (m_prfbCracker);

		l_v3RockDrops.x = l_arrTrasChild [itrack].position.x;
		l_v3RockDrops.y = l_arrTrasChild [itrack].position.y;
		l_v3RockDrops.z = l_arrTrasChild [itrack].position.z;
		
		l_objCracker.transform.position = l_v3RockDrops;
		
		l_objCracker.transform.parent =  GameObject.Find("MainPath").transform.GetChild(0);
		
		l_listObj.Add (l_objCracker);
	}
	
	void InitLog(){
		GameObject l_objLogs = (GameObject)Instantiate (m_prfbLogs);

		l_v3RockDrops.x = l_arrTrasChild [itrack].position.x;
		l_v3RockDrops.y = l_arrTrasChild [itrack].position.y;
		l_v3RockDrops.z = l_arrTrasChild [itrack].position.z;
		
		l_objLogs.transform.position = l_v3RockDrops;
		
		l_objLogs.transform.parent =  GameObject.Find("MainPath").transform.GetChild(0);
		
		l_listObj.Add (l_objLogs);
	}
	
	void InitSlipperyIce(){
		GameObject l_objSlipperyIce = (GameObject)Instantiate (m_prfbSlipperyIce);
		
//		int i = Random.Range (4, 8);
//		l_v3RockDrops.x = l_arrTrasChild [i].position.x;
//		l_v3RockDrops.y = l_arrTrasChild [i].position.y;
//		l_v3RockDrops.z = l_arrTrasChild [i].position.z;
//		l_objSlipperyIce.transform.position = l_v3RockDrops;
		
		l_objSlipperyIce.transform.parent =  GameObject.Find("MainPath").transform;
		l_listObj.Add (l_objSlipperyIce);
	}
	
	
	void InitWaterDrop(){
		GameObject l_objWaterDrop = (GameObject)Instantiate (m_prfbWaterDrop);

		l_v3RockDrops.x = l_arrTrasChild [itrack].position.x;
		l_v3RockDrops.y = l_arrTrasChild [itrack].position.y;
		l_v3RockDrops.z = l_arrTrasChild [itrack].position.z;
		l_objWaterDrop.transform.position = l_v3RockDrops;
		
		//		l_objWaterDrop.transform.localEulerAngles = new Vector3 (9f, 22f, 4f);
		l_objWaterDrop.transform.parent =  GameObject.Find("MainPath").transform.GetChild(0);
		
		l_listObj.Add (l_objWaterDrop);
	}
}
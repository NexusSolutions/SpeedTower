using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyPlayer : MonoBehaviour {
	
	public static int st_LifeCnter, st_itrack = 0;
	public static int BackCnt;
	public static bool st_gameOver;

	public GameObject m_arrobjParent,Player1, m_objResetPlayerBtn;
	public GameObject m_objPauseMenu,m_objHoldMenu;
//	public List<GameObject> Lifes= new List<GameObject>(10);
	public Texture2D m_texRightArrow, m_texLeftArrow, m_texDownArrow, m_textUpArrow;
	public TextMesh m_meshHold, m_meshRemainingLife;
	
	Transform[] l_arrTrasChild = new Transform[4];
	Vector3 l_v3Pos;
	Vector2 firstPressPos, secondPressPos, currentSwipe;
	System.DateTime CurrentTime, oldDate;
	float l_fBackAngel = 8;
	string st_Times;
//	int iLife;
	public GUIStyle m_styleEmpty;
	Rect RUp, RDown, RLeft, RRight;
	Rect EUp, EDown, ELeft, ERight;
	void Start(){
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		m_objResetPlayerBtn.SetActive (false);
		st_LifeCnter = PlayerPrefs.GetInt ("Lifes", 6);
		st_gameOver = false;
		PlayerPrefs.SetString ("sysString", null);
		BackCnt = 0;

		for(int j = 0; j < 4; j++){
			l_arrTrasChild[j] = m_arrobjParent.transform.GetChild(j);
		}

		RDown = Ractangle (.01f, .78f, .17f, .22f);
		RUp = Ractangle (.01f, .55f, .17f, .22f);
		RLeft = Ractangle (.74f, .73f, .17f, .22f);
		//		RRight = rectangle (.88f, .001f, .12f, .17f);
		
		RLeft.width = RUp.height;
		RLeft.height = RUp.width;
		RRight = Ractangle (.87f, .73f, .17f, .22f);
		RRight.width = RUp.height;
		RRight.height = RUp.width;
		
		EDown = Ractangle (.01f, 0f, .17f, .22f);
		EUp = Ractangle (.01f, .26f, .17f, .22f);
		ELeft = Ractangle (.74f, .03f, .17f, .22f);
		//		RRight = rectangle (.88f, .001f, .12f, .17f);
		
		ELeft.width = EUp.height;
		ELeft.height = EUp.width;
		ERight = Ractangle (.87f, .03f, .17f, .22f);
		ERight.width = EUp.height;
		ERight.height = EUp.width;

//		RDown = Ractangle (.01f, .83f, .13f, .18f);
//		RUp = Ractangle (.01f, .64f, .13f, .18f);
//		RLeft = Ractangle (.76f, .8f, .13f, .18f);
//		//		RRight = rectangle (.88f, .001f, .12f, .17f);
//		
//		RLeft.width = RUp.height;
//		RLeft.height = RUp.width;
//		RRight = Ractangle (.88f, .8f, .13f, .18f);
//		RRight.width = RUp.height;
//		RRight.height = RUp.width;
//		
//		EDown = Ractangle (.01f, 0f, .12f, .17f);
//		EUp = Ractangle (.01f, .2f, .12f, .17f);
//		ELeft = Ractangle (.76f, .03f, .12f, .17f);
//		//		RRight = rectangle (.88f, .001f, .12f, .17f);
//		
//		ELeft.width = EUp.height;
//		ELeft.height = EUp.width;
//		ERight = Ractangle (.88f, .03f, .12f, .17f);
//		ERight.width = EUp.height;
//		ERight.height = EUp.width;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.K)) {
			Application.CaptureScreenshot(System.DateTime.Now.Minute + "" + System.DateTime.Now.Second + ".png");		
		}

		if (MyPlayer.st_gameOver) {
			HoldPopUp();
		}

		if (Trigger.IsFreezed)      return;
		if (!InitSpirel.st_isPlay)  return;
		foreach (Touch T in Input.touches) {
			//		if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1)) {
			if(T.phase == TouchPhase.Began){
			if(ELeft.Contains(T.position)){
				StartCoroutine("GoLeft");
			}
			else if(ERight.Contains(T.position)){
				StartCoroutine("GoRight");
			}
			
			if(EUp.Contains(T.position)){
				gameObject.transform.parent.parent.GetComponent<Animation>().Play();
			}
			else if(EDown.Contains(T.position) && GameObject.Find ("JumpObject").GetComponent<Animation>().isPlaying == false){
					Player1.GetComponent<Animator>().Play("Down");
			}
			}
		}
	}

	void LateUpdate() {
		if (Trigger.IsFreezed)      return;
		if (!InitSpirel.st_isPlay)  return;


//		Swipe ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject.Find ("JumpObject").GetComponent<Animation>().Play ("Jump");
//			Player1.GetComponent<Animator>().Play("Jump");
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow)){
			//GameObject.Find ("JumpObject").GetComponent<Animation>().Play("Down");
			Player1.GetComponent<Animator>().Play("Down");
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow) && GameObject.Find ("JumpObject").GetComponent<Animation>().isPlaying == false) {
			StartCoroutine("GoRight");
		} 
		else if (Input.GetKeyDown (KeyCode.LeftArrow) && GameObject.Find ("JumpObject").GetComponent<Animation>().isPlaying == false) {
			StartCoroutine("GoLeft");
		}
		else if  (Input.GetKeyDown (KeyCode.A)) {
			GoBack();
		}
	}

//	public void Swipe(){
//		if(Input.touches.Length > 0){
//			Touch t = Input.GetTouch(0);
//			if(t.phase == TouchPhase.Began){
//				firstPressPos = new Vector2(t.position.x,t.position.y);
//			}
//			if(t.phase == TouchPhase.Moved)	{
//				secondPressPos = new Vector2(t.position.x,t.position.y);
//				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
//				currentSwipe.Normalize();
//				if(currentSwipe.x > 0 && currentSwipe.y > -0.8f && currentSwipe.y < 0.8f ){
//					StartCoroutine("GoRight");
//				}
//				else if(currentSwipe.x < 0 && currentSwipe.y > -0.8f && currentSwipe.y < 0.8f )	{
//					StartCoroutine("GoLeft");
//				}
//				if(currentSwipe.y > 0 && currentSwipe.x > -0.8f && currentSwipe.x < 0.8f)	{
//					GameObject.Find("JumpObject").GetComponent<Animation>().Play();
//				}
//				else if(currentSwipe.y <0 && currentSwipe.x > -0.8f && currentSwipe.x < 0.8f && GameObject.Find ("JumpObject").GetComponent<Animation>().isPlaying == false){
//					Player1.GetComponent<Animator>().Play("Down");
//				}
//
//			}
//		}
//	}

	void OnGUI(){
//		if (Trigger.IsFreezed)      return;
		if (!InitSpirel.st_isPlay)  return;

//		Event E1 = Event.current;
//		
//		if (E1.type == EventType.MouseDown) {
//			if(ELeft.Contains(Input.mousePosition)){
//				StartCoroutine("GoLeft");
//			}
//			else if(ERight.Contains(Input.mousePosition)){
//				StartCoroutine("GoRight");
//			}
//		}
		
		GUI.Button (RLeft, m_texLeftArrow, m_styleEmpty);
		GUI.Button (RRight, m_texRightArrow, m_styleEmpty);
		
//		Event E2 = Event.current;
//		
//		if (E2.type == EventType.MouseDown) {
//			if(EUp.Contains(Input.mousePosition)){
//				GameObject.Find("JumpObject").GetComponent<Animation>().Play();
//			}
//			else if(EDown.Contains(Input.mousePosition) && GameObject.Find ("JumpObject").GetComponent<Animation>().isPlaying == false){
//				Player1.GetComponent<Animator>().Play("Down");
//			}
//		}
		GUI.Button (RUp, m_textUpArrow, m_styleEmpty);
		GUI.Button (RDown, m_texDownArrow, m_styleEmpty);
//		if(GUI.Button(RLeft,m_texLeftArrow, m_styleEmpty)){
//			StartCoroutine("GoLeft");
//		}
//		else if(GUI.Button(RRight,m_texRightArrow, m_styleEmpty)){
//			StartCoroutine("GoRight");
//		}
//
//		if(GUI.Button(RUp,m_textUpArrow, m_styleEmpty)){
//			GameObject.Find("JumpObject").GetComponent<Animation>().Play();
//		}
//		else if(GUI.Button(RDown,m_texDownArrow, m_styleEmpty) && GameObject.Find ("JumpObject").GetComponent<Animation>().isPlaying == false){
//			Player1.GetComponent<Animator>().Play("Down");
//		}

	}
	
	Rect Ractangle(float x, float y, float w, float h){
		return new Rect (Screen.width * x, Screen.height * y, Screen.width * w, Screen.height * h);
	}

	Vector3 RealPos;
	IEnumerator GoRight(){
		if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[0].transform.localPosition.z)){
			st_itrack = 1;
			l_v3Pos.x =  l_arrTrasChild[1].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[1].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[1].transform.localPosition.z)){
			st_itrack = 2;
			l_v3Pos.x =  l_arrTrasChild[2].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[2].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[2].transform.localPosition.z)){
			st_itrack = 3;
			l_v3Pos.x =  l_arrTrasChild[3].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[3].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[3].transform.localPosition.z)){
			st_itrack = 3;
			l_v3Pos.x =  l_arrTrasChild[3].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[3].transform.localPosition.z;
		}
	Repeat :
			yield return new WaitForSeconds (0.01f);
		RealPos = transform.localPosition;
		RealPos.z -= 0.005f;
		transform.localPosition = RealPos;
		
		if (transform.localPosition.z >= l_v3Pos.z) {
			goto Repeat;
		}
		transform.localPosition = l_v3Pos;
		gameObject.transform.localRotation = Quaternion.Euler (0,325.72f,0);
	}
	
	IEnumerator GoLeft(){
		if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[3].transform.localPosition.z)){
			st_itrack = 2;
			l_v3Pos.x =  l_arrTrasChild[2].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[2].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[2].transform.localPosition.z)){
			st_itrack = 1;
			l_v3Pos.x =  l_arrTrasChild[1].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[1].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[1].transform.localPosition.z)){
			st_itrack = 0;
			l_v3Pos.x =  l_arrTrasChild[0].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[0].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[0].transform.localPosition.z)){
			st_itrack = 0;
			l_v3Pos.x =  l_arrTrasChild[0].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[0].transform.localPosition.z;
		}
		
	Repeat :
			yield return new WaitForSeconds (0.01f);
		RealPos = transform.localPosition;
		RealPos.z += 0.005f;
		transform.localPosition = RealPos;
		
		if (transform.localPosition.z <= l_v3Pos.z) {
			goto Repeat;
		}
		transform.localPosition = l_v3Pos;
		gameObject.transform.localRotation = Quaternion.Euler (0,325.72f,0);
	}
	
	float FormatValue(float Val){
		return  float.Parse(Val.ToString("0.0000"));	
	}

	void HoldPopUp(){
		CurrentTime = System.DateTime.Now;
		long temp = System.Convert.ToInt64(PlayerPrefs.GetString("sysString"),null);
		oldDate = System.DateTime.FromBinary(temp);
		
		System.TimeSpan difference = CurrentTime.Subtract(oldDate);

		int RHour = (1 - difference.Hours);
		if (RHour < 0) {
			RHour  = 0;	
		}
//		int RMinuts = (1 - difference.Minutes);
//		if (RMinuts < 0) {
//			RMinuts  = 0;	
//		}


		st_Times = (RHour) + ":" + (60 - difference.Minutes)  + ":" + (60 - difference.Seconds);
		m_meshHold.text = st_Times + "";
		
		if(difference.Hours >= 2){
			Application.LoadLevel(Application.loadedLevel);
			PlayerPrefs.SetString ("sysString", null);
			MyPlayer.st_gameOver = false;
		}
	}

	void ChkIsJumping(){
		if (GameObject.Find ("JumpObject").animation.isPlaying == true)
		{
			float TimeRemain=GameObject.Find ("JumpObject").animation["Jump"].length-GameObject.Find ("JumpObject").animation["Jump"].time;
//			print(TimeRemain);
			StartCoroutine(Waits(TimeRemain));
		}
		else
			GoBack();
	}

	IEnumerator Waits(float T){
		yield return new WaitForSeconds (T);
//		print ("BAck");
		GoBack ();
	}

	void GoBack(){
		BackCnt++;
		if (BackCnt <=12){
			l_fBackAngel += 8;

			Vector3 pos = gameObject.transform.parent.position;
			pos.y -= 0.005f;
			m_arrobjParent.transform.position = pos;

			gameObject.transform.parent.position = pos;
			m_arrobjParent.transform.localRotation = Quaternion.Euler (0, l_fBackAngel, 0);
			gameObject.transform.parent.localRotation =  Quaternion.Euler (0, l_fBackAngel, 0);
		}
		else{
			CutLife ();
		}
	}

	void CutLife(){
//		isBlink = false;
		BackCnt = 0;
		
		PlayerPrefs.SetInt ("Lifes",PlayerPrefs.GetInt ("Lifes") - 1);
		if(PlayerPrefs.GetInt ("Lifes") < 6)     st_LifeCnter--;
//		print ("LifeCounter " + LifeCnter);
//		Lifes [st_LifeCnter].SetActive (false);
//		Lifes.Remove(Lifes[st_LifeCnter]);
		
		
		if (st_LifeCnter != 0) {
//			InitSpirel.st_isPlay=false;
//			m_objResetPlayerBtn.SetActive (true);
			m_objResetPlayerBtn.SetActive (true);
			m_objResetPlayerBtn.GetComponent<Animation>().Play("ZoomOut");	
			StartCoroutine("Wait",m_objResetPlayerBtn.animation ["ZoomOut"].length);
			m_meshRemainingLife.text = "Remaining life are " + PlayerPrefs.GetInt ("Lifes") + "";		
			InitSpirel.st_isPlay=false;
		}
		else {
			GameOver();
		}
	}

	void ResetPlayer(){

//		Time.timeScale = 1;
//		InitSpirel.Gametime = 0f;
//		InitSpirel.CurrentLevel=1;
//		CommonS.st_IsCollider = false;
//		InitSpirel.st_isPlay = true;
		if(m_objResetPlayerBtn.activeInHierarchy) 	  m_objResetPlayerBtn.SetActive (false);
//		StopCoroutine ("DoBlinks");

//		GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.shader=Shader.Find("Diffuse");

//		GameObject obj = GameObject.Find ("EndOfSpiral");
//		obj.transform.parent=null;
//		obj.transform.localPosition=new Vector3(0f,0.335f,-0.1348f);
//		obj.transform.localEulerAngles=new Vector3(-4f,85f,0f);
//		obj.transform.parent=GameObject.Find("MainPath").transform;
		BackCnt = 0;
		st_itrack = 0;
		if (Player1.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).IsName("Down")) {
			Player1.GetComponent<Animator>().Play("Run");
		}

//		if(GameObject.Find ("JumpObject").GetComponent<Animation> ().isPlaying){
//			GameObject.Find ("JumpObject").GetComponent<Animation> ().Stop ();
//		}
		
		if (GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ParticleSystem> ().isPlaying){
			GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ParticleSystem> ().Stop ();
			GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.SetColor("_Color",Color.white);
		}
		GameObject.FindGameObjectWithTag ("PlayerMesh").renderer.enabled = true;
		if (st_LifeCnter != 0) {
			l_fBackAngel = 0;
			Vector3 pos = gameObject.transform.parent.localPosition;
			if(PlayerPrefs.GetString("PlayerSelect") == "Girl"){
				pos.y = 0.25f;
			}
			else{
				pos.y = 0.23f;
			}
			m_arrobjParent.transform.localPosition = pos;
			gameObject.transform.parent.localPosition = pos;
			m_arrobjParent.transform.localRotation = Quaternion.Euler (0, l_fBackAngel, 0);
			gameObject.transform.parent.localRotation =  Quaternion.Euler (0, l_fBackAngel, 0);
			gameObject.transform.localPosition = l_arrTrasChild[0].transform.localPosition;
			GameObject.FindWithTag("Player").transform.localPosition= Vector3.zero;
			gameObject.transform.localRotation = Quaternion.Euler (0,325.72f,0);
		}



//		else
//		{
//			GameOver();
//		}
	}

//	void SetPlayer(){
//
//			
//	}

//	public void LoadTheme()
//	{
//		m_objHoldMenu.animation.Play ("SwipeOutHoldMenu");
//		StartCoroutine ("Wait1", StartCoroutine ("Wait", m_objPauseMenu.animation ["SwipeOutHoldMenu"].length));
//		Application.LoadLevel("MainMenu");	
//	}

//	IEnumerator Wait1(float t)
//	{
//		yield return new WaitForSeconds (t);
//
//	}
	void GameOver(){
//		PlayerPrefs.SetInt ("Level", 1);
		PlayerPrefs.SetInt ("Lifes", 6);
		
		if (System.Convert.ToInt64 (PlayerPrefs.GetString ("sysString").Length) == 0) {
			PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());	
			print("Saving this date to prefs: " + System.DateTime.Now);
			print("Saving this date to prefs: " + System.DateTime.Now.ToBinary().ToString());
		}
		st_gameOver = true;
		InitSpirel.st_isPlay = false;
//		ChkAllowStart.boolAllowStart
		m_objHoldMenu.SetActive (true);
		m_objHoldMenu.animation.Play ("SwipeInHoldMenu");
		StartCoroutine("Wait",m_objHoldMenu.animation ["SwipeInHoldMenu"].length);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ().enabled = false;
//		Application.LoadLevel("GameOver");
//		UnityEditor.EditorUtility.DisplayDialog ("Life", "You Lost 1 Life!!!", "OK");
	}

	public void GamePause(){
		if (!InitSpirel.st_isPlay)
			return;
		m_objPauseMenu.SetActive (true);
		InitSpirel.st_isPlay = false;
		m_objPauseMenu.GetComponent<Animation> ().Play ();
		StartCoroutine("Wait",m_objPauseMenu.animation ["ZoomOut"].length);
//		Time.timeScale = 0f;	
	}

	public void GameResume(){
		Time.timeScale = 1f;
		InitSpirel.st_isPlay = true;
		m_objPauseMenu.SetActive (false);
	}

	IEnumerator Wait(float t){
		yield return new WaitForSeconds (t + 0.2f);
		Time.timeScale = 0f;
	}
}

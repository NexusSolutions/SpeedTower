using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class MultyPlayerController : MonoBehaviour {
	int st_LifeCnter;
	int BackCnt;
	bool st_gameOver;

	public GameObject m_arrobjParent,Player1, m_objResetPlayerBtn, m_PrfbMtlBar;
	public GameObject m_objPauseMenu,m_objHoldMenu;
	
	public TextMesh m_meshHold;
	
	Texture2D l_texLife, l_texLife_bg;
	Transform[] l_arrTrasChild = new Transform[4];
	Vector3 l_v3Pos;
	Vector2 firstPressPos, secondPressPos, currentSwipe;
	System.DateTime CurrentTime, oldDate;
	float l_fBackAngel = 8;
	string st_Times;
	Rect EUp, EDown, ELeft, ERight;
	
	public GUIStyle m_styleEmpty;
	

	public bool  IsSelf(){
		return GPGMultiplayer.getCurrentPlayerParticipantId() == GetComponentInChildren<SetPlayer>().ParticipantId ? true : false;	
	}
	
	public void SenddMessage(string Message){
		var bytes = System.Text.Encoding.UTF8.GetBytes (Message);
		GPGMultiplayer.sendUnreliableRealtimeMessageToAll( bytes );
	}
	
	void OnEnable(){
		GPGMultiplayerManager.onRealTimeMessageReceivedEvent += onRealTimeMessageReceivedEvent;
		GPGMultiplayerManager.onDisconnectedFromRoomEvent += onDisconnectedOrLeftRoom;
		GPGMultiplayerManager.onLeftRoomEvent += onDisconnectedOrLeftRoom;
	}

	void OnDisable(){
		GPGMultiplayerManager.onRealTimeMessageReceivedEvent -= onRealTimeMessageReceivedEvent;
		GPGMultiplayerManager.onDisconnectedFromRoomEvent -= onDisconnectedOrLeftRoom;
		GPGMultiplayerManager.onLeftRoomEvent -= onDisconnectedOrLeftRoom;
	}
	
	private void onRealTimeMessageReceivedEvent( string senderParticipantId, byte[] message ){
		var messageString = System.Text.Encoding.UTF8.GetString( message );

		if(messageString == "PlayerRank")      Camera.main.SendMessage("PlayerRank");
		Debug.Log ("Message  are :" +messageString);

		if(GetComponentInChildren<SetPlayer>().ParticipantId == senderParticipantId){
			Debug.Log ("Message In Fuction :" +messageString);
			
			if(messageString.StartsWith("Passbar")){
				string s = messageString.Substring(7);
				Debug.Log("Passbar = " + s);
				SendMessage("SetString2",s);
			}
			if(messageString == "GameOver"){
				Debug.Log("message recieved");
				GameOver();
			}

			if(messageString=="IamJumping")          gameObject.transform.parent.parent.GetComponent<Animation>().Play();
			else if(messageString=="IamGoingDown")   Player1.GetComponent<Animator>().Play("Down");
			else if(messageString=="IamGoingRight")  StartCoroutine("GoRight");
			else if(messageString=="IamGoingLeft")   StartCoroutine("GoLeft");

			if(messageString == "GoBack"){
				gameObject.transform.GetChild(0).GetChild(1).SendMessage("CallGoBack");
			}
		}
	}

	private void onDisconnectedOrLeftRoom(){
		if (IsSelf()) {
			SenddMessage ("GameOver");
//			GameOver();
		}
	}

	void Start(){
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		m_objResetPlayerBtn.SetActive (false);
		st_gameOver = false;
		PlayerPrefs.SetString ("sysString", null);
		st_LifeCnter = 6;
		BackCnt = 13;

		l_texLife = (Texture2D)Resources.Load ("Texture/Life") as Texture2D;
		l_texLife_bg =(Texture2D) Resources.Load ("Texture/life-bg") as Texture2D;


		for (int j = 0; j < 4; j++) {
			l_arrTrasChild [j] = m_arrobjParent.transform.GetChild(j);
		}

//		EDown = Ractangle (.01f, 0f, .14f, .19f);
//		EUp = Ractangle (.01f, .2f, .14f, .19f);
//		ELeft = Ractangle (.76f, .03f, .14f, .19f);
//		//		RRight = rectangle (.88f, .001f, .12f, .17f);
//		
//		ELeft.width = EUp.height;
//		ELeft.height = EUp.width;
//		ERight = Ractangle (.88f, .03f, .14f, .19f);
//		ERight.width = EUp.height;
//		ERight.height = EUp.width;

		EDown = Ractangle (.01f, 0f, .17f, .22f);
		EUp = Ractangle (.01f, .26f, .17f, .22f);
		ELeft = Ractangle (.74f, .03f, .17f, .22f);
		//		RRight = rectangle (.88f, .001f, .12f, .17f);
		ELeft.width = EUp.height;
		ELeft.height = EUp.width;
		ERight = Ractangle (.87f, .03f, .17f, .22f);
		ERight.width = EUp.height;
		ERight.height = EUp.width;
	}

	void Update(){
		if (!IsSelf ())     return;

		if (TowerRunManager.st_totalPlayer <= 0) {
			Debug.Log("MSG is PlayerRank");
			SenddMessage("PlayerRank");
			Camera.main.SendMessage("PlayerRank");
		} 

		if (!MultyplayerInit.st_isPlay)    return;

		foreach (Touch T in Input.touches) {
			if(T.phase == TouchPhase.Began){
				if(ELeft.Contains(T.position)){
					StartCoroutine("GoLeft");
					SenddMessage("IamGoingLeft");
				}
				else if(ERight.Contains(T.position)){
					StartCoroutine("GoRight");
					SenddMessage("IamGoingRight");
				}

				if(EUp.Contains(T.position)){
					gameObject.transform.parent.parent.GetComponent<Animation>().Play();
					SenddMessage ("IamJumping");
				}
				else if(EDown.Contains(T.position) && GameObject.Find ("JumpObject").GetComponent<Animation>().isPlaying == false){
					Player1.GetComponent<Animator>().Play("Down");
					SenddMessage("IamGoingDown");;
				}
			}
		}
	}

	void OnGUI(){
		if (!IsSelf ())     return;
		Rect R = Ractangle (0f, 0.01f, 0, 0.103f);
		R.width = 2.3f * R.height;
		R.x = (Screen.width * 0.995f) - (R.width);
		GUI.DrawTexture (R, l_texLife_bg);
		
		GUI.BeginGroup (R);
		Rect R1 = Ractangle(0.008f, 0.014f ,0, 0.072f);
		R1.width = 0.5f * R1.height;
		for(int i = 0; i < BackCnt/2 ; i++){
			GUI.DrawTexture(R1, l_texLife);
			R1.x += (R1.width); 
		}
		GUI.EndGroup ();
	}

	Rect Ractangle(float x, float y, float w, float h){
		return new Rect (Screen.width * x, Screen.height * y, Screen.width * w, Screen.height * h);
	}

	void ChkIsJumping(){
		if (gameObject.transform.parent.parent.animation.isPlaying == true){
			float TimeRemain = gameObject.transform.parent.parent.animation["Jump"].length-gameObject.transform.parent.parent.animation["Jump"].time;
			StartCoroutine(Waits(TimeRemain));
		}
		else{
			GoBack();
		}
	}

	IEnumerator Waits(float T){
		yield return new WaitForSeconds (T);
		GoBack ();
	}

	void GoBack(){
		BackCnt--;
		if (BackCnt > 1){
			BackSetp();
		}
		else{
			SenddMessage("GameOver");
			GameOver();
		}
	}

	void BackSetp(){
		l_fBackAngel += 8;
		Vector3 pos = gameObject.transform.parent.position;
		pos.y -= 0.005f;
		m_arrobjParent.transform.position = pos;
		gameObject.transform.parent.position = pos;
		m_arrobjParent.transform.localRotation = Quaternion.Euler (0, l_fBackAngel, 0);
		gameObject.transform.parent.localRotation =  Quaternion.Euler (0, l_fBackAngel, 0);
	}

	Vector3 RealPos;
	IEnumerator GoRight(){
		if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[0].transform.localPosition.z)){	
			l_v3Pos.x =  l_arrTrasChild[1].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[1].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[1].transform.localPosition.z)){
			l_v3Pos.x =  l_arrTrasChild[2].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[2].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[2].transform.localPosition.z)){
			l_v3Pos.x =  l_arrTrasChild[3].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[3].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[3].transform.localPosition.z)){
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
			l_v3Pos.x =  l_arrTrasChild[2].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[2].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[2].transform.localPosition.z)){
			l_v3Pos.x =  l_arrTrasChild[1].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[1].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[1].transform.localPosition.z)){
			l_v3Pos.x =  l_arrTrasChild[0].transform.localPosition.x;
			l_v3Pos.z =  l_arrTrasChild[0].transform.localPosition.z;
		}
		else if(FormatValue(transform.localPosition.z) == FormatValue(l_arrTrasChild[0].transform.localPosition.z)){
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

	void GameOver(){
		Debug.Log ("Gameover = " + GetComponentInChildren<SetPlayer>().ParticipantName);
		CommonS.st_listPlayerName.Add (GetComponentInChildren<SetPlayer>().ParticipantName);
		TowerRunManager.st_totalPlayer -= 1;
		Debug.Log ("TOtalPlayer:"+TowerRunManager.st_totalPlayer);
		Destroy(gameObject.transform.GetChild (0).GetChild (1).GetComponent<BoxCollider>());
		Destroy(gameObject.transform.GetChild (0).GetChild (1).GetComponent<Rigidbody>());
		Destroy (gameObject.transform.GetChild (0).GetChild (0).GetComponent<SkinnedMeshRenderer>());
	}
}
//void LateUpdate() {
	//		if (!MultyplayerInit.st_isPlay)     return;
	//		if (!IsSelf())  return;
	//		Swipe ();
	//		
	//		if (TowerRunManager.st_totalPlayer <= 0) {
	//			Debug.Log("MSG is PlayerRank");
	//			SenddMessage("PlayerRank");
	//			Camera.main.SendMessage("PlayerRank");
	//		}  
	//
	////		if (Input.GetKey(KeyCode.Escape)) {
	////			Application.Quit(); 
	////		}
	//
	//		if (Input.GetKeyDown (KeyCode.Space)) {
	//			gameObject.transform.parent.parent.GetComponent<Animation>().Play ("Jump");
	//		}
	//		else if(Input.GetKeyDown(KeyCode.DownArrow))
	//		{
	//			Player1.GetComponent<Animator>().Play("Down");
	//		}
	//		else if (Input.GetKeyDown (KeyCode.RightArrow) && gameObject.transform.parent.parent.GetComponent<Animation>().isPlaying == false) {
	//			StartCoroutine("GoRight");
	//		} 
	//		else if (Input.GetKeyDown (KeyCode.LeftArrow) && gameObject.transform.parent.parent.GetComponent<Animation>().isPlaying == false) {
	//			StartCoroutine("GoLeft");
	//		}
	//		else if  (Input.GetKeyDown (KeyCode.A)) {
	//			GoBack();
	//		}
	//	}
	//
	//	public void Swipe(){
	//		if(Input.touches.Length > 0){
	//			Touch t = Input.GetTouch(0);
	//			if(t.phase == TouchPhase.Began){
	//				firstPressPos = new Vector2(t.position.x,t.position.y);
	//			}
	//			if(t.phase == TouchPhase.Moved){
	//				secondPressPos = new Vector2(t.position.x,t.position.y);
	//				
	//				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
	//				
	//				currentSwipe.Normalize();
	//
	//
	//				if(currentSwipe.y > 0 && currentSwipe.x > -0.8f && currentSwipe.x < 0.8f){
	//					gameObject.transform.parent.parent.GetComponent<Animation>().Play();
	//					SenddMessage ("IamJumping");
	//				}
	//				else if(currentSwipe.y <0 && currentSwipe.x > -0.8f && currentSwipe.x < 0.8f && gameObject.transform.parent.parent.GetComponent<Animation>().isPlaying == false){
	//					Player1.GetComponent<Animator>().Play("Down");
	//					SenddMessage("IamGoingDown");
	//				}
	//				else if(currentSwipe.x > 0 && currentSwipe.y > -0.8f && currentSwipe.y < 0.8f ){
	//					StartCoroutine("GoRight");
	//					SenddMessage("IamGoingRight");
	//				}
	//				else if(currentSwipe.x < 0 && currentSwipe.y > -0.8f && currentSwipe.y < 0.8f )	{
	//					StartCoroutine("GoLeft");
	//					SenddMessage("IamGoingLeft");
	//				}
	//			}
	//		}
	//	}

//	void CutLife(){
//		st_LifeCnter--;
//		BackCnt = 0;
//		
//		if (st_LifeCnter != 0) {
//			m_objResetPlayerBtn.SetActive (true);
//			m_objResetPlayerBtn.GetComponent<Animation>().Play("ZoomOut");	
//			StartCoroutine("Wait",m_objResetPlayerBtn.animation ["ZoomOut"].length);
//			MultyplayerInit.st_isPlay = false;
//		}
//		else {
//			GameOver();
//		}
//	}
//
//	public  void ResetPlayer(){
//		gameObject.SendMessage ("ResetLevel");
//		m_objResetPlayerBtn.SetActive (false);
//
////		Camera.main.SendMessage ("ResetLevel");
////		Time.timeScale = 1;
////		InitSpirel.Gametime = 0f;
////		InitSpirel.CurrentLevel=1;
////		CommonS.st_IsCollider = false;
////		InitSpirel.st_isPlay = true;
////		StopCoroutine ("DoBlinks");
////		GameObject.FindGameObjectWithTag("PlayerMesh").renderer.enabled = true;
////		GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.shader=Shader.Find("Diffuse");
//
////		if(GameObject.Find ("JumpObject").GetComponent<Animation> ().isPlaying)
//		if(gameObject.transform.parent.parent.GetComponent<Animation> ().isPlaying){
////			GameObject.Find ("JumpObject").GetComponent<Animation> ().Stop ();
//			gameObject.transform.parent.parent.GetComponent<Animation> ().Stop ();
//		}
//
//		if (Player1.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).IsName("Down")) {
//			Player1.GetComponent<Animator>().Play("Run");
//		}
//
//		if (gameObject.transform.GetChild (0).GetChild (1).GetComponentInChildren<ParticleSystem> ().isPlaying) {
//			gameObject.transform.GetChild(0).GetChild(1).GetComponentInChildren<ParticleSystem> ().Stop ();
//			gameObject.transform.GetChild(0).GetChild(0).renderer.material.SetColor("_Color",Color.white);
//		}
//
////		if (GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ParticleSystem> ().isPlaying)
////		{
////			GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ParticleSystem> ().Stop ();
////			GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.SetColor("_Color",Color.white);
////		}
//
////		GameObject obj = GameObject.Find ("EndOfSpiral");
////		obj.transform.parent=null;
////		obj.transform.localPosition=new Vector3(0f,0.335f,-0.1348f);
////		obj.transform.localEulerAngles=new Vector3(-4f,85f,0f);
////		obj.transform.parent=GameObject.Find("MainPath").transform;
//
//		if(st_LifeCnter!=0){
//			l_fBackAngel = 0;
//			Vector3 pos = gameObject.transform.parent.position;
//			pos.y = 0.23f;
//			m_arrobjParent.transform.position = pos;
//			gameObject.transform.parent.position = pos;
//			m_arrobjParent.transform.localRotation = Quaternion.Euler (0, l_fBackAngel, 0);
//			gameObject.transform.parent.localRotation =  Quaternion.Euler (0, l_fBackAngel, 0);
//			gameObject.transform.position = l_arrTrasChild[0].transform.position;
//			GameObject.FindWithTag("Player").transform.position = Vector3.zero;
//			gameObject.transform.localRotation = Quaternion.Euler (0,325.72f,0);
//		}
//	}
//	public void GamePause(){
//		if (!MultyplayerInit.st_isPlay)     return;
//		m_objPauseMenu.SetActive (true);
//		MultyplayerInit.st_isPlay = false;
//		m_objPauseMenu.GetComponent<Animation> ().Play ();
//		StartCoroutine("Wait",m_objPauseMenu.animation ["ZoomOut"].length);
//	}
//
//	public void GameResume(){
//		Time.timeScale = 1f;
//		MultyplayerInit.st_isPlay = true;
//		m_objPauseMenu.SetActive (false);
//	}
//
//	IEnumerator Wait(float t){
//		yield return new WaitForSeconds (t);
//		Time.timeScale = 0f;
//	}



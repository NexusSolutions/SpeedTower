using UnityEngine;
using System.Collections;
using Prime31;


public class MenuEventManager : MonoBehaviour{
	public static bool st_IsMusicOn, boolAllowStart;

	public GameObject m_arrobjParent,txtLoading, m_objStartScreen, m_objThemeSelectScreen, m_objSettingScreen,
					m_SelectRockTheme,m_SelectElectricTheme,m_SelectIceTheme, m_SelectFireTheme, m_objPrvsBtn, m_objNextBtn, m_objBack, m_objStoreMenu;

	public Texture2D  m_settingBG,m_texAmour, m_texAmourPresed, m_texIAPAmour, m_texIAPBG,m_texIAPinvisible, 
					m_texIAPLife, m_texIAPPoints, m_texInvisible, m_texInvisiblePresed, m_texLife, 
					m_texLifePresed, m_texPoints, m_texPointsPresed;

	public Sprite m_spriteMusicOn, m_spriteMusicOff;
	public GameObject m_AudioSource;
	public AudioClip m_clipCardSide, m_ClickSound;
	public GameObject m_objHoldMenu;
	public GUIStyle m_styleEmpty;
	Texture2D[] Texture1 = new Texture2D[6];
	string[] String1 = new string[]{"1-2", "1-3", "2-1", "2-3", "3-1", "3-2"};
	bool boolChkMusic, isHold;

	string[] IAPIDs;

//	#region FB
//	private void CallFBInit(){
//		FB.Init(OnInitComplete, OnHideUnity);
//	}
//	
//	private void OnInitComplete(){
//		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
//		//		MainMenu.IsInitialized = true;
//		//		InitGame();
//	}
//	
//	private void OnHideUnity(bool isGameShown){
//		Debug.Log("Is game showing? " + isGameShown);
//	}
//
//	private void CallFBLoginShareGame()
//	{
//		Debug.Log("FB Login Call");
//		FB.Login("email,publish_actions", LoginCallbackShareGame);
//		Debug.Log("FB Login Call Out");
//	}
//	
//	private void LoginCallbackShareGame(FBResult result){
//		if (!string.IsNullOrEmpty (result.Error)) {
//			FB.Login("email,publish_actions", LoginCallbackShareGame);
//			return;
//		}
//		
//		if (FB.IsLoggedIn) {
//			FB.Feed("", "", "LinkName", "LinkCaption","LinkDescription",
//			        "https://fbcdn-photos-e-a.akamaihd.net/hphotos-ak-xpa1/t39.2081-0/p128x128/10734300_880255685332319_1626932122_n.png",
//			        "","", "","",null, null);	
//		}
//	}
//
//	private void FBShareGameCallback(FBResult result){
//		if (!string.IsNullOrEmpty (result.Error)) {
//			FB.Feed("", "", "LinkName", "LinkCaption","LinkDescription",
//			        "https://fbcdn-photos-e-a.akamaihd.net/hphotos-ak-xpa1/t39.2081-0/p128x128/10734300_880255685332319_1626932122_n.png",
//			        "","", "","",null, FBShareGameCallback);
//			return;
//		}
//	}
//           	#endregion

	void Awake(){
		st_IsMusicOn = (PlayerPrefs.GetInt ("IsMusicOn", 1) == 1) ? true : false;
		AudioListener.volume = (st_IsMusicOn) ? 1 : 0;

		IAPIDs = new string[30];
		TextAsset T = Resources.Load ("IDS") as TextAsset;
		IAPIDs = T.text.Split (System.Environment.NewLine.ToCharArray ());
	}

	void Start () {
#if UNITY_IPHONE || UNITY_ANDROID
		string Key = IAPIDs[30];
		IAP.init (Key);


		string[] androidSkus = new string[15]; //{ "com.prime31.testproduct", "android.test.purchased", "android.test.purchased2", "com.prime31.managedproduct", "com.prime31.testsubscription" };
		string[] iosProductIds = new string[15];// { "anotherProduct", "tt", "testProduct", "sevenDays", "oneMonthSubsciber" };

		for(int i = 0; i < 15; i++){
			androidSkus[i] = IAPIDs[i];
			iosProductIds[i] = IAPIDs[i + 15];
		}

		IAP.requestProductData( iosProductIds, androidSkus, productList =>
		                       {
			Debug.Log( "Product list received" );
			Utils.logObject( productList );
		});
#endif
		if(!PlayerPrefs.HasKey("IsMusicOn"))		PlayerPrefs.SetInt("IsMusicOn",1);
		if(!PlayerPrefs.HasKey("SpiralTex"))		PlayerPrefs.SetInt("SpiralTex",1);
		if(!PlayerPrefs.HasKey("sysString"))		PlayerPrefs.SetString("sysString",null);
		if(!PlayerPrefs.HasKey("Points"))			PlayerPrefs.SetInt("Points",0);
		if(!PlayerPrefs.HasKey("Armour"))			PlayerPrefs.SetInt("Armour",0);
		if(!PlayerPrefs.HasKey("Invisible"))		PlayerPrefs.SetInt("Invisible",0);
		if(!PlayerPrefs.HasKey("BuyMP"))			PlayerPrefs.SetInt("BuyMP",0);
		if(!PlayerPrefs.HasKey("PlayerSelect"))		PlayerPrefs.SetString("PlayerSelect","Boy");
		if(!PlayerPrefs.HasKey("Lifes"))			PlayerPrefs.SetInt ("Lifes", 6);
		if(!PlayerPrefs.HasKey("Theme"))			PlayerPrefs.SetInt ("Theme", 1);

		if(!PlayerPrefs.HasKey("RLevel"))			PlayerPrefs.SetInt ("RLevel", 1);
		if(!PlayerPrefs.HasKey("ELevel"))			PlayerPrefs.SetInt ("ELevel", 1);
		if(!PlayerPrefs.HasKey("ILevel"))			PlayerPrefs.SetInt ("ILevel", 1);
		if(!PlayerPrefs.HasKey("FLevel"))			PlayerPrefs.SetInt ("FLevel", 1);
		
		boolChkMusic = true;
//		CallFBInit();
//		if (PlayerPrefs.GetInt ("Lifes", 0) <= 6) {
//			PlayerPrefs.SetInt ("Lifes", 6);
//		}
		Time.timeScale = 1f;

		StartCoroutine ("Wait", 0.3f);
		if (PlayerPrefs.GetInt("SpiralTex", 1) == 1) {
			for(int i = 0 ; i < 6; i++){
				Texture1[i] = (Texture2D)Resources.Load("Rock/" + String1[i]);
				m_arrobjParent.transform.GetChild(i).transform.renderer.material.mainTexture = Texture1[i];
			}
		}
		else if(PlayerPrefs.GetInt("SpiralTex", 1) == 2) {
			for(int i = 0 ; i < 6; i++){
				Texture1[i] = (Texture2D)Resources.Load("Electric/" + String1[i]);
				m_arrobjParent.transform.GetChild(i).transform.renderer.material.mainTexture = Texture1[i];
			}	
		}
		else if(PlayerPrefs.GetInt("SpiralTex", 1) == 3) {
			for(int i = 0 ; i < 6; i++){
				Texture1[i] = (Texture2D)Resources.Load("Ice/" + String1[i]);
				m_arrobjParent.transform.GetChild(i).transform.renderer.material.mainTexture = Texture1[i];
			}
		}
		else if(PlayerPrefs.GetInt("SpiralTex", 1) == 4) {
			for(int i = 0 ; i < 6; i++){
				Texture1[i] = (Texture2D)Resources.Load("Fire/" + String1[i]);
				m_arrobjParent.transform.GetChild(i).transform.renderer.material.mainTexture = Texture1[i];
			}
		}
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	bool IsInApp = false, Islife, IsPoints, IsInvisib, IsAmour;
	enum InApp{
		Life = 0,
		Point = 1,
		amour = 2,
		invisible = 3
	}
	InApp Enum_inApp = InApp.Life;

	void OnGUI(){
		if (IsInApp) {

			m_styleEmpty.alignment = TextAnchor.MiddleRight;
			m_styleEmpty.fontSize = Screen.width/25;
			Rect RLabel = Rectangle(1f,0.05f,0,0f);
			if(Enum_inApp == InApp.Life){
				GUI.Label(RLabel,"You have " +PlayerPrefs.GetInt("Lifes") + " Life.", m_styleEmpty);
			}
			else if(Enum_inApp == InApp.Point){
				GUI.Label(RLabel,"You have " +PlayerPrefs.GetInt("Points") + " Point.", m_styleEmpty);
			}
			else if(Enum_inApp == InApp.amour){
				GUI.Label(RLabel,"You have " +PlayerPrefs.GetInt("Armour") + " Armour.", m_styleEmpty);
			}
			else if(Enum_inApp == InApp.invisible){
				GUI.Label(RLabel,"You have " +PlayerPrefs.GetInt("Invisible") + " Invisible Power.", m_styleEmpty);
			}
			Rect Rgroup = Rectangle(0f,0,0, .75f);
			Rgroup.width = 1.42f * Rgroup.height;
			Rgroup.x = (Screen.width/2) -(Rgroup.width/2);
			Rgroup.y = (Screen.height/2) - (Rgroup.height/2);
			GUI.BeginGroup(Rgroup, m_texIAPBG, m_styleEmpty);

			Rect R1 = Rectangle(0,0.005f, 0, .15f);
			R1.width = 1.76f * R1.height;
			float x = Rgroup.width - ((R1.width * 4) + (Screen.width * 0.01f));
			x= x/3;
			if(GUI.Button(R1,"", m_styleEmpty)){
				Enum_inApp = InApp.Life;
			}
			Rect R2 = R1;
			R2.x = R1.width + x;

			if(GUI.Button(R2,"", m_styleEmpty)){
				Enum_inApp = InApp.Point;
			}
			Rect R3 = R1;
			R3.x = (R1.width * 2) + (x * 2);
			if(GUI.Button(R3, "", m_styleEmpty)){
				Enum_inApp = InApp.amour;
			}

			Rect R4 = R1;
			R4.x = (R1.width * 3) + (x* 3);
			if(GUI.Button(R4,"", m_styleEmpty)){
				Enum_inApp = InApp.invisible;
			}

			Rect Rapi = Rectangle(0,R1.height,0, .45f);
			Rapi.width = 1.75f * Rapi.height;
			Rapi.x = (Rgroup.width/2) -(Rapi.width/2);
			Rapi.y = (Rgroup.height/2) - ((Rapi.height/2) + (Screen.width * 0.005f) - R1.height/2);

			Rect Rbuy = Rectangle(0,0,0, 0.08f);
			Rbuy.width = 2f * Rbuy.height;
			Rbuy.x = Rapi.width * 0.9f;
				Rbuy.y = Rapi.height * 0.5f;

			if(Enum_inApp == InApp.Life){
				GUI.DrawTexture(R1,m_texLifePresed);
				GUI.DrawTexture(R2,m_texPoints);
				GUI.DrawTexture(R3,m_texAmour);
				GUI.DrawTexture(R4,m_texInvisible);

				GUI.Label(Rapi,m_texIAPLife, m_styleEmpty);
				if(GUI.Button(Rbuy,"", m_styleEmpty )){
					BuyClick(0, 15, "Lifes", 2);
				}

				Rbuy.y = Rbuy.y + Rapi.height * 0.26f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(1, 16, "Lifes", 5);
				}

				Rbuy.y = Rbuy.y + Rapi.height * 0.26f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(2, 17, "Lifes", 11);
				}

				Rbuy.y = Rbuy.y + Rapi.height * 0.26f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(3, 18, "Lifes", 22);
				}
//				for(int i = 0; i < 4 ; i++){
//					if(GUI.Button(Rbuy,"", m_styleEmpty)){
//						print("HH");
//					}
//					Rbuy.y = Rbuy.y + Rapi.height * 0.25f;
//					
//				}
//				for(int i = 0; i < 3 ; i++){
//					if(GUI.Button(Rbuy,"", m_styleEmpty)){
//						print("HH");
//					}
//					Rbuy.y = Rbuy.y + Rapi.height * 0.33f;
//				}
			}
			else if(Enum_inApp == InApp.Point){
				
				GUI.DrawTexture(R1,m_texLife);
				GUI.DrawTexture(R2,m_texPointsPresed);
				GUI.DrawTexture(R3,m_texAmour);
				GUI.DrawTexture(R4,m_texInvisible);

				GUI.Label(Rapi,m_texIAPPoints, m_styleEmpty);
				
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(4, 19, "Points", 200);
				}
				
				Rbuy.y = Rbuy.y + Rapi.height * 0.25f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(5, 20, "Points", 500);
				}
				
				Rbuy.y = Rbuy.y + Rapi.height * 0.25f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(6, 21, "Points", 1100);
				}
				
				Rbuy.y = Rbuy.y + Rapi.height * 0.25f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(7, 22, "Points", 2300);
				}
			}
			else if(Enum_inApp == InApp.amour){
				
				GUI.DrawTexture(R1,m_texLife);
				GUI.DrawTexture(R2,m_texPoints);
				GUI.DrawTexture(R3,m_texAmourPresed);
				GUI.DrawTexture(R4,m_texInvisible);

				GUI.Label(Rapi,m_texIAPAmour, m_styleEmpty);
				Rbuy.y = Rapi.height * 0.55f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(8, 23, "Armour",2);
				}
					Rbuy.y = Rbuy.y + Rapi.height * 0.33f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(9, 24, "Armour",5);
				}
				Rbuy.y = Rbuy.y + Rapi.height * 0.33f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(10, 25, "Armour",11);
				}
			}
			else if(Enum_inApp == InApp.invisible){
				
				GUI.DrawTexture(R1,m_texLife);
				GUI.DrawTexture(R2,m_texPoints);
				GUI.DrawTexture(R3,m_texAmour);
				GUI.DrawTexture(R4,m_texInvisiblePresed);

				GUI.Label(Rapi,m_texIAPinvisible, m_styleEmpty);
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(11, 26, "Invisible",2);
				}
				Rbuy.y = Rbuy.y + Rapi.height * 0.33f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(12, 27, "Invisible",5);
				}
				Rbuy.y = Rbuy.y + Rapi.height * 0.33f;
				if(GUI.Button(Rbuy,"", m_styleEmpty)){
					BuyClick(13, 28, "Invisible",11);
				}
			}
			GUI.EndGroup();
		}
	}


	Rect Rectangle(float x, float y, float w, float h){
		return new Rect (Screen.width * x, Screen.height * y, Screen.width * w, Screen.height * h);
	}

	void BuyClick(int Android, int iOS, string playPref, int num){
		string IAPId;
		#if UNITY_ANDROID
		IAPId= IAPIDs[Android];
		#elif UNITY_IPHONE
		IAPId= IAPIDs[iOS];
		#endif
		
		if(DoIAP(IAPId)==true){
			PlayerPrefs.SetInt(playPref, PlayerPrefs.GetInt(playPref) + num);
		}
	}

	public static bool DoIAP(string IAPID){
		
		var productId = IAPID;
		bool B = false;
		#if UNITY_EDITOR && (!UNITY_IPHONE || UNITY_ANDROID)
		B = true;
		#else
		IAP.purchaseConsumableProduct( productId, ( didSucceed, error ) =>
		                                 {
			Debug.Log( "purchasing product " + productId + " result: " + didSucceed );
			
			if(didSucceed ){
				B = true;
			}
			else{ 
				Debug.Log( "purchase error: " + error );
				B = false;
			}
		});
		#endif
		return B;
	}
	void BtnClick1(string ID) {
		if (ID == "InAppBtn") {
			hideStartScreen();	
			m_objStoreMenu.SetActive(true);
			IsInApp = true;
		}
		else if(ID == "StartBtn"){
			CommonS.st_isSinglePlayer = true;
			CommonS.st_isMultyPlayer = false;
			if(isHold) m_objHoldMenu.SetActive(true);
			hideStartScreen();	
			m_objThemeSelectScreen.SetActive(true);
			m_objPrvsBtn.SetActive(false);

			GameObject.FindGameObjectWithTag("Next").animation.Play();
			GameObject.FindGameObjectWithTag("Play").animation.Play();

			m_SelectRockTheme.animation.Play();
			m_objBack.SetActive(true);
			m_SelectRockTheme.SetActive(true);
			CommonS.st_enmCrntTheme=CommonS.GameTheme.Rock;
		}
		else if(ID == "MultyBtn"){
			CommonS.st_isMultyPlayer = true;
			CommonS.st_isSinglePlayer = false;
			if(isHold) m_objHoldMenu.SetActive(true);
			if(PlayerPrefs.GetInt("BuyMP")>0)
			{
				hideStartScreen();	
				m_objThemeSelectScreen.SetActive(true);
				m_objPrvsBtn.SetActive(false);
				
				GameObject.FindGameObjectWithTag("Next").animation.Play();
				GameObject.FindGameObjectWithTag("Play").animation.Play();

				m_SelectElectricTheme.transform.position = Vector3.zero;
				m_objBack.SetActive(true);
				m_SelectElectricTheme.SetActive(true);
				m_SelectElectricTheme.animation.Play();

				CommonS.st_enmCrntTheme=CommonS.GameTheme.Electricity;
			}
		}
		else if (ID == "SettingsBtn"){
			hideStartScreen();	
			m_objSettingScreen.SetActive(true);

			GameObject.Find("MusicBtn").GetComponent<SpriteRenderer>().sprite = (st_IsMusicOn) ? m_spriteMusicOn : m_spriteMusicOff;
			GameObject.Find("MusicContainer").animation.Play("ComeInFromRight");
//			GameObject.Find("FBContainer").animation.Play("ComeInFromLeft");
		}
		else if (ID == "BackBtn"){	
			m_AudioSource.GetComponent<AudioSource>().clip = m_ClickSound;
			
			StartCoroutine("Wait", 0f);

			if(m_objSettingScreen.activeInHierarchy)     m_objSettingScreen.SetActive(false);
			if(m_objHoldMenu.activeInHierarchy)     m_objHoldMenu.SetActive(false);

			m_SelectRockTheme.SetActive(false);
			m_objThemeSelectScreen.SetActive(false);
			m_objBack.SetActive(false);
			m_SelectElectricTheme.SetActive(false);
			m_SelectIceTheme.SetActive(false);
			m_SelectFireTheme.SetActive(false);
			m_objStoreMenu.SetActive(false);
			IsInApp = false;	
		}
		else if (ID == "PlayBtn" ) {
			m_AudioSource.GetComponent<AudioSource>().clip = m_ClickSound;
			if (System.Convert.ToInt64 (PlayerPrefs.GetString ("sysString").Length) == 0){
				boolAllowStart=true;

				if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock)
					Application.LoadLevel ("SinglePlayer");
				else{
					if(CommonS.st_isSinglePlayer)     Application.LoadLevel ("SinglePlayer");
					else  Application.LoadLevel ("SignIn");
				}
			}
			else{
				boolAllowStart = false;
				m_objHoldMenu.SetActive(true);
				isHold =true;
			}
		}
		else if (ID == "PriviousBtn" &&  !isHold) {
			m_AudioSource.GetComponent<AudioSource>().clip = m_clipCardSide;

			if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity) {
				PreviousThemeSelect (m_SelectElectricTheme, m_SelectRockTheme);
				CommonS.st_enmCrntTheme = CommonS.GameTheme.Rock;
				m_objBack.SetActive(true);
				m_objPrvsBtn.SetActive (false);
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice) {
				PreviousThemeSelect (m_SelectIceTheme, m_SelectElectricTheme);
				CommonS.st_enmCrntTheme = CommonS.GameTheme.Electricity;

				if(CommonS.st_isMultyPlayer){
					m_objPrvsBtn.SetActive (false);
					m_objBack.SetActive(true);
				}
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire) {
				PreviousThemeSelect (m_SelectFireTheme, m_SelectIceTheme);
				CommonS.st_enmCrntTheme = CommonS.GameTheme.Ice;

				m_objNextBtn.SetActive (true);
				m_objNextBtn.animation.Play ();
			}
			UnlockTheme();
		} 
		else if (ID == "NextBtn" &&  !isHold) {
			m_AudioSource.GetComponent<AudioSource>().clip = m_clipCardSide;
			if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock || (CommonS.st_isMultyPlayer && CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity)) {
				if(CommonS.st_isMultyPlayer){
						m_SelectIceTheme.SetActive (true);
						NextThemeSelect (m_SelectIceTheme, m_SelectElectricTheme);
						CommonS.st_enmCrntTheme = CommonS.GameTheme.Ice;
				}     
				else {
					m_SelectElectricTheme.SetActive (true);
					NextThemeSelect (m_SelectElectricTheme, m_SelectRockTheme);
				   CommonS.st_enmCrntTheme = CommonS.GameTheme.Electricity;
				}
				m_objBack.SetActive(false);
			
				m_objPrvsBtn.SetActive (true);
				m_objPrvsBtn.animation.Play ();

					//				m_objNextBtn.SetActive(true);
					//				m_objNextBtn.animation.Play();
			} 
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity && CommonS.st_isSinglePlayer) {
					m_SelectIceTheme.SetActive (true);
					NextThemeSelect (m_SelectIceTheme, m_SelectElectricTheme);
					CommonS.st_enmCrntTheme = CommonS.GameTheme.Ice;
	
			} 
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice) {
					m_SelectFireTheme.SetActive (true);
					NextThemeSelect (m_SelectFireTheme, m_SelectIceTheme);
					CommonS.st_enmCrntTheme = CommonS.GameTheme.Fire;
	
					m_objNextBtn.SetActive (false);
			}
			UnlockTheme();
		}
		else if (ID == "MusicBtn" && boolChkMusic) {
			m_AudioSource.GetComponent<AudioSource>().clip = m_ClickSound;
			st_IsMusicOn = !st_IsMusicOn;
			
			PlayerPrefs.SetInt("IsMusicOn", (st_IsMusicOn) ? 1 : 0);
//			AudioListener.volume = ((st_IsMusicOn) ? 1 : 0);
//			GameObject.Find("MusicBtn").GetComponent<SpriteRenderer>().sprite = (st_IsMusicOn) ? m_spriteMusicOn : m_spriteMusicOff;
			StartCoroutine( SetVolume());
		}
		else if (ID == "FbBtn" ) {
			m_AudioSource.GetComponent<AudioSource>().clip = m_ClickSound;
			
//			if(!FB.IsLoggedIn){	
//				CallFBLoginShareGame();
//			}
//			else{
//				FB.Feed("", "", "LinkName", "LinkCaption","LinkDescription",
//				        "https://fbcdn-photos-e-a.akamaihd.net/hphotos-ak-xpa1/t39.2081-0/p128x128/10734300_880255685332319_1626932122_n.png",
//				        "","", "","",null, FBShareGameCallback);	
//			}	
		}
		else if(ID == "PlayerSelect"){
			Application.LoadLevel("PlayerSelect");
		}
		m_AudioSource.GetComponent<AudioSource>().Play();
	}

	void UnlockTheme(){
		if (m_objThemeSelectScreen.activeInHierarchy) {
			if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock){
				m_objThemeSelectScreen.transform.GetChild(0).gameObject.SetActive(true);
			}	
			else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
				if(PlayerPrefs.GetInt("Theme", 1) >= 2) m_objThemeSelectScreen.transform.GetChild(0).gameObject.SetActive(true);
				else  m_objThemeSelectScreen.transform.GetChild(0).gameObject.SetActive(false);                             
			}
			else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
				if(PlayerPrefs.GetInt("Theme", 1) >= 3) m_objThemeSelectScreen.transform.GetChild(0).gameObject.SetActive(true);
				else  m_objThemeSelectScreen.transform.GetChild(0).gameObject.SetActive(false);   
			}
			else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
				if(PlayerPrefs.GetInt("Theme", 1) >= 4) m_objThemeSelectScreen.transform.GetChild(0).gameObject.SetActive(true);
				else  m_objThemeSelectScreen.transform.GetChild(0).gameObject.SetActive(false);   
			}		
		}
	}

	void hideStartScreen(){
		m_AudioSource.GetComponent<AudioSource>().clip = m_clipCardSide;
		m_objStartScreen.SetActive (false);
	}

	IEnumerator Wait(float Seconds){
		yield return new WaitForSeconds (Seconds);
		txtLoading.SetActive (false);
		m_objStartScreen.SetActive (true);

		m_AudioSource.GetComponent<AudioSource>().clip = m_clipCardSide;
		m_AudioSource.GetComponent<AudioSource>().Play();
	}

	void NextThemeSelect(GameObject objShow, GameObject objHide){
		objShow.animation["LeftMove_Theme"].time = 0;
		objShow.animation["LeftMove_Theme"].speed = 1f;
		objShow.animation.Play("LeftMove_Theme");
		
		objHide.animation["RightMove_Theme"].time = 0f;
		objHide.animation["RightMove_Theme"].speed = 1f;
		objHide.animation.Play("RightMove_Theme");
	}
		
	void PreviousThemeSelect(GameObject objHide, GameObject objShow){
		objHide.animation["LeftMove_Theme"].time = 0.5f;
		objHide.animation["LeftMove_Theme"].speed = -1f;
		objHide.animation.Play("LeftMove_Theme");
		
		objShow.animation["RightMove_Theme"].time = 0.5f;
		objShow.animation["RightMove_Theme"].speed = -1f;
		objShow.animation.Play("RightMove_Theme");
	}

	IEnumerator SetVolume(){
		boolChkMusic = false;
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
		GameObject.Find("MusicBtn").GetComponent<SpriteRenderer>().sprite = (st_IsMusicOn) ? m_spriteMusicOn : m_spriteMusicOff;
		boolChkMusic = true;
	}
}

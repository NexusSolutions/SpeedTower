using UnityEngine;
using System.Collections;
using Prime31;
public class BuyMP : MonoBehaviour {
	public GUIStyle m_styleEmpty;
	public Texture m_texBG,m_texBuyMP;
	// Use this for initialization
	void Awake(){
//		PlayerPrefs.DeleteAll ();
		DontDestroyOnLoad (this);
	}

	void OnGUI(){
		if(CommonS.st_isMultyPlayer && PlayerPrefs.GetInt("BuyMP")<=0)
		{
			Time.timeScale = 0;
			Rect R=Ractangle(0,0,1,1);
			GUI.DrawTexture(R,m_texBG);

			R = Ractangle(0,0,0,.55f);
			R.width = 1.44f * R.height;
			R.x = (Screen.width/2)-(R.width/2);
			R.y = (Screen.height/2) - (R.height/2);
			GUI.DrawTexture (R, m_texBuyMP);

			R=Ractangle(0.24f,0.585f,0.2f,0.13f);
			if(GUI.Button(R,"",m_styleEmpty)){ //cancel
				CommonS.st_isMultyPlayer=false;
				Time.timeScale = 1;
			}

			R=Ractangle(0.55f,0.585f,0.21f,0.13f);
			if(GUI.Button(R,"",m_styleEmpty)){ //buy
				print("buy");
				string IAPId;
				TextAsset T = Resources.Load("IDS") as TextAsset;
				#if UNITY_ANDROID
				IAPId= T.text.Split(System.Environment.NewLine.ToCharArray())[14];
				#elif UNITY_IPHONE
				IAPId=T.text.Split(System.Environment.NewLine.ToCharArray())[29];
				#endif
				
				if(DoIAP(IAPId)==true){
					PlayerPrefs.SetInt("BuyMP", PlayerPrefs.GetInt("BuyMP") + 2);
					Application.LoadLevel("MainMenu");
					print(PlayerPrefs.GetInt("BuyMP"));
					Time.timeScale = 1;
				}
			}
		}

	}

	public static bool DoIAP(string IAPID){
		
		var productId = IAPID;
		bool B = false;
		#if UNITY_EDITOR && (!UNITY_IPHONE || UNITY_ANDROID)
		B = true;
		#else
		IAP.purchaseNonconsumableProduct( productId, ( didSucceed, error ) =>
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
	Rect Ractangle(float x, float y, float w, float h){
		return new Rect (Screen.width * x, Screen.height * y, Screen.width * w, Screen.height * h);
	}

}

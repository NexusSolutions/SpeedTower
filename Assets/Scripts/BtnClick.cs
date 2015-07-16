using UnityEngine;
using System.Collections;

public class BtnClick : MonoBehaviour {
	public static bool st_boolChkMusic;
	AudioClip ClickClip;
	public GameObject BoySelect, GirlSelect;
	void Start () {
		ClickClip = Resources.Load ("Clip/ClickSound") as AudioClip;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		st_boolChkMusic = true;

		if(gameObject.name == "Boy"){
			if(PlayerPrefs.GetString("PlayerSelect") == "Girl"){
				BoySelect.SetActive(false);
				GirlSelect.SetActive(true);
			}
			else{
				BoySelect.SetActive(true);
				GirlSelect.SetActive(false);
			}
		}
	}

	void OnMouseDown() {
		GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().clip = ClickClip;
		GameObject.FindWithTag("ClickSound").GetComponent<AudioSource> ().Play ();

		if (gameObject.name == "ResetPlayer") {
			Camera.main.SendMessage ("ResetLevel");
			GameObject.Find("PlayerContainer").SendMessage("ResetPlayer");	
		}
		else if (gameObject.name == "MainMenu") {
			Application.LoadLevel("MainMenu");	
		}
		else if (gameObject.name == "Resume") {
			GameObject.Find("PlayerContainer").SendMessage("GameResume");	
		}
		else if (gameObject.name == "ResumeLifeAdd") {
			Camera.main.SendMessage("ResumeLifeAdd");	
		}
		else if (gameObject.name == "Restart") {
			Application.LoadLevel(Application.loadedLevel);	
		}
		else if (gameObject.name == "Ok") {
			Application.LoadLevel("MainMenu");	
		}
		else if(gameObject.name == "BackBtn") {
			print("form SignIn BackBtn");
			Application.LoadLevel("MainMenu");
		}
		else if(gameObject.name == "NextTheme") {
			if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock) {
				CommonS.st_enmCrntTheme = CommonS.GameTheme.Electricity;
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
				CommonS.st_enmCrntTheme = CommonS.GameTheme.Ice;
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
				CommonS.st_enmCrntTheme = CommonS.GameTheme.Fire;
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
				CommonS.st_enmCrntTheme = CommonS.GameTheme.Rock;
			}
			Application.LoadLevel("SinglePlayer");
		}
//		else if(gameObject.name == "ContinueBtn"){
//			CommonS.st_iCurrentLevel++;
//			Application.LoadLevel("SinglePlayer");
//		}
		else if(gameObject.name == "Boy") {
			PlayerPrefs.SetString("PlayerSelect","Boy");
			BoySelect.SetActive(true);
			GirlSelect.SetActive(false);
		}
		else if(gameObject.name == "Girl") {
			PlayerPrefs.SetString("PlayerSelect","Girl");
			BoySelect.SetActive(false);
			GirlSelect.SetActive(true);
		}
	}
}

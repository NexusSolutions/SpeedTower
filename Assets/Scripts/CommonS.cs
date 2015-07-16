using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CommonS : MonoBehaviour {
//	public static int BackCnt=0;//,NoOfSpilePassed=0; LifeCnter=1, CurrentLevel=1
	public static bool st_isPopUp,_isGameInProgress ;
	public static List<string> st_listPlayerName = new List<string>();
	public static List<string> st_listPlayerPoints = new List<string>();
	public static List<string> st_SelectList = new List<string>();
	
	public static string st_RoomCreatorID;
//	public static float st_LifeCnter = 6;
	
	public static float st_fSpirelHeight = 0.19016f;
//	public static float st_fSpirelPosY = 0.34f;
//	public static Vector3 PosMtlBarDwn=new Vector3(0.13f,0.22f,0.009f),PosMtlBarUp=new Vector3(0.13f,0.251f,0.009f);
	public static Vector3 PosMtlBarDwn=new Vector3(0.13f,0.25f,0.009f),PosMtlBarUp=new Vector3(0.13f,0.284f,0.009f);
//	public static Vector3 PosMtlBarDwn=new Vector3(0.0735f,0.25f,0.1067f),PosMtlBarUp=new Vector3(0.0735f,0.28f,0.1067f);
	
	public static bool st_isHold, st_isSinglePlayer, st_isMultyPlayer;
//	public static float st_fRotateMainspeed = 0f;
	
	public static bool st_IsCollider = false;
	public static bool isRoomConnected;
	public enum GameTheme{
		None = 0,
		Rock = 1,
		Electricity = 2,
		Ice = 3,
		Fire = 4
	}
	
	public static GameTheme st_enmCrntTheme =GameTheme.None;
}

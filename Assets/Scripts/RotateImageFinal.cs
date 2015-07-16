using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotateImageFinal : MonoBehaviour {
	public static float st_fYoffset;
	public GameObject Tower1, Tower2;
	//	float Yoffset = 0f,Yoffset1=0f ,Yoffset2=0, Yoffset3 = 0 ;
	
//	public Texture[] EleTex;
	Texture[] Texture1 = new Texture[6];
	string[] String1 = new string[]{"1-2", "1-3", "2-1", "2-3", "3-1", "3-2"};
	public static string strTower1TexName, strTower2TexName, strTower3TexName;
	
	public static float st_fRotateMainspeed = 0.2f;
	float t = 0f, Mainspeed;
	float F = 0f,R = 0f;
	float F1 = 0, F2 = 360, R1, R2;
	float SpirleHeight;
//	public static float OffsetSpd = 0.001f;
	
	
	void Start () {

		if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock) {
			for(int i = 0 ; i < 6; i++){
				Texture1[i] = (Texture)Resources.Load("Rock/" + String1[i]) as Texture;
			}	
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
			for(int i = 0 ; i < 6; i++){
				Texture1[i] = (Texture)Resources.Load("Electric/" + String1[i])as Texture;
			}
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
			for(int i = 0 ; i < 6; i++){
				Texture1[i] = (Texture)Resources.Load("Ice/" + String1[i])as Texture;
			}
		}
		else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
			for(int i = 0 ; i < 6; i++){
				Texture1[i] = (Texture)Resources.Load("Fire/" + String1[i])as Texture;
			}
		}

		if(name == "MainPath"){
//			OffsetSpd = 0.001f;
			t = 0f;
			SpirleHeight = CommonS.st_fSpirelHeight;
			R1 = transform.position.y;
			R2 = R1 - SpirleHeight;
			F = R = 0;
			F1 = 0;
			F2 = 360;
		}
		else{
			setTowerTex();
			setTower2Tex();
			st_fYoffset = 0f;
		}
	}
	
	
	void Update () {

		if (CommonS.st_isSinglePlayer) {
			if(!InitSpirel.st_isPlay)	return;
		}
		else{
			if(!MultyplayerInit.st_isPlay)	return;
		}
		
		if(name == "MainPath"){
			RotatePath();
		}
		else{
			ChangeOffset();
		}
	}
	
	void ChangeOffset(){
		Tower1.renderer.material.mainTextureOffset = new Vector2 (0, st_fYoffset);
		Tower2.renderer.material.mainTextureOffset = new Vector2 (0, st_fYoffset);
		
		st_fYoffset -= (0.01f * st_fRotateMainspeed);
//		st_fYoffset -=  ((Time.deltaTime) * st_fRotateMainspeed);
		
//		st_fYoffset -= (st_fRotateMainspeed/2);
		
		
		if (st_fYoffset <= -1f) {
			st_fYoffset = 0;
			setTower1Tex1 ();
			setTower2Tex ();
		}
	}
	
	void RotatePath(){
		R = Mathf.Lerp (R1, R2,t);
		F = Mathf.Lerp (F1, F2, t);
		if (R <= R2) {
			t = 0;
			R1 = R2;
			R2 -= SpirleHeight;
			F1 = 0;
			F2 = 360;		
		} 
		if (t < 1) {
			t += ((Time.deltaTime) * st_fRotateMainspeed);
		}
		transform.position = new Vector3 (0, R, 0);
		transform.localRotation =  Quaternion.Euler (0, F, 0);	
	}
	
	void setTowerTex(){	
		int TexIndex=Random.Range (0, 6);
		Tower1.renderer.material.mainTexture = Texture1 [TexIndex];

		RotateImageFinal.strTower1TexName = Tower1.renderer.material.mainTexture.name;
	}
	
	void setTower1Tex1(){	
		
		List<Texture> MatchingTex=new List<Texture>();
		string texName="";
		
		texName = RotateImageFinal.strTower1TexName;
		string s = texName.Substring(RotateImageFinal.strTower1TexName.Length-1,1);

		foreach(Texture t in Texture1)
		{
			if(t.name.EndsWith(s))
				MatchingTex.Add(t);
		}		

		Tower1.renderer.material.mainTexture = MatchingTex[Random.Range(0,MatchingTex.Count)];
		
		RotateImageFinal.strTower1TexName = Tower1.renderer.material.mainTexture.name;
	}
	
	void setTower2Tex(){	
		List<Texture> MatchingTex=new List<Texture>();
		string texName="";
		
		texName = RotateImageFinal.strTower1TexName;
		string s = texName.Substring(RotateImageFinal.strTower1TexName.Length-1,1);

		foreach(Texture t in Texture1)
		{
			if(t.name.StartsWith(s))
				MatchingTex.Add(t);
		}		
	
		Tower2.renderer.material.mainTexture = MatchingTex[Random.Range(0,MatchingTex.Count)];
		
		RotateImageFinal.strTower2TexName = Tower2.renderer.material.mainTexture.name;
		
	}
}




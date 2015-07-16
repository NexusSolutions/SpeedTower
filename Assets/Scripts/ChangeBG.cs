using UnityEngine;
using System.Collections;

public class ChangeBG : MonoBehaviour {
	void Start () {
		if(name == "MainBG"){
			Texture l_texBG;	
			if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Rock) {
				l_texBG = (Texture2D)Resources.Load("Background/RockBG");
				transform.renderer.material.mainTexture  = l_texBG;	
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
				l_texBG = (Texture2D)Resources.Load("Background/ElectricBG");
				transform.renderer.material.mainTexture  = l_texBG;	
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
				l_texBG = (Texture2D)Resources.Load("Background/IceBG");
				transform.renderer.material.mainTexture  = l_texBG;	
			}
			else if (CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
				l_texBG = (Texture2D)Resources.Load("Background/FireBG");
				transform.renderer.material.mainTexture  = l_texBG;	
			}
		}

		if (name == "LightSnowFinal") {
			ParticleAnimator particleAnimator = GetComponent<ParticleAnimator>();
			Color[] modifiedColors = particleAnimator.colorAnimation;

			if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity){
					modifiedColors[0] = Color.blue;
					modifiedColors[1] = Color.blue;
					modifiedColors[2] =Color.cyan;
					modifiedColors[3] =Color.blue;
					modifiedColors[4] = Color.black;
//				modifiedColors[0] = Color.black;
//				modifiedColors[1] = Color.grey;
//				modifiedColors[2] =Color.yellow;
//				modifiedColors[3] =Color.grey;
//				modifiedColors[4] = Color.black;
			}
			else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice){
				for(int i=0; i < 5; i++){
					modifiedColors[i] = Color.white;
				}
			}
			else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire){
				modifiedColors[0] = Color.yellow;
				modifiedColors[1] = Color.yellow;
				modifiedColors[2] =Color.red;
				modifiedColors[3] =Color.yellow;
				modifiedColors[4] = Color.black;
			}
			particleAnimator.colorAnimation = modifiedColors;
		}
	}
}

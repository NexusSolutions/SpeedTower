using UnityEngine;
using System.Collections;

public class Align : MonoBehaviour {
	
	public float HoriZDistance;
	public float VerticleDistance;
	public bool Left;
	public bool Top;
	public bool Hcenter;
	public bool Vcenter;
	void Awake () {
		Vector3 L,R,T,B;
		L = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		R = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
		T=Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
		B=Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 Pos = transform.position;
		if(Left){
			Pos.x = L.x + HoriZDistance;
		}
		else{
			Pos.x = R.x - HoriZDistance;
		}

		if(Top){
			Pos.y = T.y - VerticleDistance;
		}
		else{
			Pos.y = B.y + VerticleDistance;
		}
		if (Hcenter) {
			Pos.x = 0;		
		}
		if (Vcenter) {
			Pos.y = 0;		
		}
		transform.position = Pos;
	}
}

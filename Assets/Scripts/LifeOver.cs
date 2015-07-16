using UnityEngine;
using System.Collections;

public class LifeOver : MonoBehaviour {
	string st_Times;
	public TextMesh m_meshHold;
	System.DateTime CurrentTime, oldDate;

	void Satrt(){
		m_meshHold.text = "1" ;
	}

	void Update(){
		if (System.Convert.ToInt64 (PlayerPrefs.GetString ("sysString").Length) == 0) {
			MenuEventManager.boolAllowStart = true;
		} 
		else {
			CurrentTime = System.DateTime.Now;
			long temp = System.Convert.ToInt64 (PlayerPrefs.GetString ("sysString"), null);
			oldDate = System.DateTime.FromBinary (temp);
			System.TimeSpan difference = CurrentTime.Subtract (oldDate);
			int RHour = (1 - difference.Hours);
			if (RHour < 0) {
				RHour  = 0;	
			}
			//		int RMinuts = (1 - difference.Minutes);
			//		if (RMinuts < 0) {
			//			RMinuts  = 0;	
			//		}
			
			
			st_Times = (RHour) + ":" + (60 - difference.Minutes)  + ":" + (60 - difference.Seconds);

			if (difference.Hours >= 1) {
				PlayerPrefs.SetString ("sysString", null);
				MenuEventManager.boolAllowStart = true;

				Camera.main.SendMessage ("BtnClick1", "PlayBtn");
			} 
			else {
				MenuEventManager.boolAllowStart = false;
			}

			m_meshHold.text = st_Times + "";
		}
	}
}

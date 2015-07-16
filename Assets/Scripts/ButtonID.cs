using UnityEngine;
using System.Collections;

public class ButtonID: MonoBehaviour {
	
	public string id;

	void OnMouseDown() {
		if (Application.loadedLevelName == "MainMenu") {
			Camera.main.SendMessage("BtnClick1", id);
		}
	}
}


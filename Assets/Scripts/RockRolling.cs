using UnityEngine;
using System.Collections;

public class RockRolling : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (tag == "Log") {
			rigidbody.AddTorque (1.5f, 0f, 0f);
		}
		else
		{
			rigidbody.AddTorque (2, 0, 2);
		}
	}
}

using UnityEngine;
using System.Collections;

public class RotateTower : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation =  Quaternion.Euler (0, Time.time  * 60, 0);	
	}
}

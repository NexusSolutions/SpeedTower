using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public AudioSource m_AudioSource;
	AudioClip m_clipLog, m_clipRock, m_clipCollide, m_clipBeam, m_clipSlipperyIce, m_clipWaterDrop, m_clipMetalBar, m_clipCracker;
	public static bool IsFreezed = false;
	bool isSetArmour =true;
	void Start()
	{
		isSetArmour = true;
		if (tag == "Player"){
			m_clipCollide = Resources.Load ("Clip/Collider")as AudioClip;
			m_clipSlipperyIce = Resources.Load ("Clip/SlipperyIce")as AudioClip;
			m_clipWaterDrop = Resources.Load ("Clip/WaterDrop")as AudioClip;
			m_clipMetalBar = Resources.Load ("Clip/MatelBar")as AudioClip;
			m_clipCracker = Resources.Load("Clip/firework")	as AudioClip;
		}
//		else if(name=="RockDetection")
//		{
//			m_clipLog = Resources.Load ("Clip/Log")as AudioClip;
//			m_clipRock = Resources.Load ("Clip/Rock")as AudioClip;
//		}
		else if(tag=="Beam"){
			m_clipBeam = Resources.Load ("Clip/Beam")as AudioClip;	
		}
	}

	void OnCollisionEnter(Collision collision) {
		if(!CommonS.st_IsCollider)
		{
			if(InitSpirel.st_isArmour && isSetArmour){
				StartCoroutine(ResetArmor());
			}
			else{
				if (collision.gameObject.tag == "MetalBar") {
						m_AudioSource.GetComponent<AudioSource>().clip = m_clipMetalBar;
						m_AudioSource.GetComponent<AudioSource>().Play();
						
//						StartCoroutine(CallGoBack(1));
						
						if(InitSpirel.IsTrigger) StartCoroutine(CallGoBack(1));
						else StartCoroutine(CallGoBack(0));
				}
				else if (collision.gameObject.tag == "Beam" ){
					GameObject.FindWithTag("ClickSound").GetComponent<AudioSource>().clip = m_clipBeam;
					GameObject.FindWithTag("ClickSound").GetComponent<AudioSource>().Play();
					if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice)
					{
						StartCoroutine("CallGoBack",1);
						StartCoroutine("Waitss");
						
						GetComponent<Animator>().Play("Freeze");
						IsFreezed=true;
					}
					else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire)
					{
						StartCoroutine("CallGoBack",1);	
						StartCoroutine("Waitss");
						
						gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
						GameObject.FindGameObjectWithTag ("PlayerMesh").renderer.material.SetColor ("_Color", Color.black);
					}
					else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity)
					{
						gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
						GameObject.FindGameObjectWithTag ("PlayerMesh").renderer.material.SetColor ("_Color", Color.black);
//										StartCoroutine("CallGoBack",4);
						
						if(InitSpirel.IsTrigger)      StartCoroutine(CallGoBack(3));
						else                          StartCoroutine(CallGoBack(0));
					}
				}

				if (collision.gameObject.tag == "Cracker") {

					m_AudioSource.GetComponent<AudioSource>().clip = m_clipCracker;
					m_AudioSource.GetComponent<AudioSource>().Play();

					GameObject.Find("crackerPS").particleSystem.Play();
					collision.gameObject.SetActive(false);

					if(InitSpirel.IsTrigger) StartCoroutine(Waitss());
					else StartCoroutine(CallGoBack(0));
				}
				else if(collision.gameObject.tag == "Rock"){
					m_AudioSource.GetComponent<AudioSource>().clip = m_clipCollide;
					m_AudioSource.GetComponent<AudioSource>().Play();

//					StartCoroutine(CallGoBack(3));

					if(InitSpirel.IsTrigger) StartCoroutine(CallGoBack(2));
					else StartCoroutine(CallGoBack(0));
				}
				else if (collision.gameObject.tag == "Log" ) {
					m_AudioSource.GetComponent<AudioSource>().clip = m_clipCollide;
					m_AudioSource.GetComponent<AudioSource>().Play();

//					StartCoroutine(CallGoBack(2));

					if(InitSpirel.IsTrigger) StartCoroutine(CallGoBack(2));
					else StartCoroutine(CallGoBack(0));
				}
				else  if (collision.gameObject.tag == "WaterDrop" ){
					m_AudioSource.GetComponent<AudioSource>().clip = m_clipWaterDrop;
					m_AudioSource.GetComponent<AudioSource>().Play();

					gameObject.GetComponentInChildren<ParticleSystem>().Play();

					GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.SetColor("_Color",Color.black);
//					StartCoroutine(CallGoBack(2));

					if(InitSpirel.IsTrigger) StartCoroutine(CallGoBack(2));
					else StartCoroutine(CallGoBack(0));
				}
				else if (collision.gameObject.tag == "SlipperyIce" ){
					m_AudioSource.GetComponent<AudioSource>().clip = m_clipSlipperyIce;
					m_AudioSource.GetComponent<AudioSource>().Play();

					GetComponent<Animator>().Play("IceSlips");
				}
			}
		}
//		if(name == "RockDetection")
//		{
//			if (collision.gameObject.tag == "Rock" ||  collision.gameObject.tag == "Log") {
//				if(collision.gameObject.tag == "Rock"){
//
//					GetComponent<AudioSource>().clip = m_clipRock;
//					GetComponent<AudioSource>().Play();
//				}
//				else{
//					audio.clip = m_clipLog;
//					audio.Play();
//				}
//
//				collision.gameObject.transform.parent = GameObject.Find("MainPath").transform;
//				collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
//				collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
//			}
//		}
	}

	IEnumerator CallGoBack(int NumofTimes){
//		Handheld.Vibrate ();
		CommonS.st_IsCollider = true;
		StartCoroutine (DoBlinks());
//		GameObject.Find("PlayerContainer").SendMessage("DoBlinks");
		for (int i = 0; i < NumofTimes; i++) {
			GameObject.Find("PlayerContainer").SendMessage("ChkIsJumping");
		}
		yield return new WaitForSeconds (0.5f);
		CommonS.st_IsCollider = false;

		if(GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.GetColor("_Color") !=  Color.red){
			GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.SetColor("_Color", Color.white);
		}
	}

	public IEnumerator DoBlinks() {
		float duration = 0.2f;
		while (duration > 0f)  {
			duration -= Time.deltaTime;
			GameObject.FindGameObjectWithTag("PlayerMesh").renderer.enabled= !GameObject.FindGameObjectWithTag("PlayerMesh").renderer.enabled;
			yield return new WaitForSeconds(0.05f);
		}
		if(InitSpirel.st_isInvisible)    GameObject.FindGameObjectWithTag("PlayerMesh").renderer.enabled = false;
		else                             GameObject.FindGameObjectWithTag("PlayerMesh").renderer.enabled = true;
	}

	IEnumerator ResetArmor(){
		isSetArmour = false;
		CommonS.st_IsCollider = true;
		yield return new WaitForSeconds(3f);
		GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.SetColor("_Color",Color.white);
		InitSpirel.st_isArmour = false;
		CommonS.st_IsCollider = false;
		isSetArmour = true;
	}
	
	IEnumerator Waitss(){
		yield return new WaitForSeconds (0.5f);
		GameObject.Find("PlayerContainer").SendMessage("CutLife");	
	}
	
	void OnExitFreeze(){
		IsFreezed = false;
	}

//	void OnTriggerEnter(Collider c){
//		if(name=="EndOfSpiral"){
//			if(c.gameObject.tag=="Player"){
//				InitSpirel.NoOfSpilePassed++;
//				gameObject.transform.parent=null;
//				gameObject.transform.localPosition=new Vector3(0f,0.335f,-0.1348f);
//				gameObject.transform.localEulerAngles=new Vector3(-4f,85f,0f);
//				gameObject.transform.parent=GameObject.Find("MainPath").transform;
//			
//				if(InitSpirel.NoOfSpilePassed == 9){
//					Camera.main.SendMessage("ChangeLevel");
//				}
//			}
//		}
//	}

//	void OnParticleCollision(GameObject other) {
//		if(InitSpirel.st_isArmour && isSetArmour && !CommonS.st_IsCollider){
//			StartCoroutine(ResetArmor());
//		}
//		else{
//		if (other.gameObject.tag == "Player" && !CommonS.st_IsCollider) {
////				CommonS.st_IsCollider = true;
//				GameObject.FindWithTag("ClickSound").GetComponent<AudioSource>().clip = m_clipBeam;
//				GameObject.FindWithTag("ClickSound").GetComponent<AudioSource>().Play();
//				if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Ice)
//				{
//					print("Hello");
//					StartCoroutine("CallGoBack",1);
//					StartCoroutine("Waitss");
//
//	//				GameObject.FindGameObjectWithTag("PlayerMesh").renderer.material.shader=Shader.Find("Reflective/Specular");
//					other.GetComponent<Animator>().Play("Freeze");
//					IsFreezed=true;
//				}
//				if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Fire)
//				{
//					StartCoroutine("CallGoBack",1);	
//					StartCoroutine("Waitss");
//					
//					other.gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
//					GameObject.FindGameObjectWithTag ("PlayerMesh").renderer.material.SetColor ("_Color", Color.black);
//				}
//				else if(CommonS.st_enmCrntTheme == CommonS.GameTheme.Electricity)
//				{
//					other.gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
//					GameObject.FindGameObjectWithTag ("PlayerMesh").renderer.material.SetColor ("_Color", Color.black);
//	//				StartCoroutine("CallGoBack",4);
//					 
//					if(InitSpirel.IsTrigger)      StartCoroutine(CallGoBack(2));
//					else                          StartCoroutine(CallGoBack(0));
//				}
//			}
//		}
//	}
}
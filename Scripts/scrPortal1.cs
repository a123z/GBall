using UnityEngine;
using System.Collections;

public class scrPortal1 : MonoBehaviour {
	float rotateTime;
	bool _ballToStart=true;
	Vector3 deltaPoint = new Vector3(0,0.5f,0);

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody>().maxAngularVelocity = 20f;

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if (rotateTime>-1f){
			gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0,50,0,ForceMode.Acceleration);
			rotateTime-=Time.fixedDeltaTime;
			if (rotateTime<0){
				rotateTime = -1f;
				if (_ballToStart) GameObject.Find("Ball").GetComponent<BallScript>().GoToStart(gameObject.transform.position-deltaPoint);
				gameObject.GetComponent<ParticleSystem>().Play();
				
			}
		}
	}

	public void RunTeleport(bool BallToStart=true){
		rotateTime = 1f;//1 sec
		_ballToStart = BallToStart;
		//тихо!!! gameObject.GetComponent<AudioSource>().Play();
	}
}

using UnityEngine;
using System.Collections;

public class scrPortal1 : MonoBehaviour {
	public float NormalRotateTime=1f;
	float _rotateTime;
	bool _ballToStart=true;
	Vector3 _deltaPoint = new Vector3(0,0.5f,0);
	Vector3 _rotateForce = new Vector3(0,50f,0);
	string _ballObjectName="Ball";

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody>().maxAngularVelocity = 20f;

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if (_rotateTime>-1f){
			gameObject.GetComponent<Rigidbody>().AddRelativeTorque(_rotateForce,ForceMode.Acceleration);
			_rotateTime-=Time.fixedDeltaTime;
			if (_rotateTime<0){
				_rotateTime = -1f;
				if (_ballToStart) GameObject.Find(_ballObjectName).GetComponent<BallScript>().GoToStart(gameObject.transform.position-_deltaPoint);
				gameObject.GetComponent<ParticleSystem>().Play();
				
			}
		}
	}

	public void RunTeleport(bool BallToStart=true){
		_rotateTime = NormalRotateTime;//1 sec
		_ballToStart = BallToStart;
		//тихо!!! gameObject.GetComponent<AudioSource>().Play();
	}
}

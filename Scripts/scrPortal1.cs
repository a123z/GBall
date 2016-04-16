using UnityEngine;
using System.Collections;

public class scrPortal1 : MonoBehaviour {
	public float NormalRotateTime=1f;
	//public bool teleportRun=false;
	float _rotateTime;
	//bool _ballToStart=true;
	Vector3 _deltaPoint = new Vector3(0,0.5f,0);
	Vector3 _rotateForce = new Vector3(0,50f,0);
	//string _ballObjectName="Ball";
	GameObject ball;
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
				GoToStart(gameObject.transform.position-_deltaPoint);
				gameObject.GetComponent<ParticleSystem>().Play();
				if (ball!=null) ball.GetComponent<BallScript>().ResetAfterTeleport(); //сбросили таймер полёта
			}
		}
	}

	public bool RunTeleport(GameObject aBall){
		if (ball != null) return(false);

		_rotateTime = NormalRotateTime;//1 sec
		ball = aBall;
		
        //тихо!!! 
        gameObject.GetComponent<AudioSource>().Play();
        return (true);
    }

	void GoToStart(Vector3 newPos){
		if (ball==null) return;
		ball.transform.position = newPos;
		//teleportRun = false;
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		ball.GetComponent<ConstantForce>().force = Vector3.zero;
		ball.GetComponent<BallScript>().teleportRun = false;
		ball = null;
		//Debug.Log(string.Format("v={0} av={1}",gameObject.GetComponent<Rigidbody>().velocity,gameObject.GetComponent<Rigidbody>().angularVelocity));
	}
}

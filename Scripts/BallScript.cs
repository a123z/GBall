using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	//public GameObject PortalPrefub;
	public bool teleportRun=false;

	const float G=6.67545E-11f;
	const float m1=20f;
	//int i=0;
	GameObject basket;
	GameObject portal;
	Vector3 tV3;
	int BasketPass=0;
	float WaitPass=0;

	// Use this for initialization
	void Start () {
		basket = GameObject.FindGameObjectWithTag("basket");
		portal = GameObject.FindGameObjectWithTag("portal");
	}
	
	// Update is called once per frame
	void Update () {


	}

	void FixedUpdate () {
		GameObject[] GObjs;
		GObjs = GameObject.FindGameObjectsWithTag("points");
		Vector3 Grav=Vector3.zero;
		foreach (GameObject GObj in GObjs) {
			tV3 = GetGravity(-transform.position+GObj.transform.position, GObj.GetComponent<pointScript>().gravity);
			if (tV3.sqrMagnitude>1){
			//if ((GObj.transform.position-transform.position).sqrMagnitude<200){
				Grav += tV3;
			}
		}
		//Debug.Log(string.Format("deltatime={0} grav={1}",Time.fixedDeltaTime, Grav.magnitude));
		//i++;
		gameObject.GetComponent<ConstantForce>().force = Grav;
		if ((gameObject.transform.position.y<-30)&&!teleportRun){
			//Debug.Log(string.Format("calc count={0}, y={1}",i,gameObject.transform.position.y));
			//i=0;
			//teleportRun = true;
			//GameObject.Find("pfPortal1").GetComponent<scrPortal1>().RunTeleport();
			teleportRun = portal.GetComponent<scrPortal1>().RunTeleport(gameObject);
			//GoToStart();

		}
		if (basket!=null &&(basket.transform.position-gameObject.transform.position).sqrMagnitude<0.5f){
			if (WaitPass<=0){
				BasketPass++;
				WaitPass = 1f;
				if (BasketPass>=3){
					BasketPass = 0;
					//GameObject.Find("pfPortal1").GetComponent<scrPortal1>().RunTeleport();
					//teleportRun = portal.GetComponent<scrPortal1>().RunTeleport(gameObject);
					GameObject.Find("pfResultCanvas").GetComponent<scrResult>().ShowResult();

				}
			}else WaitPass-=Time.fixedDeltaTime;
			//Debug.Log(string.Format("pass={0}",BasketPass));
		} else if (BasketPass>0)BasketPass=0;

	}

	Vector3 GetGravity(Vector3 dest, float gravity){
		//float res = G*m1*gravity/dest.sqrMagnitude;
		//Mathf.Floor
		return(dest.normalized*m1*gravity/dest.sqrMagnitude);
	}

	/*public void GoToStart(Vector3 newPos){
		gameObject.transform.position = newPos;
		teleportRun = false;
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		gameObject.GetComponent<ConstantForce>().force = Vector3.zero;

		//Debug.Log(string.Format("v={0} av={1}",gameObject.GetComponent<Rigidbody>().velocity,gameObject.GetComponent<Rigidbody>().angularVelocity));
	}*/


}

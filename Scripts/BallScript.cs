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
	float maxSpeed=0;
	float maxHeight=0;
	float tmpFloat;
	float startFlightTime=0;
	// Use this for initialization
	public void Start () {
		basket = GameObject.FindGameObjectWithTag("basket");
		portal = GameObject.FindGameObjectWithTag("portal");

	}
	
	// Update is called once per frame
	void Update () {
		

	}

	void FixedUpdate () {
		GameObject[] GObjs;
		GObjs = GameObject.FindGameObjectsWithTag("points"); //получить все точки на уровне
		Vector3 Grav=Vector3.zero;
		Vector3 tV3;
		foreach (GameObject GObj in GObjs) { //посчитаем гравитационное воздействие от действующих точек
			if (!GObj.GetComponent<pointScript>().pointOn) continue;
			int idxGr=1;
			switch (GObj.GetComponent<pointScript>().pointType) {
				case 0: //обычная точка - притягивающая 
					goto default; 
				case 1: //антигравитационная точка
					idxGr=-1;
					break;
				case 2: //пульсирующая точка
					tV3 = (GObj.transform.position - transform.position).normalized + transform.GetComponent<Rigidbody>().velocity.normalized;
					if (tV3.sqrMagnitude>1f && (GObj.transform.position - transform.position).sqrMagnitude > 2f) idxGr = 1; //если приближается - притягивать
						else idxGr = -1; //если удаляется - отталкивать
					break;
				default:
					idxGr=1;
					break;
			}
			tV3 = GetGravity(-transform.position+GObj.transform.position, idxGr*GObj.GetComponent<pointScript>().gravity);
			if (tV3.sqrMagnitude>1){
			//if ((GObj.transform.position-transform.position).sqrMagnitude<200){
				Grav += tV3;
			}
		}

		//добавим воздействие расчитанной гравитации на шар
		gameObject.GetComponent<ConstantForce>().force = Grav;  

		//если шар ушёл вниз ниже 30 - вернем его
		if ((gameObject.transform.position.y<-30)&&!teleportRun){
			teleportRun = portal.GetComponent<scrPortal1>().RunTeleport(gameObject);
		}

		//проверим что шар действительно в корзине
		if (basket!=null &&(basket.transform.position-gameObject.transform.position).sqrMagnitude<0.5f){
			if (WaitPass<=0){
				BasketPass++;
				WaitPass = 1f;
				if (BasketPass>=3){
					BasketPass = 0;
					//GameObject.Find("pfPortal1").GetComponent<scrPortal1>().RunTeleport();
					//teleportRun = portal.GetComponent<scrPortal1>().RunTeleport(gameObject);
					if (myGlobal.currentLevel.noChangeAfterTeleport){
						Debug.Log("show result");
						basket = null;
						GameObject.Find("pfResultCanvas").GetComponent<scrResult>().ShowResult();
					} else if (!teleportRun) teleportRun = portal.GetComponent<scrPortal1>().RunTeleport(gameObject);

				}
			}else WaitPass-=Time.fixedDeltaTime;
			//Debug.Log(string.Format("pass={0}",BasketPass));
		} else if (BasketPass>0)BasketPass=0;


		//для расчёта доп. очков посчитаем макс. скорость и высоту
		tmpFloat = gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude;
		if (tmpFloat>maxSpeed) maxSpeed = tmpFloat;

		if (maxHeight<gameObject.transform.position.y) maxHeight = gameObject.transform.position.y;
	}

	Vector3 GetGravity(Vector3 dest, float gravity){ //
		//float res = G*m1*gravity/dest.sqrMagnitude;
		//Mathf.Floor
		return(dest.normalized*m1*gravity/dest.sqrMagnitude);
	}


	public float GetMaxSpeed(){
		return Mathf.Sqrt(maxSpeed);
	}

	public float GetMaxHeight(){
		return maxHeight;
	}

	public void ResetAfterTeleport(){
		startFlightTime = Time.realtimeSinceStartup; 
		maxSpeed = 0;
		maxHeight = 0;
	}

	public float GetFlightTime(){
		if (startFlightTime==0) return 0;
		return Time.realtimeSinceStartup-startFlightTime;
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

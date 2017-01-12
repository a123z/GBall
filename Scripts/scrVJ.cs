using UnityEngine;
using System.Collections;

public class scrVJ : MonoBehaviour {
	public GameObject MovingGO;
	public float Step = 0.2f;

	Vector3 tempV3;
	bool pressed=false;
	float waitRepeat=0;
	float dWaitRepeat = 0.5f;
	int dir=0;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (pressed){
			if (waitRepeat<=0){
				Debug.Log("repeat press");
				move(dir);
				waitRepeat = 0.1f + dWaitRepeat; 
				if (dWaitRepeat>0) dWaitRepeat -= 0.05f;
			} else waitRepeat -= Time.deltaTime;
		} else dWaitRepeat = 0.20f;
	}


	void move(int aDir=0){
		if (MovingGO != null){
			if (aDir == 0) return;
			switch (aDir){
				case 1: MovingGO.transform.position += new Vector3(0,Step,0); break;
				case 2: MovingGO.transform.position += new Vector3(Step,0,0); break;
				case 3: MovingGO.transform.position -= new Vector3(0,Step,0); break;
				case 4: MovingGO.transform.position -= new Vector3(Step,0,0); break;
			}
			myGlobal.currentLevel.noChangeAfterTeleport = false;
		} else Debug.Log("Moving Object is NULL!");
	}

	/*public void moveUp(){
		if (MovingGO != null){
			MovingGO.transform.position += new Vector3(0,Step,0);
		} else Debug.Log("Moving Object is NULL!");
	}

	public void moveDown(){
		if (MovingGO != null){
			MovingGO.transform.position -= new Vector3(0,Step,0);
		} else Debug.Log("Moving Object is NULL!");
	}

	public void moveLeft(){
		if (MovingGO != null){
			MovingGO.transform.position -= new Vector3(Step,0,0);
		} else Debug.Log("Moving Object is NULL!");
	}

	public void moveRight(){
		if (MovingGO != null){
			MovingGO.transform.position += new Vector3(Step,0,0);
		} else Debug.Log("Moving Object is NULL!");
	}*/

	public void stopPress(){
		if (pressed) pressed = false;
	}

	public void startPress(int Direction){
		dir = Direction;
		if (!pressed) {
			pressed = true;
			waitRepeat = 0;
		}
	}

	public void test33(int i=0){
		Debug.Log("test N"+i.ToString());
	}
}

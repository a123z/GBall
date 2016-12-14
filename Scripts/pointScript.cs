using UnityEngine;
using System.Collections;

public class pointScript : MonoBehaviour {
	public float gravity=10f;
	public bool pointOn=true;
	public int pointType=0;

	protected int selectstate=1;
	// Use this for initialization
	void Start () {
		SetGravity(gravity);
		SetSelectState(0);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){

	}

	public void SetGravity(float GraviValue){
		gravity = GraviValue;
		if (gravity<0) gravity = 0;
		float ShpereScale = Mathf.Sqrt(2f*GraviValue/2.0f)+1;//m1*gravi/F
		//Debug.Log(string.Format("new scale={0},{1}",ShpereScale,GraviValue));
		gameObject.transform.GetChild(4).localScale = new Vector3(ShpereScale,ShpereScale,ShpereScale);
	}

	public float GetGravity(){
		return(gravity);
	}

	public void SetPointOn(){
		pointOn = true;
		gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
		if (gameObject.GetComponent<MeshRenderer>().material.HasProperty("emission")){
			gameObject.GetComponent<MeshRenderer>().material.SetFloat("Emission",0);
		} else Debug.Log("no prop");
	}

	public void SetPointOff(){
		pointOn = false;
		gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
		if (gameObject.GetComponent<MeshRenderer>().material.HasProperty("Emission")){
			gameObject.GetComponent<MeshRenderer>().material.SetFloat("Emission",0.5f);
		}
	}

	public int GetSelectState(){
		//
		return(selectstate);
	}

	public void SetSelectState(int SelectState=0){
		selectstate = SelectState;
		if (selectstate>2)selectstate=0;
		if (gameObject.transform.childCount==0) return;
		switch (selectstate){
		case 2: //power change
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
			gameObject.transform.GetChild(1).gameObject.SetActive(true);
			gameObject.transform.GetChild(4).gameObject.SetActive(true);
			gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
			gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.red;
			gameObject.transform.GetChild(2).gameObject.SetActive(false);
			gameObject.transform.GetChild(3).gameObject.SetActive(false);
			break;
		case 1: //move point
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
			gameObject.transform.GetChild(1).gameObject.SetActive(true);
			gameObject.transform.GetChild(2).gameObject.SetActive(true);
			gameObject.transform.GetChild(3).gameObject.SetActive(true);
			gameObject.transform.GetChild(4).gameObject.SetActive(true);
			gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
			gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.green;
			gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Color.green;
			gameObject.transform.GetChild(3).GetComponent<MeshRenderer>().material.color = Color.green;
			break;
		default: 
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
			gameObject.transform.GetChild(1).gameObject.SetActive(false);
			gameObject.transform.GetChild(2).gameObject.SetActive(false);
			gameObject.transform.GetChild(3).gameObject.SetActive(false);
			gameObject.transform.GetChild(4).gameObject.SetActive(false);
			break;
		}
	}
}

using UnityEngine;
using System.Collections;

public class scrPointControl : MonoBehaviour {
	GameObject point;
	GameObject powerTxtObj;
	// Use this for initialization
	void Start () {

		powerTxtObj = GameObject.Find("txtPower");
		powerTxtObj.GetComponent<UnityEngine.UI.Text>().text = "0";
		Debug.Log(string.Format("asdas{0}",powerTxtObj.GetComponentInParent<RectTransform>().anchoredPosition));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showControl(GameObject aActivePoint){
		point = aActivePoint;
		//Canvas.

		transform.position = Camera.main.WorldToScreenPoint(aActivePoint.transform.position)+(new Vector3(Screen.width/15,0,0));
		updatePowerText();
		gameObject.SetActive(true);
		//Debug.Log(string.Format("screen{0}",Camera.main.WorldToScreenPoint(ActivePoint.transform.position)));
		//Debug.Log(string.Format("viewpoint{0}",Camera.main.WorldToViewportPoint(ActivePoint.transform.position)));

	}

	public void hideControl(){
		if (gameObject.activeSelf)gameObject.SetActive(false);
	}

	public void gravityPlus(){
		point.GetComponent<pointScript>().SetGravity(point.GetComponent<pointScript>().GetGravity()+1);
		updatePowerText();
	}

	public void gravityMinus(){
		point.GetComponent<pointScript>().SetGravity(point.GetComponent<pointScript>().GetGravity()-1);
		updatePowerText();
	}

	void updatePowerText(){
		powerTxtObj.GetComponent<UnityEngine.UI.Text>().text = point.GetComponent<pointScript>().GetGravity().ToString();
	}

	public void delPoint(){
		Destroy(point);
		hideControl();
	}
}

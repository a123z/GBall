using UnityEngine;
using System.Collections;

public class scrPointControl : MonoBehaviour {
	GameObject point;
	GameObject powerTxtObj;
	GameObject grCount;
	// Use this for initialization
	void Start () {
		grCount = GameObject.Find("txtGr");
		powerTxtObj = GameObject.Find("txtPower");
		powerTxtObj.GetComponent<UnityEngine.UI.Text>().text = "0";
		//Debug.Log(string.Format("asdas{0}",powerTxtObj.GetComponentInParent<RectTransform>().anchoredPosition));
		if (grCount==null) Debug.Log("Поле для отображения кол-ва гравитонов не найдено.");
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
		if ((myGlobal.Gr-myGlobal.deltaGr)>=0){
			myGlobal.Gr += -myGlobal.deltaGr;
			point.GetComponent<pointScript>().SetGravity(point.GetComponent<pointScript>().GetGravity()+myGlobal.deltaGr);
			updatePowerText();
		};
	}

	public void gravityMinus(){
		if (point.GetComponent<pointScript>().GetGravity()-myGlobal.deltaGr>=0){
			myGlobal.Gr += myGlobal.deltaGr;
			point.GetComponent<pointScript>().SetGravity(point.GetComponent<pointScript>().GetGravity()-myGlobal.deltaGr);
			updatePowerText();
		}
	}

	void updatePowerText(){
        if (powerTxtObj != null && point != null)powerTxtObj.GetComponent<UnityEngine.UI.Text>().text = point.GetComponent<pointScript>().GetGravity().ToString();
		grCount.GetComponent<UnityEngine.UI.Text>().text = myGlobal.Gr.ToString();
	}

	public void delPoint(){
		Destroy(point);
		hideControl();
	}
}

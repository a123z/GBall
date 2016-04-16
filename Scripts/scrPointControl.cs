using UnityEngine;
using System.Collections;

public class scrPointControl : MonoBehaviour {
	GameObject point;
	GameObject powerTxtObj;
	GameObject grCount;
	// Use this for initialization
	void Start () {
        Debug.Log("start scrPointControl");
        //grCount = GameObject.Find("txtGr");
        //if (grCount == null) Debug.Log("E.! txtGr not found!!!!");
        //powerTxtObj = GameObject.Find("txtPower");
        //if (powerTxtObj == null) Debug.Log("E.! powerTxtObj not found!!!!");
        initPointControl();
        powerTxtObj.GetComponent<UnityEngine.UI.Text>().text = "0";
		//Debug.Log(string.Format("asdas{0}",powerTxtObj.GetComponentInParent<RectTransform>().anchoredPosition));
		if (grCount==null) Debug.Log("Поле для отображения кол-ва гравитонов не найдено.");
	}
	
    void initPointControl()
    {
        if (grCount == null) grCount = GameObject.Find("txtGr");
        if (grCount == null) Debug.Log("E.! txtGr not found!!!!");
        if (powerTxtObj == null) powerTxtObj = GameObject.Find("txtPower");
        if (powerTxtObj == null) Debug.Log("E.! powerTxtObj not found!!!!");
    }

	// Update is called once per frame
	void Update () {
	
	}

	public void showControl(GameObject aActivePoint){
		point = aActivePoint;
        //Canvas.
        initPointControl();
		transform.position = Camera.main.WorldToScreenPoint(aActivePoint.transform.position + new Vector3(aActivePoint.transform.localScale.x*2,0,0))+(new Vector3(50,0,0));
		updatePowerText();
		gameObject.SetActive(true);
		//Debug.Log(string.Format("screen{0}",Camera.main.WorldToScreenPoint(ActivePoint.transform.position)));
		//Debug.Log(string.Format("viewpoint{0}",Camera.main.WorldToViewportPoint(ActivePoint.transform.position)));

	}

	public void hideControl(){
		if (gameObject.activeSelf)gameObject.SetActive(false);
	}

	public void gravityPlus(){
		if ((myGlobal.gameData.gr-myGlobal.deltaGr)>=0){
			myGlobal.gameData.gr += -myGlobal.deltaGr;
			point.GetComponent<pointScript>().SetGravity(point.GetComponent<pointScript>().GetGravity()+myGlobal.deltaGr);
			updatePowerText();
		};
	}

	public void gravityMinus(){
		if (point.GetComponent<pointScript>().GetGravity()-myGlobal.deltaGr>=0){
			myGlobal.gameData.gr += myGlobal.deltaGr;
			point.GetComponent<pointScript>().SetGravity(point.GetComponent<pointScript>().GetGravity()-myGlobal.deltaGr);
			updatePowerText();
		}
	}

	void updatePowerText(){
        if (powerTxtObj != null && point != null)powerTxtObj.GetComponent<UnityEngine.UI.Text>().text = point.GetComponent<pointScript>().GetGravity().ToString();
		grCount.GetComponent<UnityEngine.UI.Text>().text = myGlobal.gameData.gr.ToString();
	}

	public void delPoint(){
		Destroy(point);
		hideControl();
	}
}

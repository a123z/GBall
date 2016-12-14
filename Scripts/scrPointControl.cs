using UnityEngine;
using System.Collections;

public class scrPointControl : MonoBehaviour {
	GameObject point;
	GameObject powerTxtObj;
	GameObject grCount;
	GameObject VJ;

	bool pressed;
	int typePress=0;
	float waitRepeat=0;
	float dWaitRepeat = 0.5f;

	// Use this for initialization
	void Start () {
        Debug.Log("start scrPointControl");

	}

	void Awake(){
		VJ = GameObject.Find("pfCanvas/VJoystick");
		if (VJ == null) Debug.Log("VJoystick not found!!!"); else Debug.Log("VJoystick found");
		initPointControl();
		updatePowerText();
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
		if (pressed){
			if (waitRepeat<=0){
				Debug.Log("repeat press");
				gravityChange(typePress);
				waitRepeat = 0.15f + dWaitRepeat; 
				if (dWaitRepeat>0) dWaitRepeat -= 0.05f;
			} else waitRepeat -= Time.deltaTime;
		} else dWaitRepeat = 0.15f;
	}

	public void showControl(GameObject aActivePoint){
		point = aActivePoint;
        //Canvas.
        initPointControl();
		transform.position = Camera.main.WorldToScreenPoint(aActivePoint.transform.position + new Vector3(aActivePoint.transform.localScale.x*2,0,0))+(new Vector3(50,0,0));
		updatePowerText();
		gameObject.SetActive(true);
		if (VJ != null) {
			VJ.GetComponent<scrVJ>().MovingGO = aActivePoint;
			VJ.SetActive(true);
		}
		//Debug.Log(string.Format("screen{0}",Camera.main.WorldToScreenPoint(ActivePoint.transform.position)));
		//Debug.Log(string.Format("viewpoint{0}",Camera.main.WorldToViewportPoint(ActivePoint.transform.position)));

	}

	public void hideControl(){
		if (gameObject.activeSelf)gameObject.SetActive(false);
		if (VJ != null) {
			VJ.GetComponent<scrVJ>().MovingGO = null;
			VJ.SetActive(false);
		}
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

	void gravityChange(int type){
		if ((type == 1 && (myGlobal.gameData.gr-myGlobal.deltaGr)>=0)||
			(type == -1 && point.GetComponent<pointScript>().GetGravity()-myGlobal.deltaGr>=0)){
			myGlobal.gameData.gr += -myGlobal.deltaGr*type;
			point.GetComponent<pointScript>().SetGravity(point.GetComponent<pointScript>().GetGravity()+type*myGlobal.deltaGr);
			updatePowerText();
		};
	}

	void updatePowerText(){ //обновляет текст в общем кол-ве гравитонов и в исползованом на точке
        if (powerTxtObj != null && point != null)powerTxtObj.GetComponent<UnityEngine.UI.Text>().text = point.GetComponent<pointScript>().GetGravity().ToString();
		grCount.GetComponent<UnityEngine.UI.Text>().text = myGlobal.gameData.gr.ToString();
	}

	public void delPoint(){
		myGlobal.gameData.gr += (int)point.GetComponent<pointScript>().GetGravity();
		int t = point.GetComponent<pointScript>().pointType;
		if (t>0) myGlobal.gameData.specGrCount[t]++;
		Destroy(point);
		updatePowerText();
		hideControl();
	}

	public void onOffPoint(){
		if (point.GetComponent<pointScript>().pointOn) point.GetComponent<pointScript>().SetPointOff();
		else point.GetComponent<pointScript>().SetPointOn();
	}

	public void stopPress(){
		if (pressed) pressed = false;
	}

	public void startPress(int typeKey){
		typePress = typeKey;
		if (!pressed) {
			pressed = true;
			waitRepeat = 0;
		}
	}


}

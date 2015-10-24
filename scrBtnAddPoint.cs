using UnityEngine;
using System.Collections;

public class scrBtnAddPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowAddPointsPanel(){
		GameObject addPPanel = GameObject.Find("panPoints");
		if (addPPanel != null) {
			addPPanel.transform.rotation = new Quaternion(0,0,0,0);
			//addPPanel.SetActive(true);
		}
	}

	public void HideAddPointsPanel(){
		GameObject addPPanel = GameObject.Find("panPoints");
		if (addPPanel != null) {
			addPPanel.transform.rotation = new Quaternion(90,0,0,0);
			//addPPanel.SetActive(false);
		}
	}

	//bool IsHideAddPointsPanel(){}

}

using UnityEngine;
using System.Collections;

public class scrResult : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Start scrResult");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowResult(){
		GetComponent<Canvas>().enabled = true;
		myGlobal.UIClick = true;



		GameObject.Find("txtTime_scr").GetComponent<UnityEngine.UI.Text>().text = Mathf.FloorToInt((Time.realtimeSinceStartup - myGlobal.StartLevelTime)/60).ToString()+
			":"+Mathf.FloorToInt((Time.realtimeSinceStartup - myGlobal.StartLevelTime)%60).ToString();
		GameObject.Find("txtGrLeft_scr").GetComponent<UnityEngine.UI.Text>().text = myGlobal.gameData.gr.ToString();

		int scr = Mathf.FloorToInt(GameObject.Find("Ball").GetComponent<BallScript>().GetMaxSpeed()/
									GameObject.Find("goLevel").GetComponent<scrLevel>().HighSpeed)*5;
		GameObject.Find("txtFast_scr").GetComponent<UnityEngine.UI.Text>().text = scr.ToString();
		myGlobal.gameData.score += scr;

		scr =  Mathf.FloorToInt(GameObject.Find("Ball").GetComponent<BallScript>().GetMaxHeight()/
							GameObject.Find("goLevel").GetComponent<scrLevel>().LargeHeight)*5;
		myGlobal.gameData.score += scr;
		GameObject.Find("txtLong_scr").GetComponent<UnityEngine.UI.Text>().text = scr.ToString();

		myGlobal.gameData.score += 100;

		GameObject.Find("txtScore_scr").GetComponent<UnityEngine.UI.Text>().text = myGlobal.gameData.score.ToString();

	}

	public void NextLevel(){
		GameObject tGO = GameObject.Find("goLevel");
		if (tGO!=null){
			if (tGO.GetComponent<scrLevel>().levelNum < myGlobal.levelsCount){
				tGO.GetComponent<scrLevel>().loadNextLevel(); 
			}
		}
	}
}

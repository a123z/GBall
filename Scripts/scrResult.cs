using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrResult : MonoBehaviour {
	int currentLevel;

	// Use this for initialization
	void Start () {
		Debug.Log("!!!!!!!!!!    Start scrResult");
		GameObject.Find("txtCaption").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtResult1;
		GameObject.Find("txtTime").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtResult2;
		GameObject.Find("txtGrLeft").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtResult3;
		GameObject.Find("txtFast").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtResult4;
		GameObject.Find("txtLong").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtResult5;
		GameObject.Find("txtScore").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtResult6;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowResult(){
		GetComponent<Canvas>().enabled = true;
		myGlobal.UIClick = true;

		GameObject goLevel = GameObject.Find("goLevel");
		currentLevel = goLevel.GetComponent<scrLevel>().levelNum;
		goLevel.GetComponent<scrController>().hidePointControl();

		myGlobal.gameData.levels[currentLevel].Score = 0;

		GameObject.Find("txtTime_scr").GetComponent<UnityEngine.UI.Text>().text = Mathf.FloorToInt((Time.realtimeSinceStartup - myGlobal.StartLevelTime)/60).ToString()+
			":"+Mathf.FloorToInt((Time.realtimeSinceStartup - myGlobal.StartLevelTime)%60).ToString();
		GameObject.Find("txtGrLeft_scr").GetComponent<UnityEngine.UI.Text>().text = myGlobal.gameData.gr.ToString();
		myGlobal.gameData.levels[currentLevel].Score += myGlobal.gameData.gr;

		int scr = Mathf.FloorToInt(GameObject.Find("Ball").GetComponent<BallScript>().GetMaxSpeed()/
									GameObject.Find("goLevel").GetComponent<scrLevel>().HighSpeed)*5;
		GameObject.Find("txtFast_scr").GetComponent<UnityEngine.UI.Text>().text = scr.ToString();
		myGlobal.gameData.levels[currentLevel].Score += scr;

		scr =  Mathf.FloorToInt(GameObject.Find("Ball").GetComponent<BallScript>().GetMaxHeight()/
							GameObject.Find("goLevel").GetComponent<scrLevel>().LargeHeight)*5;
		myGlobal.gameData.levels[currentLevel].Score += scr;
		GameObject.Find("txtLong_scr").GetComponent<UnityEngine.UI.Text>().text = scr.ToString();

		myGlobal.gameData.levels[currentLevel].Score += 100; //за прохождение уровня

		GameObject.Find("txtScore_scr").GetComponent<UnityEngine.UI.Text>().text = myGlobal.gameData.levels[currentLevel].Score.ToString();

		myGlobal.gameData.levels[currentLevel].passed = true;
		GameObject.Find("goLevel").GetComponent<scrLevel>().saveLevelData();

	}

	public void NextLevel(){
		GameObject tGO = GameObject.Find("goLevel");
		Debug.Log("next level");
		if (tGO!=null){
			if (tGO.GetComponent<scrLevel>().levelNum < myGlobal.levelsCount){
				tGO.GetComponent<scrLevel>().loadNextLevel(); 
			} else myGlobal.loadLevel(1);
		}
	}

	public void Restart(){
		myGlobal.UIClick = false;
		GetComponent<Canvas>().enabled = false;
		GameObject.Find("pfPortal").GetComponent<scrPortal1>().RunTeleport(GameObject.Find("Ball"));
		GameObject.Find("Ball").GetComponent<BallScript>().Start();
		myGlobal.gameData.levels[currentLevel].Score = 0;
	}

	public void LoadLevelSelect(){
		myGlobal.LoadLevelSelect();
		//SceneManager.LoadScene("levelSelect");
	}
}

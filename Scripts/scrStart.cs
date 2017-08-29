using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrStart : MonoBehaviour {
	public TextAsset rusLng;
	public TextAsset engLng;

	GameObject goAbout;
	GameObject goFinish;


	// Use this for initialization
	void Start () {
		goAbout = GameObject.Find("scvAbout");
		if (goAbout != null){
			goAbout.SetActive(false);
		}
		goFinish = GameObject.Find("paFinish");
		if (goFinish != null){
			if (!myGlobal.ShowFinish){
				goFinish.SetActive(false);
			} else FinishGame();
		}

		//т.к. изначально локализация не планировалась а текста у нас мало - делаем топорно - грузим текст из разных xml-файлов
		myGlobal.initLocalization();
		loadLoc();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameExit(){
		Application.Quit();
	}

	public void loadSceneSelector(){
		SceneManager.LoadScene("levelSelect");
	}

	public void AboutGame(){
		if (goAbout != null) goAbout.SetActive(true);
	}

	public void FinishGame(){
		if (goFinish != null){
			int scores = 0;
			for (int i=1; i<myGlobal.levelsCount; i++) scores += myGlobal.gameData.levels[i].Score;
			goFinish.transform.Find("txtScores").GetComponent<UnityEngine.UI.Text>().text = scores.ToString();
			goFinish.transform.Find("txtLastScores").GetComponent<UnityEngine.UI.Text>().text = myGlobal.gameData.score.ToString();
			if (myGlobal.gameData.score<scores) myGlobal.gameData.score = scores;
			myGlobal.ShowFinish = false;
			goFinish.SetActive(true);
		}
	}

	public void CloseAboutGame(){
		if (goAbout != null) goAbout.SetActive(false);
	}

	public void CloseFinishGame(){
		if (goFinish != null) goFinish.SetActive(false);
	}

	/*public void ad(){
		myGlobal.ShowAd(true);
	}*/

	public void saveLoc(){
		//goAbout.SetActive(true);
		//Debug.Log(goAbout.transform.Find("Viewport").Find("Content").GetComponent<UnityEngine.UI.Text>().text);
		myGlobal.LocalizationData.txtAbout = goAbout.transform.Find("Viewport").Find("Content").GetComponent<UnityEngine.UI.Text>().text;
		myGlobal.LocalizationData.txtBtnAbout = GameObject.Find("btnAbout").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text;
		myGlobal.LocalizationData.txtBtnExit = GameObject.Find("btnExit").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text;
		myGlobal.LocalizationData.txtBtnStart = GameObject.Find("btnStart").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text;
		goFinish.SetActive(true);
		myGlobal.LocalizationData.txtFinish1 = GameObject.Find("txtCongr").GetComponent<UnityEngine.UI.Text>().text;
		myGlobal.LocalizationData.txtFinish2 = GameObject.Find("txtYouAreFinished").GetComponent<UnityEngine.UI.Text>().text;
		myGlobal.LocalizationData.txtFinish3 = GameObject.Find("txtWithResult").GetComponent<UnityEngine.UI.Text>().text;
		myGlobal.LocalizationData.txtFinish4 = GameObject.Find("txtLastResult").GetComponent<UnityEngine.UI.Text>().text;
		myGlobal.LocalizationData.txtFinish5 = GameObject.Find("txtTryAgain").GetComponent<UnityEngine.UI.Text>().text;
		goFinish.SetActive(false);
		//myGlobal.LocalizationData.txtFinish6 = "";
		//myGlobal.LocalizationData.txtResult1 = GameObject.Find("txtTryAgain").GetComponent<UnityEngine.UI.Text>().text;
		//myGlobal.LocalizationData.
		//myGlobal.LocalizationData.



		myGlobal.SaveLocalization("rus.xml");
	}

	public void loadLoc(int LocalisationNo = 0){
		if (LocalisationNo != 0 && myGlobal.gameData.lang != LocalisationNo){
			myGlobal.gameData.lang = LocalisationNo;
			myGlobal.initLocalization();
		}
		goAbout.transform.Find("Viewport").Find("Content").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtAbout;
		GameObject.Find("btnAbout").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtBtnAbout;
		GameObject.Find("btnExit").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtBtnExit;
		GameObject.Find("btnStart").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtBtnStart;
		goFinish.SetActive(true);
		GameObject.Find("txtCongr").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtFinish1;
		GameObject.Find("txtYouAreFinished").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtFinish2;
		GameObject.Find("txtWithResult").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtFinish3;
		GameObject.Find("txtLastResult").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtFinish4;
		GameObject.Find("txtTryAgain").GetComponent<UnityEngine.UI.Text>().text = myGlobal.LocalizationData.txtFinish5;
		goFinish.SetActive(false);
	}
}

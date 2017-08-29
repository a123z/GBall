using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrLvlSelect : MonoBehaviour {
	public GameObject ButtonPrefub;
	int lvlCountW=5;
	int lvlCountH=3;
	int buttonWidth=100;
	int buttonHeight=100;
	// Use this for initialization
	void Start () {
		GameObject objBtnLvl;
		GameObject panel = GameObject.Find("lvlPanel");
		//(panel) as UnityEngine.UI.
		//Debug.Log("p="+panel.transform.position.ToString());
		//Debug.Log("w = "+buttonWidth.ToString());
		buttonWidth = Mathf.RoundToInt((Screen.width+((RectTransform)panel.transform).offsetMax.x-((RectTransform)panel.transform).offsetMin.x)/lvlCountW*1f);
		buttonHeight = Mathf.RoundToInt((Screen.height+((RectTransform)panel.transform).offsetMax.y-((RectTransform)panel.transform).offsetMin.y)/lvlCountH*1f);
		//Debug.Log("w2="+buttonWidth.ToString());
		int btnCount;
		if (myGlobal.levelsCount-1>(lvlCountW*lvlCountH)){
			btnCount = lvlCountW*lvlCountH;
		} else btnCount = myGlobal.levelsCount-1;
		int x_ = 1;
                int y_ = 1;

		for (int i=1;i<=btnCount;i++){
			//рисуем кнопку
			objBtnLvl = GameObject.Instantiate(ButtonPrefub) as GameObject;
			objBtnLvl.transform.SetParent(panel.transform,true);
			objBtnLvl.transform.localPosition = new Vector3(x_*buttonWidth-Mathf.RoundToInt(buttonWidth*0.5f),-y_*buttonHeight+Mathf.RoundToInt(buttonHeight*0.5f),0);
			//Debug.Log(objBtnLvl.transform.localPosition.ToString());
			objBtnLvl.name = "goLvlBtn"+i.ToString();
			objBtnLvl.GetComponentInChildren<UnityEngine.UI.Text>().text = i.ToString();
			objBtnLvl.GetComponent<scrLvlSelBtn>().lvlNum = i;
			//objBtnLvl.GetComponentInChildren<
			x_++;
			if (x_>lvlCountW){
				x_ = 1;
				y_++;
			}
			//Debug.Log(myGlobal.gameData.levels[1].passed.ToString());
			if (myGlobal.gameData.levels[i] != null){
				Debug.Log("lvl "+i.ToString());
				SetLevelProp(objBtnLvl, i==1||myGlobal.gameData.levels[i-1].passed, myGlobal.gameData.levels[i].passed); //opened||passed
			} else SetLevelProp(objBtnLvl, false, false);
			//SetLevelProp(i, i==1, false);
			//SetLevelProp(i, i==1);

			/*btnLvl = GameObject.Find("goLvlBtn"+i.ToString());
			btnLvl.GetComponentInChildren<UnityEngine.UI.Text>().text = i.ToString();
			btnLvl.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.blue;
			ResetLevelPassed(i);*/
			//SetLevelPassed(i.ToString());
			

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*void SetLevelPassed(int aLvlNo){
		GameObject btnLvl = GameObject.Find("goLvlBtn"+aLvlNo.ToString());
		if (btnLvl!=null){
		//	btnLvl.GetComponentInChildren<UnityEngine.UI.Text>().text = "5";
			btnLvl.GetComponentInChildren<UnityEngine.UI.Image>().enabled = true;
		}
	}

	void ResetLevelPassed(int aLvlNo){
		GameObject btnLvl = GameObject.Find("goLvlBtn"+aLvlNo.ToString());
		if (btnLvl!=null){
			//	btnLvl.GetComponentInChildren<UnityEngine.UI.Text>().text = "5";
			btnLvl.GetComponentInChildren<UnityEngine.UI.Image>().enabled = false;
		}
	}

	void SetLevelOpen(int aLvlNo, bool aOpened=true){
		GameObject btnLvl = GameObject.Find("goLvlBtn"+aLvlNo.ToString());
		if (btnLvl!=null){
			//	btnLvl.GetComponentInChildren<UnityEngine.UI.Text>().text = "5";
			btnLvl.GetComponentInChildren<UnityEngine.UI.Button>().enabled = aOpened;
		}
	}*/


	void SetLevelProp(int aLvlNo, bool aOpened=true, bool aPassed=true){
		GameObject btnLvl = GameObject.Find("goLvlBtn"+aLvlNo.ToString());
		if (btnLvl!=null){
			SetLevelProp(btnLvl,aOpened,aPassed);
		}
	}

	void SetLevelProp(GameObject objButton, bool aOpened=true, bool aPassed=true){
		if (objButton!=null){
			//	btnLvl.GetComponentInChildren<UnityEngine.UI.Text>().text = "5";
			objButton.GetComponentInChildren<UnityEngine.UI.Button>().image.enabled = aOpened;
			objButton.transform.Find("Image").GetComponentInChildren<UnityEngine.UI.Image>().enabled = aPassed;
		}
	}


}

using UnityEngine;
using System.Collections;

public class scrSettingsPanel : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		//GameObject g = GameObject.Find("btnSettings"); 
		GameObject.Find("btnSettings").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate{ShowHidePanel();});
		GameObject.Find("btnLvlScene").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate{myGlobal.LoadLevelSelect();});
		GameObject.Find("btnExit").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate{Application.Quit();});

		UnityEngine.UI.Toggle compTSnd = GameObject.Find("togSound").GetComponent<UnityEngine.UI.Toggle>();
		compTSnd.isOn = myGlobal.getSound();
		compTSnd.onValueChanged.AddListener(delegate{SetSound();});
		GameObject.Find("btnSound").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate{compTSnd.isOn = !compTSnd.isOn; SetSound();});
		//GameObject.Find("togSound").GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener(delegate{SetSound();});

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowHidePanel(){
		gameObject.GetComponent<Canvas>().enabled = !gameObject.GetComponent<Canvas>().enabled;
	}

	public void SetSound(){
		myGlobal.SoundOn(GameObject.Find("togSound").GetComponent<UnityEngine.UI.Toggle>().isOn);
	}
}

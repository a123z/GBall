using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class scrTextInfo : MonoBehaviour {
	public int txtId=0;
	public string txt="";
	public 

	GameObject svGO;
	// Use this for initialization

	void Awake(){
		svGO = GameObject.Find("svTextInfo");
		if (svGO == null) Debug.Log("svTextInfo NOT Found!!");
	}

	void Start () {
		HideText();
	}


	// Update is called once per frame
	void Update () {
		if (svGO != null && svGO.activeSelf && (Input.GetMouseButton(0)||Input.touchCount>0)) HideText();
		
	}



	public void ShowText(int txtId, Vector2 GoPos){
		if (svGO != null){
			svGO.transform.Find("Content").GetComponent<UnityEngine.UI.Text>().text = txt;
			svGO.SetActive(true);
		}
	}

	public void ShowText(Vector2 GoPos){
		if (svGO != null){
			//Debug.Log("asdasdasd");
			//Debug.Log(svGO.transform.GetComponent<UnityEngine.UI.Text>().rectTransform.sizeDelta.x.ToString());

			//Debug.Log(svGO.transform.GetComponent<UnityEngine.RectTransform>().sizeDelta.ToString());
			//Debug.Log(svGO.transform.ToString());

			svGO.transform.position = GoPos - (GoPos-new Vector2(Screen.width/2, Screen.height/2)).normalized * 150;
			svGO.transform.Find("Viewport").Find("Content").GetComponent<UnityEngine.UI.Text>().text = txt;
			svGO.SetActive(true);
		}
	}

	public void ShowText(){
		if (svGO != null){
			Debug.Log(svGO.transform.childCount.ToString());
			//GameObject tgo = svGO.transform.Find("Viewport").Find("Content").gameObject;

			svGO.transform.position = new Vector3(Screen.width/2,Screen.height/2,0);
			svGO.transform.Find("Viewport").Find("Content").GetComponent<UnityEngine.UI.Text>().text = txt;
			svGO.SetActive(true);
		}
	}

	public void HideText(){
		if (svGO != null){
			svGO.SetActive(false);
		}
	}

	/*public void aaaaa(){//(BaseEventData bde){
		//Random.InitState(Mathf.RoundToInt(Time.realtimeSinceStartup));
		svGO.transform.Find("Viewport").Find("Content").GetComponent<UnityEngine.UI.Text>().text = "";
		for (int i=1; i<16;i++){
			svGO.transform.Find("Viewport").Find("Content").GetComponent<UnityEngine.UI.Text>().text += " " + Random.Range(0,100).ToString();
		}
	}*/
}

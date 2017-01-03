using UnityEngine;
using System.Collections;

public class scrTextInfo : MonoBehaviour {
	public int txtId=0;

	GameObject svGO;
	// Use this for initialization
	void Start () {
		svGO = GameObject.Find("svTextInfo");
		if (svGO == null) Debug.Log("svTextInfo NOT Found!!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowText(int txtId, Vector2 GoPos){
		if (svGO != null){
			svGO.SetActive(true);
		}
	}

	public void HideText(){
		if (svGO != null){
			svGO.SetActive(false);
		}
	}
}

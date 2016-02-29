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

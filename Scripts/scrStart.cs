using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}

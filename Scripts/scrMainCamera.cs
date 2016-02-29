using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadNextScene(){
		//Application.LoadLevel(1);
        SceneManager.LoadScene(1);
    }

}

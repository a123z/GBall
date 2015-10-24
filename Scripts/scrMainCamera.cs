using UnityEngine;
using System.Collections;

public class scrMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadNextScene(){
		Application.LoadLevel(1);
	}

}

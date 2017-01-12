using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

public class scrAd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowAd(){
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
}

using UnityEngine;
using System.Collections;
#if unity_android 
	using UnityEngine.Advertisements;
#endif
//using UnityEngine.EventSystems;

public class scrAd : MonoBehaviour {
	float AdRepeatTime = 600f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowAd(bool ShowNow = false){
		#if unity_android
		if (ShowNow) myGlobal.timeFromLastAd = Time.realtimeSinceStartup - AdRepeatTime - 1;
			if (Time.realtimeSinceStartup-myGlobal.timeFromLastAd > AdRepeatTime){
				if (Advertisement.IsReady())
				{
					Advertisement.Show();
			
				}
				myGlobal.timeFromLastAd = myGlobal.timeFromLastAd
			}
		#endif
	}
}

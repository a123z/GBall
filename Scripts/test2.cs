using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class test2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void testEvent(BaseEventData bed){
		if (bed != null){
			Debug.Log ("tteesstt = "+bed.selectedObject.name);
		}
	}
}

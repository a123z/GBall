using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class test2 : MonoBehaviour {
	Ray ray;
	RaycastHit hit;
	bool RaycastF;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastF = Physics.Raycast(ray, out hit, 100f);
			//if (RaycastF) Debug.Log("raycast touch"+hit.collider.name);
			if (RaycastF){ //проверяем что попали в гравиточку    если никуда не попали и долго держим - создать новую
				Debug.Log(string.Format("asd {0}",hit.collider.isTrigger));
			}
		}
	
	}

	public void testEvent(BaseEventData bed){
		if (bed != null){
			Debug.Log ("tteesstt = "+bed.selectedObject.name);
		}
	}
}

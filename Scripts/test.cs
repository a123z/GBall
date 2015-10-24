using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class test : MonoBehaviour {
	GameObject rrr;
	public GameObject point2DPrefab;
	public GameObject point3DPrefab;
	// Use this for initialization
	void Start () {
	
	}   
	
	// Update is called once per frame
	void Update () {
	
	}

	/*void OnGUI(){
		if (GUI.Button(new Rect(10,10,50,20),"asdasd")){
			rrr.transform.position = rrr.transform.position + Vector3.left;
		}
	}*/

	public void moveUp(){
		transform.position += Vector3.up;
	}

	public void moveDown(){
		transform.position += Vector3.down;
	}

	public void OnOff(){
		transform.position += Vector3.right;
	}

	public void Delete(){
		transform.position += Vector3.left;
	}

	public void testEvent(BaseEventData bde){


		if (bde!=null){
			Debug.Log("Event This is test EventData! ");//+bde.currentInputModule.name);
			//myGlobal.UIClick = true;
			//Input.ResetInputAxes();

		} else Debug.Log("This is test Event! (BaseEventData is null)");
		//EventSystem.current
	}

	/*public void testEvent2(BaseEventData bde){

		if (bde != null){
			Debug.Log("Event2 PointerEventData = ");//+bde.selectedObject.name);
		} else Debug.Log("This is test Event2! (BaseEventData is null)");
	}*/

	public void beginDrag(BaseEventData bde){
		rrr = GameObject.Instantiate(point2DPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
		rrr.transform.SetParent(GameObject.Find("pfCanvas").transform);
	}

	public void doingDrag(BaseEventData bde){
		if (rrr!=null){
			rrr.transform.position = Input.mousePosition;
		} else Debug.Log("rrr is null");
	}

	public void endDrag(BaseEventData bde){
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		//Debug.Log(string.Format("pos {0}  {1}",pos,Input.mousePosition));
		GameObject.Instantiate(point3DPrefab,pos,Quaternion.identity);
		Destroy(rrr);
		//rrr = null;
	}

}

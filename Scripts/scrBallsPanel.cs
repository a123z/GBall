using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class scrBallsPanel : MonoBehaviour {
	GameObject rrr;
	//public GameObject point2DPrefab; //не используется в скрипте 
	GameObject point3DPrefab;
	public int pointType=0;

	// Use this for initialization
	void Start () {
		if (GameObject.Find(myGlobal.goLevelName)==null) Debug.Log("goLevel not Found");
		//point3DPrefab = (GameObject) UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+myGlobal.pointPrefabName[pointType]+".prefab", typeof(GameObject));
		point3DPrefab = (GameObject) Resources.Load(myGlobal.pointPrefabName[pointType]);
		if (myGlobal.gameData == null) Debug.Log("gameData not init");

	}   

	void Awake(){
		if (myGlobal.gameData.specGrCount != null ) showCount();
		else Debug.Log("specGrCount not init");
	}

	// Update is called once per frame
	void Update () {
	
	}

	/*void OnGUI(){
		if (GUI.Button(new Rect(10,10,50,20),"asdasd")){
			rrr.transform.position = rrr.transform.position + Vector3.left;
		}
	}*/

	public void decPointCount(){
		if (pointType!=0) {
			myGlobal.gameData.specGrCount[pointType]--;
			showCount();
		}

	}

	public void showCount(){
		switch (pointType){
			case 0:
				GameObject.Find("iBgrGrCount").GetComponentInChildren<UnityEngine.UI.Text>().text = "∞";
				break;
			case 1: 
				GameObject.Find("iBgrAGrCount").GetComponentInChildren<UnityEngine.UI.Text>().text = myGlobal.gameData.specGrCount[pointType].ToString();
				break;
			case 2: 
				GameObject.Find("iBgrPGrCount").GetComponentInChildren<UnityEngine.UI.Text>().text = myGlobal.gameData.specGrCount[pointType].ToString();
				break;
			default: 
				gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "0";
				break;
		}
	}

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
		rrr = GameObject.Instantiate(gameObject, Input.mousePosition, Quaternion.identity) as GameObject;
		rrr.transform.SetParent(GameObject.Find("pfCanvas").transform);
		myGlobal.UIClick = true;
	}

	public void doingDrag(BaseEventData bde){
		if (rrr!=null){
			rrr.transform.position = Input.mousePosition;
		} else Debug.Log("rrr is null");
	}

	public void endDrag(BaseEventData bde){
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		//gameObject.
		//Debug.Log(string.Format("pos {0}  {1}",gameObject.name,gameObject.transform.position));
		if (pointType==0 || myGlobal.gameData.specGrCount[pointType]>0){
			GameObject.Instantiate(point3DPrefab,pos,Quaternion.identity);
			decPointCount();
		}

		Destroy(rrr);
		myGlobal.UIClick = false;
		//rrr = null;
	}

}

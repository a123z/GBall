using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CameraScrpt : MonoBehaviour {
	public GameObject PointPrefab;

	GameObject selected_point;
	Plane tmp_plane;
	Ray ray;
	RaycastHit hit;
	int mode=0; //0-construct mode 1-play mode
	int oper_id=0; //0-no operation, 1-move point 2-change power ?3-on|off point 4-add point 5-del point
	Vector3 ray_cast;
	Vector3 mouse_pos;
	//int levelsCount = 4;
	//SaveLoad.Level[] levels;
	float tf=10f;
	float maxSlVal;
	float minSlVal;
	bool PointSelected=false;
	bool guiChange=false;
	bool btnPressOnPoint=false;
	bool point_first_click=false;

	// Use this for initialization
	void Start () {
		//levels = new SaveLoad.Level[levelsCount];
		//levels[0] = new SaveLoad.Level();

		tmp_plane = new Plane(Vector3.forward,0);
		mode = 0;
		//return;

		//levels[0].points.GetLength


		Input.simulateMouseWithTouches = true;
	}

	void Update () {
		/*if (!GUI.changed && Input.GetMouseButtonDown(0) && mode==0&&1==0) {
			//btnPressed = true;
			Debug.Log(string.Format("upd gui_chg={0}",GUI.changed ));
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 500.0f)) {    //попали в какой-то объект
				if (hit.transform.gameObject.tag=="points"){ //попали в гравитационную точку
					if (PointSelected){
						if (selected_point!=hit.transform.gameObject){ //Е. до этого уже выбрана другая точка то снять с неё выбор
							selected_point.GetComponent<pointScript>().SetSelectState(0);
							selected_point = hit.transform.gameObject;
							point_first_click = true;
							selected_point.GetComponent<pointScript>().SetSelectState(1);
						} else { //Е. до этого выбрана эта же точка то сменить тип выбора
							//dcfgdf

						}
					} else {
						selected_point = hit.transform.gameObject;
						selected_point.GetComponent<pointScript>().SetSelectState(1);
						point_first_click = true;
					}
					btnPressOnPoint = true;//????
					PointSelected = true;
					mouse_pos = Input.mousePosition;
				} else if (PointSelected){PointSelected = false;selected_point.GetComponent<pointScript>().SetSelectState(0);}
			} else if (PointSelected){
				      PointSelected = false;selected_point.GetComponent<pointScript>().SetSelectState(0);
					} else if (oper_id==4){
								float rayDistance;
								tmp_plane.Raycast(ray, out rayDistance);
								GameObject g = Instantiate(PointPrefab,ray.GetPoint(rayDistance),Quaternion.identity) as GameObject;
								g.GetComponent<pointScript>().gravity = 100;
								oper_id=0;
							}
		} else
		if (!guiChange && Input.GetMouseButtonUp(0) && mode==0){
			btnPressOnPoint = false;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (PointSelected){
				if (!point_first_click&&(mouse_pos - Input.mousePosition).sqrMagnitude<0.2f){//отпустили кнопку без смещения
					switch (selected_point.GetComponent<pointScript>().GetSelectState()){
					case 1:selected_point.GetComponent<pointScript>().SetSelectState(2);
						break;
					case 2:selected_point.GetComponent<pointScript>().SetSelectState(1);
						break;
					default:selected_point.GetComponent<pointScript>().SetSelectState(1);
						break;
					}
				} else if (point_first_click)point_first_click=false;
				  
			}
		}
		if (btnPressOnPoint && mode==0 && Input.GetMouseButton(0)){ //кнопка нажата - отслеживаем движение
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float rayDistance;
			tmp_plane.Raycast(ray, out rayDistance);
			if (selected_point.GetComponent<pointScript>().GetSelectState()==1){
				selected_point.transform.position = ray.GetPoint(rayDistance);
			} else if (selected_point.GetComponent<pointScript>().GetSelectState()==2){
				float tg = selected_point.GetComponent<pointScript>().gravity;
				//Debug.Log(string.Format("tg={0}, y={1}",tg,(selected_point.transform.position - ray.GetPoint(rayDistance)).y));
				selected_point.GetComponent<pointScript>().SetGravity(tg+(ray.GetPoint(rayDistance)-selected_point.transform.position).y*tg*0.01f);
			}

		}*/
	}

	// Update is called once per frame
	/*void Update () {
		if (!guiChange && Input.GetMouseButtonDown(0) && mode==0) {
			//Debug.Log("down");
			//Graphic.wasClicked = true; //????
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//check for click to point
			if (Physics.Raycast (ray, out hit, 500.0f)) {    //попали в какой-то объект
				if (hit.transform.gameObject.tag=="points"){
					selected_point = hit.transform.gameObject;

					PointSelected = true;
					tf = selected_point.GetComponent<pointScript>().gravity/10;
					maxSlVal = Mathf.Abs(selected_point.GetComponent<pointScript>().gravity/10)*2;
					minSlVal = -maxSlVal;
					if (oper_id==4){
						return; //нельзя добавть новую точку в уже существующую - выход
					} else if (oper_id==5){
						DestroyObject(selected_point);
						oper_id=0;
						return;
					}else if (oper_id==0){
						oper_id=2;
						
						return;//???
					}
					//target = hit.point;
				} else return;
			} else {
						//if (!guiChange) PointSelected = false;
				        if (oper_id==4){
					        float rayDistance;
							tmp_plane.Raycast(ray, out rayDistance);
							GameObject g = Instantiate(PointPrefab,ray.GetPoint(rayDistance),Quaternion.identity) as GameObject;
							g.GetComponent<pointScript>().gravity = 400;
							oper_id=0;
			       		}
				   }
		} else

		if (!guiChange && Input.GetMouseButtonUp(0) && mode==0){
			//Debug.Log("up");
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (oper_id==2){
				float rayDistance;
				tmp_plane.Raycast(ray, out rayDistance);
				selected_point.transform.position = ray.GetPoint(rayDistance);
				oper_id = 0;
			}
		} else
		if (oper_id==2){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float rayDistance;
			tmp_plane.Raycast(ray, out rayDistance);
			selected_point.transform.position = ray.GetPoint(rayDistance);
		}
	}*/






	/*void OnGUI(){
		//Debug.Log(string.Format("gui gui_chg={0}",GUI.changed ));
		if (GUI.Button(new Rect(0,0,Screen.width/12f,Screen.height/12f),"Exit")){
			//OnApplicationQuit();
			Application.Quit();
		}
		if (GUI.Button(new Rect(Screen.width/11.5f,0,Screen.width/12f,Screen.height/12f),"repeat")){
			GameObject ball = GameObject.Find("Ball")as GameObject;
			//ball.GetComponent<BallScript>().GoToStart();
			GameObject portal1 = GameObject.Find("pfPortal")as GameObject;
			if (portal1==null)Debug.Log("portal not find"); 
			portal1.GetComponent<scrPortal1>().RunTeleport();
		}
		//bool tb = oper_id==4;
		if (GUI.RepeatButton(new Rect(0,Screen.height*2/12f,Screen.width/18f,Screen.height/12),"+")){
			oper_id = 4;
		}
		if (GUI.Button(new Rect(0,Screen.height*3/12f,Screen.width/18f,Screen.height/12f),"-")){
			Debug.Log(string.Format("gui gui_chg={0}",GUI.changed ));
			if (PointSelected){
				Debug.Log(string.Format("sp={0}",selected_point.name));
				DestroyObject(selected_point);
			}
		} 

		//tf = GUI.VerticalSlider(new Rect(Screen.width-25, 20, 20, Screen.height-40), tf, maxSlVal, minSlVal);
		//GUI.Label(new Rect(120, 5, 40, 20),tf.ToString());
		 
		guiChange = GUI.changed;
		//Debug.Log(string.Format("11{0} {1}",guiChange,PointSelected));
		if (guiChange&&PointSelected){
			selected_point.GetComponent<pointScript>().gravity = tf*10;
			Debug.Log(string.Format("asdasd{0} {1}",guiChange,PointSelected));
		}

	}*/

}

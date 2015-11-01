using UnityEngine;
using System.Collections;


public class Controller : MonoBehaviour {
	int Touch1Type; //0-ничего не делаем 1-попали в точку 2-никуда не попали 
	                //3-попали в точку и двигаем  двигаем  4-никуда не попали и держим 5-попали в точку и отпустили
	Vector3 BeginTouch1;
	Vector3 BeginTouch2;
	Vector3 TouchPos1;  //для отработки мышки или прикосновения
	Vector3 TouchPos2;  //для отработки мышки или прикосновения
	Vector3 pointStartPos;
	Vector3 tempV3;
	GameObject point;
	GameObject pointControl;
	float Touch1Time;
	Ray ray;
	RaycastHit hit;
	bool RaycastF;
	float WheelAxis;


	// Use this for initialization
	void Start () {
		//Canvas.
		pointControl = new GameObject();
		pointControl = GameObject.Find("pfCanvas/pointControl1") as GameObject;
		//Debug.Log("start in controller " + pointControl.ToString());
		//pointControl = GameObject.FindGameObjectWithTag("points") as GameObject;
		if (pointControl == null) Debug.Log("not found");
			else pointControl.GetComponent<scrPointControl>().hideControl();
		BeginTouch1 = new Vector2();
		BeginTouch2 = new Vector2();
		pointStartPos = new Vector3();
		tempV3 = new Vector3();
		//WheelAxis = Input.mouseScrollDelta.y;
	}
	
	// Update is called once per frame
	void Update () {
		//отрабатываем нажатия на экран
		if (Input.mouseScrollDelta.y != 0){
			if (Camera.main.orthographic){
				if (((Camera.main.orthographicSize - Input.mouseScrollDelta.y)>15)&&
				    ((Camera.main.orthographicSize - Input.mouseScrollDelta.y)<50)){
					Camera.main.orthographicSize += - Input.mouseScrollDelta.y;
					//WheelAxis = Input.mouseScrollDelta.y;
				}
			}
			//Debug.Log(string.Format("wheel {0}",));
		}

		if ((Input.touchCount>0)||(Input.GetMouseButton(0))||(Input.GetMouseButtonUp(0))) {
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () || myGlobal.UIClick) {
				return;
			}
	    	if (Input.touchCount==1){ //одно нажатие - двигаем точку или управляем параметрами
				TouchPos1 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				switch (Input.GetTouch(0).phase) {
					case TouchPhase.Began:
						Touch1Type = 1; //считаем что попали в точку - проверим позднее
						break;
					case TouchPhase.Moved: //двигаем палец
						switch (Touch1Type){
						case 2: //нажали на точку
							Touch1Type = 3; //попали в точку и двигаем её
							break;
						case 4: //нажали мимо точки
							Touch1Type = 5; //попали мимо точки и двигаем мышь/палец
							break;
						default:
							Touch1Type = 1;
							break;
						}
						break;
					case TouchPhase.Stationary:
						break;
					case TouchPhase.Canceled:
						goto case TouchPhase.Ended;
						break;
					case TouchPhase.Ended:
						switch (Touch1Type){
						case 2:
							Touch1Type = 6;//show menu
							break;
						case 4:
							Touch1Type = 7;//show menu
							break;
						}
						Touch1Type = 0; //ввести тип 9 - окончание нажатия?
						break;
				}


				if (Touch1Type==0){ //до этого ничего не нажимали
					BeginTouch1 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
					Touch1Time = Time.time;

					ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
					RaycastF = Physics.Raycast(ray, out hit, 100f);
					//if (RaycastF) Debug.Log("raycast touch"+hit.collider.name);
					if (RaycastF&&(hit.collider.tag == "point")){ //проверяем что попали в гравиточку    если никуда не попали и долго держим - создать новую
						point = hit.collider.gameObject;
						pointStartPos = point.transform.position;
						Touch1Type = 1;       //movePoint = true;
						//отобразить меню точки
					} else {
							Touch1Type = 2;
							//cameraStartPos = camera.transform.position;
						}

				} 
			} else if (Input.touchCount==2){ //ничего не двигаем - масштабируем экран
					TouchPos1 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
					TouchPos2 = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
					Touch1Type = 9;
					//movePoint = false;
					/*if (Touch1Type != 5){ //до этого касались не двумя пальцами
						BeginTouch1 = Input.GetTouch(0).position;
						BeginTouch2 = Input.GetTouch(1).position;
						Touch1Type = 5;
					} else {

							//контроль мах и мин значаения
							BeginTouch1 = Input.GetTouch(0).position;
							BeginTouch2 = Input.GetTouch(1).position; 
						}*/
		  		} 
			if (Input.GetMouseButton(0)){
				TouchPos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if (Input.GetMouseButtonDown(0)){
					Touch1Type = 1; //куда-то нажали
				} else
				if ((BeginTouch1-TouchPos1).sqrMagnitude>0.1){//двигаем мышь
					Debug.Log("mb pressed");
					switch (Touch1Type){
					case 2: //нажали на точку
						Touch1Type = 3; //попали в точку и двигаем её
						break;
					case 4: //нажали мимо точки
						Touch1Type = 5; //попали мимо точки и двигаем мышь/палец
						break;
					}
				}
			} else if (Input.GetMouseButtonUp(0)){
				switch (Touch1Type){
				case 2:
					Touch1Type = 6;//show menu
					break;
				case 4:
					Touch1Type = 7;//show menu
					break;
				}
				//Touch1Type = 0; //ввести тип 9 - окончание нажатия?
			}
			//Debug.Log(string.Format("type {0}",Touch1Type));
			//main DO case
			switch (Touch1Type){ //должо выполняться при хотябы одном нажатии
			case 1: //нажали 1 пальцем
				BeginTouch1 = TouchPos1;
				Touch1Time = Time.time;
				//ray = Camera.main.ScreenPointToRay(TouchPos1);
				ray = new Ray(TouchPos1, Vector3.forward);
				RaycastF = Physics.Raycast(ray, out hit, 110f);
				if (RaycastF) Debug.Log("raycast mouse"+hit.collider.name);
				//if (RaycastF)Debug.Log(string.Format("tag {0}",hit.collider.tag));
				if (RaycastF&&(hit.collider.tag == "points")){ //проверяем что попали в гравиточку    если никуда не попали и долго держим - создать новую
					//Debug.Log(string.Format("tag {0}",hit.collider.tag));
					point = hit.collider.gameObject;
					pointStartPos = point.transform.position;
					Touch1Type = 2; 
					//goto case 2;
					//отобразить меню точки
				} else {
							Touch1Type = 4;
							goto case 4;
						}

				break;
			case 3: //нажали в точку и двигали
				tempV3 = TouchPos1 - BeginTouch1;
				if (tempV3.sqrMagnitude > 0.03f) {
					//Camera.main.ScreenToViewportPoint();
					point.transform.position = point.transform.position + tempV3;
					BeginTouch1 = TouchPos1;
				}
				break;
			case 4: //Нажали в пустое место скрыть меню точки если открыто
				pointControl.GetComponent<scrPointControl>().hideControl();
				break;
			case 5: //нажали мимо точки и двигали
				tempV3 = TouchPos1 - BeginTouch1;
				if (tempV3.sqrMagnitude > 0.1f) {
					Debug.Log(string.Format("move cam"));// {0}",tempV3));
					GetComponent<Camera>().transform.position = GetComponent<Camera>().transform.position - tempV3;
					//BeginTouch1 = TouchPos1;
					BeginTouch1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);//need case touch or mouse
				}
				break;
			case 6: //show menu point
				//Debug.Log("show menu point");
				if (pointControl != null){
					//Debug.Log("call control"+point.name);
					pointControl.GetComponent<scrPointControl>().showControl(point);
				}
				Touch1Type = 0; //ввести тип 9 - окончание нажатия?
				break;
			case 7: //show menu screen
				Debug.Log("show menu screen");
				Touch1Type = 0; //ввести тип 9 - окончание нажатия?
				break;
			case 8:
				Debug.Log("Touch1Type 8");
				break;
			case 9:
				if (Camera.current.orthographic){
					Camera.current.fieldOfView = Camera.current.fieldOfView*(BeginTouch1 - BeginTouch2).magnitude/(Input.GetTouch(0).position-Input.GetTouch(1).position).magnitude;
				}
				break;
		}
	} else if (Input.touchCount==0){ //ничего не нажали или может только отпустили?
			//if ((Touch1Type==1)&&(Time.time-Touch1Time<1)){
			//show context menu1
			//}
			//Touch1Type = 0;
		}//touch=0
	}   //update
} //class

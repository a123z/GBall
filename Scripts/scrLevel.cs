using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class scrLevel : MonoBehaviour {
	public int levelNum=0; //номер уровня 0-стартовый экран
	public int graviPlus;  //сколько начислено бонусных гравитонов
	public int graviMinus; //сколько истрачено гравитонов за уровень
	public float HighSpeed = 5f;	//скорость выше которой насчитываются бонусы
	public float LargeHeight = 40f;  //высота выше которой насчитываются бонусы
	public int[] przCnt;   //кол-во призов при первом проходе уровня
	/*public GameObject Point0Prefab;
	public GameObject Point1Prefab;
	public GameObject Point2Prefab;*/
	public GameObject PrefabPrize;

	// Use this for initialization
	void Start () {
		//print(Application.persistentDataPath.ToString());
		Debug.Log("scrLevel start " + levelNum.ToString());
		//if ((levelNum==0)||myGlobal.gameData==null){ //если первый уровень или данные ещё не загружены
		if (myGlobal.gameData == null){ //если данные ещё не загружены
			myGlobal.Init(); //инициализируем массивы данных
			myGlobal.LoadLevelsFromFile(myGlobal.saveFileName); //заполняем данными из файла сохранения
		}
		//Debug.Log(przCnt.Length.ToString());
		//if (myGlobal.gameData.levels[levelNum].prizeCount == null) myGlobal.gameData.levels[levelNum].prizeCount = przCnt;
		loadLevelData(); //загружаем сцену из массива данных
		myGlobal.UIClick = false; 
		
		myGlobal.StartLevelTime = Time.realtimeSinceStartup;//запоминаем время для рассчёта времени прохождения
		Debug.Log("start time "+myGlobal.StartLevelTime.ToString());

		if (myGlobal.gameData.gr<=0) myGlobal.gameData.gr = 300; //for debug

		//отключим видимость зон для размещения призов (они должны быть видимы только на этапе создания уровня)
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("prizeArea")){ 
			g.GetComponent<MeshRenderer>().enabled = false;
		}


	}

	/*void OnGUI(){
		GUI.Label(new Rect(40,60,400,80),Application.persistentDataPath.ToString());
	}*/

	// Update is called once per frame
	void Update () {
	
	}

	public void loadNextLevel(){
		saveLevelData();
		myGlobal.loadLevel(levelNum+1);
		//SceneManager.LoadScene("level" + (levelNum+1).ToString());
	}

	public void saveLevelData(){
		
		//Debug.Log(string.Format("asds {0} lvl len ={1}   {2}  {3}",ggg.GetLength(0),myGlobal.levels.GetLength(0),levelNum, myGlobal.levels[levelNum]==null ));
		//Debug.Log(string.Format("asds {0}",);
		if (myGlobal.gameData.levels[levelNum] == null) {
			myGlobal.gameData.levels[levelNum] = new scrClasses.Level();
		}
		myGlobal.gameData.levels[levelNum].graviMinus = 0;
		myGlobal.gameData.levels[levelNum].graviPlus = 0;
		//myGlobal.gameData.levels[levelNum].passed = false;
		GameObject[] ggg = GameObject.FindGameObjectsWithTag("points");
		myGlobal.gameData.levels[levelNum].points = new scrClasses.Point[ggg.GetLength(0)];
		int i_=0;
		foreach (GameObject g in ggg){
			myGlobal.gameData.levels[levelNum].points[i_] = new scrClasses.Point(g.transform.position,g.GetComponent<pointScript>().gravity, g.GetComponent<pointScript>().pointType);
			i_++;
		}
		//обнулим кол-во призов в массиве
		Debug.Log( myGlobal.gameData.levels[levelNum].prizeCount.GetLength(0).ToString());
		for (i_=0; i_<myGlobal.gameData.levels[levelNum].prizeCount.GetLength(0);i_++){
			myGlobal.gameData.levels[levelNum].prizeCount[i_] = 0;
		}
		//посчитаем кол-во призов на сцене и запишем в массив
		ggg = GameObject.FindGameObjectsWithTag("prize");
		foreach (GameObject g in ggg){
			myGlobal.gameData.levels[levelNum].prizeCount[g.GetComponent<scrPrize>().prizeType]++;
		}

	}

	void loadLevelData(){
		if (myGlobal.gameData.specGrCount == null) myGlobal.gameData.specGrCount = new int[10] {0,10,10,0,0,0,0,0,0,0};
		if (myGlobal.gameData.levels[levelNum] != null) {
			for (int i_=0; i_<myGlobal.gameData.levels[levelNum].points.GetLength(0); i_++){
				//if (myGlobal.gameData.levels[levelNum].points[i_] != null){
					GameObject g = Instantiate(
										((GameObject) Resources.Load(myGlobal.pointPrefabName[myGlobal.gameData.levels[levelNum].points[i_].pType])),
										//((GameObject) UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+myGlobal.pointPrefabName[myGlobal.gameData.levels[levelNum].points[i_].pType]+".prefab", typeof(GameObject))),
										myGlobal.gameData.levels[levelNum].points[i_].getPos(),
										Quaternion.identity
									) as GameObject;
					g.GetComponent<pointScript>().gravity = myGlobal.gameData.levels[levelNum].points[i_].GraviMass; 
					g.GetComponent<pointScript>().pointType = myGlobal.gameData.levels[levelNum].points[i_].pType; 
				//}
			}
			if (myGlobal.gameData.levels[levelNum].prizeCount != null){ //есть данные о призах
				for (int i_=0; i_<myGlobal.gameData.levels[levelNum].prizeCount.Length-1; i_++){ //для каждого типа призов
					for (int i2_= 0; i2_ < myGlobal.gameData.levels[levelNum].prizeCount[i_];i2_++){ //создадим указанное кол-во призов
						GameObject po = GameObject.Instantiate(PrefabPrize);
						po.GetComponent<scrPrize>().prizeType = i_;
					}
				}
			} else {
				Debug.Log("prizeCount is null");
				//данные о призах отсутствует - создадим данные по призам
				myGlobal.gameData.levels[levelNum].prizeCount = przCnt;
			}
		} else {
			Debug.Log("Level is null");
			//если уровень отсутствует - т.е. его раньше не проходили - создадим данные по уровню
			myGlobal.gameData.levels[levelNum] = new scrClasses.Level();
			myGlobal.gameData.levels[levelNum].prizeCount = przCnt;//new int[10]  {0,2,1,0,0,0,0,0,0,0};
		}
	}
		
	/*public void SaveLevel(){
		saveLevelData();
		myGlobal.SaveLevelsToFile(myGlobal.saveFileName);
	}*/

	public void GameExit(){ //вызывается по кнопке выход в UI
		Application.Quit();
	}

	void OnApplicationQuit() { //unity event
		//SaveLevelsToFile(myGlobal.saveFileName);
		//SaveLevel();
		saveLevelData();
		myGlobal.SaveLevelsToFile(myGlobal.saveFileName);
	}


}

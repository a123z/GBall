using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class scrLevel : MonoBehaviour {
	public int levelNum=0; //номер уровня 0-стартовый экран
	public int graviPlus;  //сколько начислено бонусных гравитонов
	public int graviMinus; //сколько истрачено гравитонов за уровень
	public float HighSpeed = 5f;	//скорость выше которой насчитываются бонусы
	public float LargeHeight = 40f;  //высота выше которой насчитываются бонусы
	public int[] przCnt;
	/*public GameObject Point0Prefab;
	public GameObject Point1Prefab;
	public GameObject Point2Prefab;*/
	public GameObject PrefabPrize;

	// Use this for initialization
	void Start () {
		Debug.Log("scrLevel start");
		if ((levelNum==0)||myGlobal.gameData==null){
			myGlobal.Init();
			LoadLevelsFromFile(myGlobal.saveFileName);
			if (myGlobal.gameData.levels.Length < myGlobal.levelsCount){
				myGlobal.gameData.levels = new scrClasses.Level[myGlobal.levelsCount];
			}
		}
		loadLevelData();
        myGlobal.UIClick = false;
		//print(Application.persistentDataPath.ToString());
		myGlobal.StartLevelTime = Time.realtimeSinceStartup;

		Debug.Log("start time "+myGlobal.StartLevelTime.ToString());
		if (myGlobal.gameData.gr<=0) myGlobal.gameData.gr = 300; //for debug

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("prizeArea")){
			g.GetComponent<MeshRenderer>().enabled = false;
		}
		//if (PrefabPrize != null)GameObject.Instantiate(PrefabPrize);

	}

	/*void OnGUI(){
		GUI.Label(new Rect(40,60,400,80),Application.persistentDataPath.ToString());
	}*/

	// Update is called once per frame
	void Update () {
	
	}

	public void loadNextLevel(){
		saveLevelData();
        //Application.LoadLevel(levelNum + 1);
		SceneManager.LoadScene("level" + (levelNum+1).ToString());
        //SceneManager.LoadScene(levelNum + 1);
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
		for (i_=0; i_<myGlobal.gameData.levels[levelNum].prizeCount.Length;i_++){
			myGlobal.gameData.levels[levelNum].prizeCount[i_] = 0;
		}
		ggg = GameObject.FindGameObjectsWithTag("prize");
		foreach (GameObject g in ggg){
			myGlobal.gameData.levels[levelNum].prizeCount[g.GetComponent<scrPrize>().prizeType]++;
		}

	}

	void loadLevelData(){
		if (myGlobal.gameData.specGrCount == null) myGlobal.gameData.specGrCount = new int[10] {0,10,10,0,0,0,0,0,0,0};
		if (myGlobal.gameData.levels[levelNum] != null) {
			for (int i_=0; i_<myGlobal.gameData.levels[levelNum].points.GetLength(0); i_++){
				GameObject g = Instantiate(
									((GameObject) Resources.Load(myGlobal.pointPrefabName[myGlobal.gameData.levels[levelNum].points[i_].pType])),
									//((GameObject) UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+myGlobal.pointPrefabName[myGlobal.gameData.levels[levelNum].points[i_].pType]+".prefab", typeof(GameObject))),
									myGlobal.gameData.levels[levelNum].points[i_].getPos(),
									Quaternion.identity
								) as GameObject;
				g.GetComponent<pointScript>().gravity = myGlobal.gameData.levels[levelNum].points[i_].GraviMass; 
				g.GetComponent<pointScript>().pointType = myGlobal.gameData.levels[levelNum].points[i_].pType; 
			}
			if (myGlobal.gameData.levels[levelNum].prizeCount != null){
				for (int i_=0; i_<myGlobal.gameData.levels[levelNum].prizeCount.Length-1; i_++){
					for (int i2_= 0; i2_ < myGlobal.gameData.levels[levelNum].prizeCount[i_];i2_++){
						GameObject po = GameObject.Instantiate(PrefabPrize);
						po.GetComponent<scrPrize>().prizeType = i2_;
					}
				}
			} else {
				Debug.Log("prizeCount is null");
				myGlobal.gameData.levels[levelNum].prizeCount = przCnt;
			}
		} else {
			Debug.Log("Level is null");
			myGlobal.gameData.levels[levelNum] = new scrClasses.Level();
			myGlobal.gameData.levels[levelNum].prizeCount = przCnt;//new int[10]  {0,2,1,0,0,0,0,0,0,0};
		}
	}
		
	void OnApplicationQuit() {
		saveLevelData();
		SaveLevelsToFile(myGlobal.saveFileName);
	}
	
	void LoadLevelsFromFile(string aFileName){
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fs = new FileStream(Application.persistentDataPath + aFileName, FileMode.OpenOrCreate);
		myGlobal.gameData = (scrClasses.GameData)formatter.Deserialize(fs);
		//Debug.Log(string.Format("len = {0}|{1}",myGlobal.levels.GetLength(0),myGlobal.levels[0].points.GetLength(0)));
		fs.Close();
		
		Debug.Log("Deserialization finished");
	}
	
	void SaveLevelsToFile(string aFileName){
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fs = new FileStream(Application.persistentDataPath + aFileName, FileMode.OpenOrCreate);
		formatter.Serialize(fs, myGlobal.gameData);
		fs.Close();
		
		Debug.Log("Serialization finished");
	}

	public void SaveLevel(){
		saveLevelData();
		SaveLevelsToFile(myGlobal.saveFileName);
	}

	public void GameExit(){
		Application.Quit();
	}

}

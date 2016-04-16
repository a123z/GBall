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
	public GameObject PointPrefab;


	// Use this for initialization
	void Start () {
		Debug.Log("scrLevel start");
		if ((levelNum==0)||myGlobal.gameData==null){
			myGlobal.Init();
			LoadLevelsFromFile(myGlobal.saveFileName);
			if (myGlobal.gameData.levels.Length<myGlobal.levelsCount){
					myGlobal.gameData.levels = new scrLevel.Level[myGlobal.levelsCount];
			}
		}
		loadLevelData();
        myGlobal.UIClick = false;
		print(Application.persistentDataPath.ToString());
		myGlobal.StartLevelTime = Time.realtimeSinceStartup;
		Debug.Log("start time "+myGlobal.StartLevelTime.ToString());
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
        SceneManager.LoadScene(levelNum + 1);
    }

	public void saveLevelData(){
		GameObject[] ggg = GameObject.FindGameObjectsWithTag("points");
		//Debug.Log(string.Format("asds {0} lvl len ={1}   {2}  {3}",ggg.GetLength(0),myGlobal.levels.GetLength(0),levelNum, myGlobal.levels[levelNum]==null ));
		//Debug.Log(string.Format("asds {0}",);
		if (myGlobal.gameData.levels[levelNum] == null) {
			myGlobal.gameData.levels[levelNum] = new Level();
		}
		myGlobal.gameData.levels[levelNum].graviMinus = 0;
		myGlobal.gameData.levels[levelNum].graviPlus = 0;
		myGlobal.gameData.levels[levelNum].passed = false;
		myGlobal.gameData.levels[levelNum].points = new Point[ggg.GetLength(0)];
		int i_=0;
		foreach (GameObject g in ggg){
			myGlobal.gameData.levels[levelNum].points[i_] = new Point(g.transform.position,g.GetComponent<pointScript>().gravity);
			i_++;
		}
	}

	void loadLevelData(){
		if (myGlobal.gameData.levels[levelNum] != null) {
			for (int i_=0; i_<myGlobal.gameData.levels[levelNum].points.GetLength(0); i_++){
				GameObject g = Instantiate(PointPrefab,myGlobal.gameData.levels[levelNum].points[i_].getPos(),Quaternion.identity) as GameObject;
				g.GetComponent<pointScript>().gravity = myGlobal.gameData.levels[levelNum].points[i_].GraviMass; 
			}
		}
	}

	[System.Serializable]
	public class Point
	{
		float _x {get; set;}
		float _y {get; set;}
		float _z {get; set;}
		public float GraviMass;
		public bool enabled;
		
		//constructor
		public Point(Vector3 pos, float gravi)
		{
			_x = pos.x;
			_y = pos.y;
			_z = pos.z;
			GraviMass = gravi;
		}
		
		public float getGraviForce(){
			return (GraviMass * 5f);
		}
		
		public void setPos(Vector3 position){
			_x = position.x;
			_y = position.y;
			_z = position.z;
			//return (new Vector3(_x, _y, _z));
		}
		
		public Vector3 getPos(){
			return (new Vector3(_x, _y, _z)); 
		}
	}
	
	[System.Serializable]
	public class Level
	{
		public Point[] points;
		public bool passed;
		//public int score;
		public int graviPlus;
		public int graviMinus;
	}

	[System.Serializable]
	public class GameData
	{
		public int score;
		public int lastLevel;
		public int gr;
		public Level[] levels;
	}
	
	void OnApplicationQuit() {
		saveLevelData();
		SaveLevelsToFile(myGlobal.saveFileName);
	}
	
	void LoadLevelsFromFile(string aFileName){
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fs = new FileStream(Application.persistentDataPath + aFileName, FileMode.OpenOrCreate);
		myGlobal.gameData = (GameData)formatter.Deserialize(fs);
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

	public void GameExit(){
		Application.Quit();
	}

}

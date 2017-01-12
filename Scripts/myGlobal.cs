using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class myGlobal{
	//public static int lastLevel;
	public static int levelsCount=11;
	//public static int score;
	//public static int GPower;
	//public static bool soundOn = true;
	public static int soundVolume;
	public static int musicVolume;
	public static bool UIClick;
	//public static scrLevel.Level[] levels;
	public static scrClasses.GameData gameData;
	public static string saveFileName="points.dat";
	public static string[] pointPrefabName = new string[10] {"pfPoint0","pfPoint1","pfPoint2","","","","","","",""};
	//public static int Gr; 
	public static int deltaGr=1;
	public static float StartLevelTime=0;
	public static string goLevelName = "goLevel";
	//public static bool noChangeAfterTeleport = false;
	public static scrLevel currentLevel;
	const int GrDefault=100; //кол-во гравитонов по умолчанию


	//static bool _musicOn;

	public static void Start(){
		//levels = new SaveLoad.Level[levelsCount];


	}

	public static void Init(){
		UnityEngine.Debug.Log("init");
		gameData = new scrClasses.GameData();
		gameData.levels = new scrClasses.Level[myGlobal.levelsCount];
		gameData.specGrCount = new int[10] {0,0,0,0,0,0,0,0,0,0};
		gameData.gr = GrDefault;
		//for (int i=0;i<levelsCount;i++){
		//	myGlobal.levels[i] = new scrLevel.Level();
		//}
	}

	public static void musicOn(bool On){
		/*foreach (Gameobject go in FindObjects()) {
			foreach (object o in go.GetComponents("Sound")) do{
				if (o.Name=="Sound"){
					o.enable = On;
				}
			} //components
		} //objects*/
	}

	public static void SoundOn(bool On){
		gameData.soundOn = On;
	}

	public static bool getSound(){
		return gameData.soundOn;
	}

	public static void SetGr(int value=GrDefault){
		gameData.gr = value;
	}
	
	public static void ClearGameProgress(int levelNum=0){
		if (levelNum==0){
			gameData.levels = null;
		} else {
			gameData.levels[levelNum] = null;
		}
		SetGr();
		
	}

	public static void loadLevel(int lvlNo){
		//SceneManager.LoadScene(lvlNo + 1);
		SceneManager.LoadScene("level" + (lvlNo).ToString());
		//Debug.Log("obj name "+this.name);
	}

	public static void LoadLevelSelect(){
		SceneManager.LoadScene("levelSelect");
	}


	public static void LoadLevelsFromFile(string aFileName){
		BinaryFormatter formatter = new BinaryFormatter();

		if (File.Exists(Application.persistentDataPath + aFileName)){
			FileStream fs = new FileStream(Application.persistentDataPath + aFileName, FileMode.Open);
			try {
				gameData = (scrClasses.GameData)formatter.Deserialize(fs);
			}
			catch(Exception ex){
				Debug.Log("При десериализации ошибка: "+ex.Message);
			}
			finally{
				fs.Close();
			}
		}

		for (int i=0;i<levelsCount-1;i++){ //если не получилось загрузить - создадим пустые уровни
			if (gameData.levels[i] == null)	gameData.levels[i] = new scrClasses.Level();
		}

		//Debug.Log(string.Format("len = {0}|{1}",myGlobal.levels.GetLength(0),myGlobal.levels[0].points.GetLength(0)));



		Debug.Log("Deserialization finished");

		//проверим что у нас массив с уровнями не меньше заявленного если нет увеличим
		//надо на этапе разработки - когда добавляются новые уровни
		if (myGlobal.gameData.levels.Length < myGlobal.levelsCount){
			myGlobal.gameData.levels = new scrClasses.Level[myGlobal.levelsCount];
		}
	}

	public static void SaveLevelsToFile(string aFileName){
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fs = new FileStream(Application.persistentDataPath + aFileName, FileMode.OpenOrCreate);
		formatter.Serialize(fs, gameData);
		fs.Close();
		
		Debug.Log("Serialization finished");
	}


}
using UnityEngine;
using UnityEngine.SceneManagement;
//#if unity_android 
using UnityEngine.Advertisements;
//#endif
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
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
	public static bool ShowFinish = false;
	public static int lastTutorStep = -1;
	public static float timeFromLastAd=0;
	//static bool _musicOn;
	public static scrClasses.LocalizationTxt LocalizationData;
	/*public static string txtBtnStart;
	public static string txtBtnExit;
	public static string txtBtnAbout;
	public static string txtAbout;
	public static string txtFinish1;
	public static string txtFinish2;
	public static string txtFinish3;
	public static string txtFinish4;
	public static string txtFinish5;
	public static string txtFinish6;
	public static string txtResult1;
	public static string txtResult2;
	public static string txtResult3;
	public static string txtResult4;
	public static string txtResult5;*/


	//=========================Private variable
	static float AdRepeatTime = 600f;



	public static void Init(){
		UnityEngine.Debug.Log("init");
		gameData = new scrClasses.GameData();
		gameData.levels = new scrClasses.Level[myGlobal.levelsCount];
		gameData.specGrCount = new int[10] {0,0,0,0,0,0,0,0,0,0};
		gameData.gr = GrDefault;
		LocalizationData = new scrClasses.LocalizationTxt();
		#if !UNITY_ADS
		//#if unity_android 
			Advertisement.Initialize("1261310",false);
		#endif
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
		if (lvlNo == 0)SceneManager.LoadScene("start");
		else {
			ShowAd();
			SceneManager.LoadScene("level" + (lvlNo).ToString());
		}
		//Debug.Log("obj name "+this.name);
	}

	public static void LoadLevelSelect(){
		SceneManager.LoadScene("levelSelect");
	}


	public static void LoadLevelsFromFile(string aFileName){
		BinaryFormatter formatter = new BinaryFormatter();

		if (File.Exists(Application.persistentDataPath + aFileName)){
			FileStream fs = new FileStream(Application.persistentDataPath + "\\" + aFileName, FileMode.Open);
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
		FileStream fs = new FileStream(Application.persistentDataPath + "\\" + aFileName, FileMode.OpenOrCreate);
		formatter.Serialize(fs, gameData);
		fs.Close();
		
		Debug.Log("Serialization finished");
	}

	public static void ShowAd(bool ShowNow = false){
		//#if unity_android
		#if UNITY_ADS
		Debug.Log("ad run!!!!!!!!!!!!!!");
		if (ShowNow) myGlobal.timeFromLastAd = Time.realtimeSinceStartup - AdRepeatTime - 1;
		if (Time.realtimeSinceStartup-myGlobal.timeFromLastAd > AdRepeatTime){
			if (Advertisement.isInitialized && Advertisement.IsReady())
			{
				Advertisement.Show();
			}
			myGlobal.timeFromLastAd = myGlobal.timeFromLastAd;
		}
		#endif
	}

	public static void LoadLocalization(string aFileName){
		//Stream reader = new MemoryStream(
		Stream reader = new FileStream(Application.persistentDataPath +"\\"+ aFileName, FileMode.Open);
		XmlSerializer serial = new XmlSerializer(typeof(scrClasses.LocalizationTxt));
		LocalizationData = (scrClasses.LocalizationTxt)serial.Deserialize(reader);

	}

	public static void LoadLocalization(TextAsset lngXmlData){
		//lngXmlData.text
		Stream reader = new MemoryStream(lngXmlData.bytes);

		//Stream reader = new FileStream(Application.persistentDataPath +"\\"+ aFileName, FileMode.Open);
		XmlSerializer serial = new XmlSerializer(typeof(scrClasses.LocalizationTxt));
		LocalizationData = (scrClasses.LocalizationTxt)serial.Deserialize(reader);
	}

	/*public static void LoadLocalization2(string aFileName){
		LocalizationData = Resources.GetBuiltinResource<scrClasses.LocalizationTxt>("rus.loc");

	}*/

	public static void SaveLocalization(string aFileName){
		//Stream saver = new FileStream(Application.persistentDataPath +"\\"+ aFileName, FileMode.CreateNew);
		Stream saver = new FileStream("\\Resources\\"+ aFileName, FileMode.CreateNew);
		XmlSerializer serial = new XmlSerializer(typeof(scrClasses.LocalizationTxt));
		serial.Serialize(saver, LocalizationData);
		//LocalizationData = (scrClasses.LocalizationTxt)serial.Deserialize(reader);
		//проверить Здесь - тут я остановился
	}

	public static void initLocalization(){
		if (myGlobal.gameData == null){ //если данные ещё не загружены
			myGlobal.Init(); //инициализируем массивы данных
			myGlobal.LoadLevelsFromFile(myGlobal.saveFileName); //заполняем данными из файла сохранения
		}

		if (myGlobal.gameData.lang == 0){
			switch (Application.systemLanguage){
				case SystemLanguage.Ukrainian: 
					goto case SystemLanguage.Russian;
					break;
				case SystemLanguage.Belarusian: 
					goto case SystemLanguage.Russian;
					break;
				case SystemLanguage.Russian: 
					LoadLocalization(Resources.Load<TextAsset>("Ru"));
					break;
				default: 
					LoadLocalization(Resources.Load<TextAsset>("En"));
					break;
					
			}
		} else switch (myGlobal.gameData.lang){
					case 2:
						LoadLocalization(Resources.Load<TextAsset>("Ru"));
						break;
					default: 
						LoadLocalization(Resources.Load<TextAsset>("En"));
						break;
				}
	}


}
using UnityEngine.SceneManagement;

public static class myGlobal{
	//public static int lastLevel;
	public static int levelsCount=10;
	//public static int score;
	public static int GPower;
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
	const int GrDefault=200; //кол-во гравитонов по умолчанию


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
		SceneManager.LoadScene("level" + (lvlNo+1).ToString());
		//Debug.Log("obj name "+this.name);
	}

	public static void LoadLevelSelect(){
		SceneManager.LoadScene("levelSelect");
	}

}
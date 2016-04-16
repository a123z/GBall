public static class myGlobal{
	//public static int lastLevel;
	public static int levelsCount=10;
	//public static int score;
	public static int GPower;
	public static bool soundOn;
	public static int soundVolume;
	public static int musicVolume;
	public static bool UIClick;
	//public static scrLevel.Level[] levels;
	public static scrLevel.GameData gameData;
	public static string saveFileName="points.dat";
	//public static int Gr; 
	public static int deltaGr=1;
	public static float StartLevelTime=0;
	const int GrDefault=200; //кол-во гравитонов по умолчанию


	static bool _musicOn;

	public static void Start(){
		//levels = new SaveLoad.Level[levelsCount];


	}

	public static void Init(){
		gameData = new scrLevel.GameData();
		gameData.levels = new scrLevel.Level[myGlobal.levelsCount];
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
}
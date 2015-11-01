public static class myGlobal{
	public static int lastLevel;
	public static int levelsCount=4;
	public static int score;
	public static int GPower;
	public static bool soundOn;
	public static int soundVolume;
	public static int musicVolume;
	public static bool UIClick;
	public static scrLevel.Level[] levels;
	public static string saveFileName="points.dat";

	static bool _musicOn;

	public static void Start(){
		//levels = new SaveLoad.Level[levelsCount];


	}

	public static void Init(){
		levels = new scrLevel.Level[myGlobal.levelsCount];
		myGlobal.levels[0] = new scrLevel.Level();
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
}
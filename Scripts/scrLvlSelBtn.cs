using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scrLvlSelBtn : MonoBehaviour {
	public int lvlNum=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadThisLevel(){
		Debug.Log("load level "+lvlNum.ToString());
		if (lvlNum>0 && (lvlNum==1 || myGlobal.gameData.levels[lvlNum-1].passed)) SceneManager.LoadScene("level" + (lvlNum).ToString());
	}
}

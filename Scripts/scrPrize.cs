using UnityEngine;
using System.Collections;

public class scrPrize : MonoBehaviour {
	public int prizeType=1;

	Material m;
	Material m2;
	float f;

	// Use this for initialization
	void Start () {
		Debug.Log("start prize");
		m = gameObject.transform.FindChild("Sphere").GetComponent<MeshRenderer>().material;
		switch (prizeType){
			case 1: m.color = new Color32(210,210,210,255); break;
			case 2: m.color = new Color32(20,180,190,255); break;
			default: m.color = new Color32(0,50,200,255); break;
		}
		m2 = gameObject.GetComponent<MeshRenderer>().material;
		SetPosition();
	}
	
	// Update is called once per frame
	void Update () {
		f += Time.deltaTime;
		m.mainTextureOffset = new Vector2(f/5, f/5);
		if (f>5) f = 0;
		//Debug.Log("ma " + m.name + (new Vector2((Time.deltaTime % 1), 0)).ToString());
	}

	void OnTriggerEnter(Collider col){
		Debug.Log(col.gameObject.name + " " + col.gameObject.tag);
		if (col.gameObject.tag == "ball" && myGlobal.gameData != null) {
			myGlobal.gameData.specGrCount[prizeType] ++;
			Destroy(gameObject, 0.2f);
		}
	}

	void OnTriggerStay(Collider col){
		Debug.Log(col.gameObject.name + " trig by " + gameObject.name);
	}

	void OnCollisionStay(Collision col){
		Debug.Log(col.gameObject.name + " coll by " + gameObject.name);
	}

	void SetPosition(){
		GameObject[] gg = GameObject.FindGameObjectsWithTag("prizeArea");
		Debug.Log("tot="+gg.Length.ToString());
		if (gg.Length>0){
			
			Random.InitState(Mathf.RoundToInt(Time.realtimeSinceStartup));
			int gn = Random.Range(0, gg.Length);
			Debug.Log("gn="+gn.ToString()+" gg(x)="+gg[gn].transform.position.x.ToString()+" tot="+gg.Length.ToString());
			transform.position = new Vector3(gg[gn].transform.position.x + Random.Range(0, gg[gn].transform.localScale.x) - Mathf.Round(gg[gn].transform.localScale.x/2),
				gg[gn].transform.position.y + Random.Range(0, gg[gn].transform.localScale.y) - Mathf.Round(gg[gn].transform.localScale.y/2), 0);
		}
	}
}

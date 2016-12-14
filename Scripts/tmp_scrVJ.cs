using UnityEngine;
using System.Collections;

public class tmp_scrVJ : MonoBehaviour {
	public Sprite JoystickCenter;
	public Sprite JoystickBG;

	public Vector2 CenterSize;
	public Vector2 BGSize;

	GameObject goCenter;
	GameObject goBG;

	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<Canvas>() != null){
			Debug.Log("type is canvas");
			goBG = new GameObject();
			goBG.transform.SetParent(gameObject.transform);
			goBG.name = "goVJbackground";
			goBG.AddComponent<RectTransform>().sizeDelta = BGSize;
			goBG.AddComponent<CanvasRenderer>();
			goBG.AddComponent<UnityEngine.UI.Image>().sprite = JoystickBG;



			goCenter = new GameObject();
			goCenter.transform.SetParent(gameObject.transform);
			goCenter.name = "goVJcenter";
			goCenter.AddComponent<RectTransform>().sizeDelta = CenterSize;
			goCenter.AddComponent<CanvasRenderer>();
			goCenter.AddComponent<UnityEngine.UI.Image>().sprite = JoystickCenter;

		} else Debug.Log("type is not canvas");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class scrTutor : MonoBehaviour {
	public Sprite[] TutorSprites;
	int lastStep ;

	// Use this for initialization
	void Start () {
		lastStep = 0;
		transform.Find("imgTutor").GetComponentInChildren<UnityEngine.UI.Image>().sprite = TutorSprites[lastStep];

	}
	
	// Update is called once per frame
	void Update () {
		//if (gameObject.activeSelf && (Input.GetMouseButton(0)||Input.touchCount>0)) gameObject.SetActive(false);
	}


	/// <summary>
	/// show stepNo Tutor sprite or lastStep-1 by default
	/// 0 is First Step
	/// </summary>
	/// <param name="stepNo"> Number of Sprite from array TutorSprites in scrTutor script.</param>
	public void showStep(int stepNo=-1){ //
		if (stepNo == -1){
			stepNo = lastStep;
		} 
		if (TutorSprites.Length-1 >= stepNo && TutorSprites[stepNo] != null){
			transform.Find("imgTutor").GetComponentInChildren<UnityEngine.UI.Image>().sprite = TutorSprites[stepNo];
			myGlobal.lastTutorStep = stepNo;
			lastStep = stepNo+1;
			gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// Closes the tutor`s picture if it`s Active.
	/// </summary>
	public void closeTutor(){
		if (gameObject.activeSelf) gameObject.SetActive(false);
	}

}

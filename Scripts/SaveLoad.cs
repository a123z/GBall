using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[System.Serializable]
	public class point
	{
		float _x {get; set;}
		float _y {get; set;}
		float _z {get; set;}
		public float GraviMass;
		public bool enabled;
		
		//constructor
		public point(Vector3 pos, float gravi)
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
	public class level
	{
		public point[] points;
		public bool passed;
		public int score;
	}

}

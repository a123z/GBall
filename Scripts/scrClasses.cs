﻿using UnityEngine;
using System.Collections;

public class scrClasses// : MonoBehaviour
{
	[System.Serializable]
	public class Point
	{
		float _x {get; set;}
		float _y {get; set;}
		float _z {get; set;}
		public float GraviMass;
		public bool enabled;
		public int pType;

		//constructor
		public Point(Vector3 pos, float gravi, int pointType=0)
		{
			_x = pos.x;
			_y = pos.y;
			_z = pos.z;
			GraviMass = gravi;
			pType = pointType;
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
	public class Level
	{
		public Point[] points;
		public bool passed;
		//public int highScore;
		public int Score;
		public int[] prizeCount;
		//public int usedGr;

		public int graviPlus;
		public int graviMinus;

		public Level(){
			passed = false;
			//highScore = 0;
			Score = 0;
			graviPlus = 0;
			graviMinus = 0;
			points = new Point[0];
			prizeCount = new int[0]; // № элемента массива - тип приза, значение элемента - кол-во призов
		}
	}

	[System.Serializable]
	public class GameData
	{
		public int score;
		public int lastLevel;
		public int gr;
		public Level[] levels;
		public int[] specGrCount; //points count: 0-base graviti 1-Anti gravity 2-pulse gravity 3-inverse gravity 
		public bool soundOn;
		public bool musicOn;
		public int lang = 0;
		//public int aGrCount;  //Anti gravity points count
		//public int pGrCount;  //pulse gravity points count
		//public int iGrCount;  //inverse gravity points count
		//or need use array????

	}

	[System.Serializable]
	public class LocalizationTxt
	{
		public string txtBtnStart;
		public string txtBtnExit;
		public string txtBtnAbout;
		public string txtAbout;
		public string txtFinish1;
		public string txtFinish2;
		public string txtFinish3;
		public string txtFinish4;
		public string txtFinish5;
		public string txtFinish6;
		public string txtResult1;
		public string txtResult2;
		public string txtResult3;
		public string txtResult4;
		public string txtResult5;
		public string txtResult6;
	}
}


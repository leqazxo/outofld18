using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cvscSettings : MonoBehaviour 
{


	public static Dictionary<string, List<KeyCode>> keySets=  new Dictionary<string, List<KeyCode>>();

	void Start() //init
	{

		//Up
		List<KeyCode> keySet = new List<KeyCode>();
		keySet.Add(KeyCode.W);	keySet.Add(KeyCode.I); 
		keySets.Add("Up", keySet);
		//Down
		keySet = new List<KeyCode>();
		keySet.Add(KeyCode.S);	keySet.Add(KeyCode.K); 
		keySets.Add("Down", keySet);
		//Left
		keySet = new List<KeyCode>();
		keySet.Add(KeyCode.A);	keySet.Add(KeyCode.J); 
		keySets.Add("Left", keySet);
		//Right
		keySet = new List<KeyCode>();
		keySet.Add(KeyCode.D);	keySet.Add(KeyCode.L); 
		keySets.Add("Right", keySet);
		//Attack
		keySet = new List<KeyCode>();
		keySet.Add(KeyCode.Z);	keySet.Add(KeyCode.LeftBracket); 		
		keySets.Add("Attack", keySet);
		//Switch
		keySet = new List<KeyCode>();
		keySet.Add(KeyCode.X);	keySet.Add(KeyCode.RightBracket); 		
		keySets.Add("Switch", keySet);
		//Buff
		keySet = new List<KeyCode>();
		keySet.Add(KeyCode.C);	keySet.Add(KeyCode.Backslash); 
		keySets.Add("Buff", keySet);
		
	}



}




















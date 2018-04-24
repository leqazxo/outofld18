using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cvscNuke : MonoBehaviour {

	public float dmg;
	public float dmg1;
	public float dmg2;
	public int type;//0:neutral 1:light 2:dark //Granst inmmunity

	float countNuke = 2.0f;

	public cvscNuke(float d, float d1, float d2, int tp)
	{
		dmg = d;
		dmg1 = d1;
		dmg2 = d2;
		type = tp;
	}


	void Start ()
	{
		//Deal Damage

		//Expire
	}

}

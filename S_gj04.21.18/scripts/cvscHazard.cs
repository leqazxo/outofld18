using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class cvscHazard : MonoBehaviour {

	public float dmg;
	public float dmg1;
	public float dmg2;
	public int type;//0:neutral 1:light 2:dark //Granst inmmunity

	public float slow; 

	public float timeToDie;

	public cvscHazard(float d, float d1, float d2, int tp, float sl, float ttd)
	{
		dmg = d;
		dmg1 = d1;
		dmg2 = d2;
		type = tp;
		slow = sl;
		timeToDie = ttd;
	}



}

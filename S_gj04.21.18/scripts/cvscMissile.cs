using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cvscMissile : MonoBehaviour {

	public float speed= 5.0f;

	public float dmg;
	public float dmg1;
	public float dmg2;
	public int type;//0:neutral 1:light 2:dark //Granst inmmunity

	public float timeToDie = 5.0f;

	public cvscMissile(float sp, float d, float d1, float d2, int tp)
	{
		speed = sp;
		dmg = d;
		dmg1 = d1;
		dmg2 = d2;
		type = tp;
	}

	void OnTriggerEnter(Collider other) 
	{

		//Deal dmg ->//
		Destroy(this.gameObject);
	}

	void FixedUpdate()
	{

		transform.position = transform.position + 
				transform.up * speed * Time.fixedDeltaTime;

	}



}
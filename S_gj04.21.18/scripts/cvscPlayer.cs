using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cvscPlayer : MonoBehaviour 
{


	public float moveSpeed = 6;
	public GameObject boss;


	public int health = 100;
	public cvscWeapons[] inventory= new cvscWeapons[2];
	public bool isEvil;
	public int id;





	Rigidbody rigidbody;
	Vector3 velocity;



	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	void Update () {
		// Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		// transform.LookAt (mousePos + Vector3.up * transform.position.y);
		// velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;
		// if (velocity != Vector3.zero && action.CurrentAction != (int)ActionIs.Walk)
		// 	action.CurrentAction = (int)ActionIs.Walk;
		// else if(velocity == Vector3.zero && action.CurrentAction != (int)ActionIs.Stop)
		// 	action.CurrentAction = (int)ActionIs.Stop;	
	}

	void FixedUpdate() {
		int axisx = Convert.ToInt32(Input.GetKey(cvscSettings.keySets["Left"][id]) ^ 
				Input.GetKey(cvscSettings.keySets["Right"][id])) + 
				Convert.ToInt32(Input.GetKey(cvscSettings.keySets["Left"][id])) * (-2);
		int axisz = Convert.ToInt32(Input.GetKey(cvscSettings.keySets["Up"][id]) ^ 
				Input.GetKey(cvscSettings.keySets["Down"][id])) +
				Convert.ToInt32(Input.GetKey(cvscSettings.keySets["Down"][id])) * (-2);
		


		velocity = new Vector3 (axisx, 0, axisz).normalized * moveSpeed;
		// Debuglog(cvscSettings.keySets["Left"][id])


		transform.LookAt (boss.transform);
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}

}

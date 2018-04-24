using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cvscWeapons : MonoBehaviour 
{

	public List<cvscWeapon> weapons;

	public class cvscWeapon
	{

		int baseDamage;
		int bonusDamage1;
		int bonusDamage2;
		bool isRanged; //slot qualifier

		public cvscWeapon(int bD, int bD1, int bD2, bool isR){
			baseDamage= bD;
			bonusDamage1= bD1;
			bonusDamage2= bD2;
			isRanged= isR;
		}
	}

	void Start (){ //init

		//Starting Weapons
			weapons.Add(new cvscWeapon(100,0,0,false));
			weapons.Add(new cvscWeapon(20,0,0,true));
	}

}

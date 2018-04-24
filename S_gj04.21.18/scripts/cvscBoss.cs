using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class cvscBoss : MonoBehaviour 
{

	public GameObject[] players = new GameObject[2];

	public float cooldown = 2.0f;
	public float turnSpeed = 15.0f;


	public float hazardDmg;
	public float hazardDmg1;
	public float hazardDmg2;
	public int hazardType;//0:neutral 1:light 2:dark //Granst inmmunity

	public GameObject hazardPrefab;
	public GameObject moldPrefab;
	public GameObject shadowNukePrefab;
	public GameObject nukePrefab;
	public GameObject missilePrefab;


	public float timeObserve = 1.0f;


	int status = 0; 


	//Aim to prison a player


	public IEnumerator WallAttack (GameObject targetPlayer) 
	{

		float traySpeed = 20.0f;

		float safeSpace = 4.0f;

		
 
 		//Look for the guy
 		Vector3 dirUnMemory = targetPlayer.transform.position;
        while(true)
        {

			Vector3 dir = (targetPlayer.transform.position - transform.position).normalized;
        	Quaternion rotTo = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed);

            dirUnMemory = targetPlayer.transform.position - dirUnMemory;

            if(Vector3.Angle(transform.forward, dir) < 1)
                break;
 
            yield return null;
        }


        //Observe & Predict
        float countTime = 0.0f;
        List<Vector3> movMemory = new List<Vector3>();
        Vector3 dirMemory = targetPlayer.transform.position;
        while ( countTime < timeObserve ){
			Vector3 dir = (targetPlayer.transform.position - transform.position).normalized;
        	Quaternion rotTo = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed);
 			
            dirMemory = targetPlayer.transform.position - dirMemory;

            countTime += Time.deltaTime;

            yield return null;
        }


        //Start tracing the hazard wall
        float turnMultiplier = 2.0f;
        RaycastHit hit1, hit2; // The order is important
		Vector3 nearPoint = (transform.position - targetPlayer.transform.position).normalized 
							* safeSpace + targetPlayer.transform.position;
		Vector3 defDir = dirMemory.Equals(Vector3.zero) ? dirUnMemory : dirMemory;

		int layerWall = 1 << 12; // cvscWall layer
		Physics.Raycast(nearPoint, defDir, out hit1, Mathf.Infinity, layerWall);
		Physics.Raycast(nearPoint, -defDir, out hit2, Mathf.Infinity, layerWall);
       	Vector3 reachDir = (hit1.point - transform.position).normalized;
        
        //Debug
		// GameObject hazard = Instantiate(hazardPrefab);
		// hazard.transform.position = nearPoint;
		// hazard.transform.LookAt(transform);
		// hazard = Instantiate(hazardPrefab);
		// hazard.transform.position = hit1.point;
		// hazard = Instantiate(hazardPrefab);
		// hazard.transform.position = hit2.point;

			// Cover the angle
        while(Vector3.Angle(transform.forward, reachDir)!=0)
        {
        	Quaternion rotTo = Quaternion.LookRotation(reachDir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed * turnMultiplier);
 
            yield return null;
        }

        	//FIRE!
        reachDir = (hit2.point - transform.position).normalized;
        float scaleSpeed = 0.5f;
        		// Mold
        		float magniHazard = (hit2.point - hit1.point).magnitude; // Measure
		        GameObject mold = Instantiate(moldPrefab);
		        mold.transform.position = hit1.point;
		        mold.transform.LookAt(hit2.point);
		        mold.transform.localScale = new Vector3 (0.1f,2.0f,magniHazard);
		        mold.transform.position = Vector3.MoveTowards(hit1.point,
		        	hit2.point,magniHazard/2.0f);

		int layerMold = 1 << 14;
        GameObject hazard = Instantiate(hazardPrefab);
        // Init nuke
        hazard.transform.position = hit1.point;
        hazard.transform.LookAt(hit2.point); 
        Vector3	lastHazard = hit1.point;
        while(Vector3.Angle(transform.forward, reachDir)!=0)
	    {
        	Quaternion rotTo = Quaternion.LookRotation(reachDir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed * turnMultiplier * 0.75f);

            //Build passed stations ->//
			float spaceBetweenStations = 1.0f;

            RaycastHit hitCurrent;
            Physics.Raycast(transform.position, transform.forward, 
            	out hitCurrent, Mathf.Infinity, layerMold);

            int missedStations = Mathf.FloorToInt(
            		(hitCurrent.point-lastHazard).magnitude / spaceBetweenStations);
           	
           	for (int i=0; i<missedStations; i++)
           	{
            	hazard = Instantiate(hazardPrefab);
            	//Init cvscNuke ->//
		        hazard.transform.position = 
		        	Vector3.MoveTowards(lastHazard, hit2.point, spaceBetweenStations);;
		        hazard.transform.LookAt(hit2.point); 
		        lastHazard = hazard.transform.position;
            }

            yield return null;
        }
	}




	public IEnumerator NukeAttack (GameObject targetPlayer)
	{

		float turnMultiplier = 1.25f;

		//Look for the guy
 		Vector3 dirUnMemory = targetPlayer.transform.position;
        while(true)
        {

			Vector3 dir = (targetPlayer.transform.position - transform.position).normalized;
        	Quaternion rotTo = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed * turnMultiplier);

            dirUnMemory = targetPlayer.transform.position - dirUnMemory;

            if(Vector3.Angle(transform.forward, dir) < 1)
                break;
 
            yield return null;
        }

        //Observe & Predict & Throw
        float countTime = 0.0f;

        List<Vector3> movMemory = new List<Vector3>();
        Vector3 dirMemory = targetPlayer.transform.position;
        while ( countTime < timeObserve*1.25f ){
			Vector3 dir = (targetPlayer.transform.position - transform.position).normalized;
        	Quaternion rotTo = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed);
 			
            dirMemory = targetPlayer.transform.position - dirMemory;

            countTime += Time.deltaTime;

            yield return null;
        }


		//Nuke falling
		float anticipation = 1.0f;
		Vector3 defDir = dirMemory.Equals(Vector3.zero) ? dirUnMemory : dirMemory;
		GameObject nukeShadow = Instantiate(shadowNukePrefab);
        nukeShadow.transform.position = targetPlayer.transform.position + 
        	defDir.normalized * anticipation;
		float timeFalling = 0.0f;
		float timeToFall = 1.5f;
		while ( timeFalling < timeToFall )
		{
			timeFalling += Time.deltaTime;
			yield return null;
		}
		
		GameObject nuke = Instantiate(nukePrefab);
		//InitNuke //
		nuke.transform.position = nukeShadow.transform.position;
		Destroy(nukeShadow);
	}



	public IEnumerator MissilesConeAttack (GameObject targetPlayer)
	{

		float turnMultiplier = 1.5f;

		//Look for the guy
 		Vector3 dirUnMemory = targetPlayer.transform.position;
        while(true)
        {

			Vector3 dir = (targetPlayer.transform.position - transform.position).normalized;
        	Quaternion rotTo = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed * turnMultiplier);

            dirUnMemory = targetPlayer.transform.position - dirUnMemory;

            if(Vector3.Angle(transform.forward, dir) < 1)
                break;
 
            yield return null;
        }

		//Observe & Predict
        float countTime = 0.0f;
        List<Vector3> movMemory = new List<Vector3>();
        Vector3 dirMemory = targetPlayer.transform.position;
        while ( countTime < timeObserve*1.25f ){
			Vector3 dir = (targetPlayer.transform.position - transform.position).normalized;
        	Quaternion rotTo = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed);
 			
            dirMemory = targetPlayer.transform.position - dirMemory;

            countTime += Time.deltaTime;

            yield return null;
        }

        
        
        float anticipation = 2.0f;
		Vector3 defDir = dirMemory.Equals(Vector3.zero) ? dirUnMemory : dirMemory;
		Vector3 pointToShoot = targetPlayer.transform.position + 
				defDir.normalized * anticipation;
		//Quick turn
        while(true)
        {

			Vector3 dir = (pointToShoot - transform.position).normalized;
        	Quaternion rotTo = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed * turnMultiplier * 5.0f);

            if(Vector3.Angle(transform.forward, dir) < 1)
                break;
 
            yield return null;
        }

        float maxAngle = 60.0f;
        int density = 7;
        //Fire missiles

        	float streamDuration = 3.5f;
        	float streamCount = 0.0f;
        	float rateStream = 0.35f;
        	float countRate = 0.36f;
        	//Missiles stream
        while(streamCount<streamDuration)
        {


        	if(rateStream < countRate){

        		float firstAngle = transform.eulerAngles.y + 180 - maxAngle * 0.5f;
        		float stepEuler = maxAngle/density;
        		float rotationInst;
	        	//Columns
		        for(int i=0; i<density; i++)
		        {
		        	
		        	GameObject missile = Instantiate(missilePrefab);
		        	missile.transform.position = transform.position;
		        	missile.transform.rotation *= Quaternion.Euler( 0, 0, firstAngle + i*stepEuler);
		        }

		        countRate = 0.0f;
		    }
		    countRate += Time.deltaTime;

	        streamCount += Time.deltaTime;

	        yield return null;
	    } 

	}




	public IEnumerator Rest () 
	{


		GameObject targetPlayer = players[0];
        while(true)
        {

			Vector3 dir = (targetPlayer.transform.position - transform.position).normalized;
        	Quaternion rotTo = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, rotTo,
                Time.deltaTime * turnSpeed);


            // if(Vector3.Angle(transform.forward, dir) < 1)
            //     break;
 
            yield return null;
        }
	}






	void Start ()
	{

		// StartCoroutine("WallAttack", players[0]);
		// StartCoroutine("NukeAttack", players[0]);
		StartCoroutine("MissilesConeAttack", players[0]);
		// StartCoroutine("Rest");
	}










	void Update ()
	{

		 
	}













}









































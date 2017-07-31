using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControll : MonoBehaviour {

	// Break out multiplyer for speed to the inspector
	public float droneSpeed;

	
	// Update is called once per frame
	void Update () {

		// Use IJKL keys to drive drone... I would like good drone physics here and you know..
		// collisions and stuff, and thrust based rigid body physics and tilting.. but no.
		// for now noclip.exe will have to do.

		if (Input.GetKey(KeyCode.J)){ // Left
			transform.Translate(-1f*droneSpeed*Time.deltaTime,0f,0f);
		}

		if (Input.GetKey(KeyCode.L)){ // Right
			transform.Translate(+1f*droneSpeed*Time.deltaTime,0f,0f);
		}

		if (Input.GetKey(KeyCode.I)){ // forward
			transform.Translate(Vector3.forward * droneSpeed  *Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.K)){ // backward
			transform.Translate(-Vector3.forward * droneSpeed * Time.deltaTime);
		}

		// U and O to go up and down...

		if (Input.GetKey(KeyCode.U)){ // Up
			transform.Translate(Vector3.up * droneSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.O)){ // down
			transform.Translate(Vector3.down * droneSpeed * Time.deltaTime);
		}
		
	}


}

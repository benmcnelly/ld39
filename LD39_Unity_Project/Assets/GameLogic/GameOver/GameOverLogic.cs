using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit ();
		}

		if (Input.GetKeyUp(KeyCode.X))
		{
			SceneManager.LoadScene ("theLevel", LoadSceneMode.Single);
		}

	}
}




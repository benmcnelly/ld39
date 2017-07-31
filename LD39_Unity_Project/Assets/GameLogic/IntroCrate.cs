using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCrate : MonoBehaviour {

	// Intro Audio
	public AudioClip intro;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.PlayOneShot(intro, 1.5F);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.X))
		{
			Destroy (gameObject);
		}
		
	}
}

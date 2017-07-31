using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishAnimation : MonoBehaviour {

	public Animation animation;
	public AudioClip DishMovingSound;
	public AudioClip Ending;
	AudioSource audioSource;



	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		animation = GetComponent<Animation>();
	}


	public void playAnimation (){
		Debug.Log ("I got this far");		
		animation.Play ();
		audioSource.PlayOneShot(DishMovingSound, 1F);
		audioSource.PlayOneShot(Ending, 0.7F);

	}

}

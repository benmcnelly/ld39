using System.Collections;//
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class glowLogic : MonoBehaviour {

	// Glow (power) level defaults exposed to editor
	public float glowLevel; // Current Glow Level
	public float minGlow; // You die below this...
	public float maxGlow; // 100% Glow is this value
	public float boosterGlow; // How much boosters help
	public float sapGlow; // How much traps hurt

	// Need to always be slowly draining glow, even faster if sprinting
	public float drainGlowSpeed;
	public float jumpGlowDrain;
	public bool isRunning;

	// Sound Effects
	public AudioClip glowHit;
	public AudioClip glowBigPowerUp;
	public AudioClip glowSmallgPowerUp;
	public AudioClip glowboxPickup;
	public AudioClip launchGLowstick;
	AudioSource audioSource;

	// Special Audio cues
	public AudioClip intro;
	public AudioClip halfHealthWarning;
	public bool warned;

	// Glowstick Logic
	public int glowstickCount = 0;
	public int glowstickPickupAmount;
	public float throwSpeed;
	public Rigidbody glowstick;
	public Transform spawnpoint;

	// Testing
	public DishAnimation dishanimation;

	public Slider glowBar;
	public Text glowStickUpdate;

	// Use this for initialization
	void Start () {
		// Init things
		audioSource = GetComponent<AudioSource>();

		// glowLevel = maxGlow;
		glowstickCount = 0;
		glowStickUpdate.text = "You have: " + glowstickCount;

		// Get initial glow status
		glowBar.value = CalculateGlow();

		// Set sprint glow bleed to false
		isRunning = false;

		// Set 50% Warning to false
		warned = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			SceneManager.LoadScene ("gameOver");
		}

		if (glowLevel > maxGlow) {
			glowLevel = maxGlow;
		}

		if (Input.GetKey(KeyCode.W))
		{
			Draining ();
		}

		// Throw a glow stick...
		if (Input.GetKeyUp(KeyCode.E) & glowstickCount > 0)
		{
			Rigidbody clone;
			clone = (Rigidbody)Instantiate (glowstick, spawnpoint.position, glowstick.rotation);
			clone.velocity = spawnpoint.TransformDirection (Vector3.forward*throwSpeed);
			audioSource.PlayOneShot(launchGLowstick, 0.9F);
			glowstickCount -= 1;
			glowStickUpdate.text = "You have: " + glowstickCount;
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			glowLevel -= jumpGlowDrain;
			glowBar.value = CalculateGlow();
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			isRunning = true;
		} else {
			isRunning = false;
		}

		// Warnings and death!

		if (glowLevel < maxGlow / 2 & warned != true) {
			Debug.Log ("Half way dead!");
			Warning ();
		} 
			

		if (glowLevel < minGlow)
		{
			Death();
		}
	}

	void Warning () {
		warned = true;
		audioSource.PlayOneShot(halfHealthWarning, 0.9F);
	}
		

	void Draining () {
		if (isRunning == true) {
			glowLevel -= drainGlowSpeed * 2;
			glowBar.value = CalculateGlow();
		} else {
			glowLevel -= drainGlowSpeed;
			glowBar.value = CalculateGlow();
		}
	}

	float CalculateGlow()
	{
		return glowLevel / maxGlow;
	}

	void OnTriggerEnter(Collider col) {
		if(col.tag == "glowRefill") {
			Debug.Log("Max Refill!");
			audioSource.PlayOneShot(glowBigPowerUp, 0.9F);
			glowLevel = maxGlow;
			glowBar.value = CalculateGlow();
		}

		if(col.tag == "glowBoost") {
			Debug.Log("moar glow");
			Destroy (col.gameObject);
			audioSource.PlayOneShot(glowSmallgPowerUp, 0.9F);
			glowLevel += boosterGlow;
			glowBar.value = CalculateGlow();
		}

		if (col.tag == "glowCrate") {
			Debug.Log ("Got a crate!");
			Destroy (col.gameObject);
			glowstickCount += glowstickPickupAmount;
			audioSource.PlayOneShot (glowboxPickup, 0.9f);
			glowStickUpdate.text = "You have: " + glowstickCount;
		}

		if(col.tag == "glowSap") {
			Debug.Log("wups,there goes some glow");
			Destroy (col.gameObject);
			audioSource.PlayOneShot(glowHit, 0.7F);
			glowLevel -= sapGlow;
			glowBar.value = CalculateGlow();
		}

		if(col.tag == "Damage") {
			Debug.Log("less glow");
		}
	}

	void OnTriggerStay(Collider col) {

		if (col.tag == "WinInteraction") {
			Debug.Log("You can hit E now");
			// Display text like "Press E to Interact"
		}

		if (col.tag == "WinInteraction" & Input.GetKeyUp(KeyCode.E)) {
			Debug.Log("Arrr, you pressed E to interact Cap'n");
			Destroy (col.gameObject);
			// Call animations and sounds from Dish Animation Method
			dishanimation.playAnimation();
			// trigger EndGame();
		}
	}



	public void Death () {
		// Load Game Over....
		SceneManager.LoadScene ("death", LoadSceneMode.Single);
	}

	public void EndGame () {
		// Load Winner Winner Space Dinner....
		Debug.Log("You Won!"); // Actually just going to let them exit when ready since they got left on the planet
	}

}




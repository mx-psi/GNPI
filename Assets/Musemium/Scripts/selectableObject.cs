using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Class for selectable capsules in main scene
 */
public class selectableObject : MonoBehaviour {
	private Transform transf; // capsule Transform
	public string sceneToLoad; // name of the scene to load
	public GameObject anim; // UI animation to load
	private float startTimer; // Timer start time
	private float loadingTime; // Time to load scene in seconds
	private int totalCollisions; // number of collisions so far

	/*
     * Initialize transform, total loading time and total collisions
     */
	void Start () {
		transf = this.GetComponent<Transform> ();
		loadingTime = 2.0f;
		totalCollisions = 0;
	}
	
	/*
     * Animate capsule rotation
     */
	void Update () {
		transf.Rotate (0, 45 * Time.deltaTime, 0, Space.World); // rotation in degrees per second
	}

    /*
     * If a collision is detected, start timer and animation
     */
	private void OnTriggerEnter(Collider other) {
		if (totalCollisions == 0) {
			anim.SetActive (true);	// start animation
			startTimer = Time.time; // save time
		}
		totalCollisions++;
	}

    /*
     * If a collision is mantained for more than loadingTime, load scene.
     */
	private void OnTriggerStay(Collider other) {
		if (Time.time - startTimer > loadingTime && sceneToLoad.Length != 0) {
			SceneManager.LoadScene (sceneToLoad);
		}
	}

    /*
     * If a collision is ended before loadingTime seconds pass, 
     * deactivate animation.
     */
	private void OnTriggerExit(Collider other) {
		if(totalCollisions == 1)
			anim.SetActive (false);
		totalCollisions--;
	}
}

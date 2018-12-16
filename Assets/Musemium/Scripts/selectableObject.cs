using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectableObject : MonoBehaviour {
	private Transform transf;
	public string sceneToLoad;
	public GameObject anim; // UI animation to load
	private float startTimer;
	private float loadingTime; // Time to load scene in seconds
	private int totalCollisions;

	// Use this for initialization
	void Start () {
		transf = this.GetComponent<Transform> ();
		loadingTime = 2.0f;
		totalCollisions = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transf.Rotate (0, 45 * Time.deltaTime, 0, Space.World); // rotation in degrees per second
	}


	private void OnTriggerEnter(Collider other) {
		if (totalCollisions == 0) {
			anim.SetActive (true);	
			startTimer = Time.time;
		}
		totalCollisions++;
	}

	private void OnTriggerStay(Collider other) {
		if (Time.time - startTimer > loadingTime && sceneToLoad.Length != 0) {
			SceneManager.LoadScene (sceneToLoad);
		}
	}

	private void OnTriggerExit(Collider other) {
		if(totalCollisions == 1)
			anim.SetActive (false);
		totalCollisions--;
	}
}

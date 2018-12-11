using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectableObject : MonoBehaviour {
	private Transform transf;
	public string sceneToLoad;
	public GameObject animation; // UI animation to load
	private float startTimer;
	private float loadingTime; // Time to load scene in seconds

	// Use this for initialization
	void Start () {
		transf = this.GetComponent<Transform> ();
		loadingTime = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		transf.Rotate (0, 45 * Time.deltaTime, 0, Space.World); // rotation in degrees per second
	}


	private void OnTriggerEnter(Collider other) {
		animation.SetActive (true);
		startTimer = Time.time;
	}

	private void OnTriggerStay(Collider other) {
		if(Time.time - startTimer > loadingTime)
			SceneManager.LoadScene(sceneToLoad);
	}

	private void OnTriggerExit(Collider other) {
		animation.SetActive (false);
	}
}

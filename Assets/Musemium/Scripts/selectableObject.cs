using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectableObject : MonoBehaviour {
	private Transform transf;
	public string sceneToLoad;

	// Use this for initialization
	void Start () {
		transf = this.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		transf.Rotate (0, 45 * Time.deltaTime, 0, Space.World); // rotation in degrees per second
	}


	private void OnTriggerEnter(Collider other) {
		Debug.Log ("Colision detectada, cambiando de escena");
		SceneManager.LoadScene(sceneToLoad);
	}
}

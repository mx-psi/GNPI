using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectableObject : MonoBehaviour {
	private Transform transform;
	private CapsuleCollider collider;

	// Use this for initialization
	void Start () {
		transform = this.GetComponent<Transform> ();
		collider = this.GetComponent<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rotationAngle = new Vector3 (1, 0, 1);
		transform.Rotate (0, 45 * Time.deltaTime, 0, Space.World); // rotation in degrees per second
	}


	private void OnTriggerEnter(Collider other) {
		Debug.Log ("Colision detectada, cambiando de escena");
		SceneManager.LoadScene("testScene");
	}
}

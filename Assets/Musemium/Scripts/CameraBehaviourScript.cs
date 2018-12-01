using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour {

	public float cameraMovementSpeed = 1;
	public float cameraZoomSpeed = 1;
	public GameObject mapa;

	// Use this for initialization
	void Start () {
		Debug.Log ("Instrucciones actuales: Flechas para mover la cámara. Click izquierdo/derecho para aumentar/reducir zoom");
	}
	
	// Update is called once per frame
	void Update () {
		movement();

	}


	void movement() {
		// Gestión del movimiento de la cámara
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		transform.Translate(
			h * cameraMovementSpeed * Time.deltaTime,
			v * cameraMovementSpeed * Time.deltaTime,
			0
		);

		// Gestión del zoom
		bool leftClicked = Input.GetButton("Fire1");
		bool rightClicked = Input.GetButton("Fire2");

		if (leftClicked) {
			transform.Translate (
				0,
				0,
				cameraZoomSpeed * Time.deltaTime
			);
		} else if (rightClicked) {
			transform.Translate (
				0,
				0,
				-cameraZoomSpeed * Time.deltaTime
			);
		}
			
		// Restricting position 
		float xPos = mapa.transform.position.x;
		float yPos = mapa.transform.position.y;

		Vector3 clampedPosition = new Vector3 (
			Mathf.Clamp (transform.position.x, xPos - 7, xPos + 7),
			Mathf.Clamp (transform.position.y, yPos - 5, yPos + 5),
			Mathf.Clamp (transform.position.z, -10, -3)
		);

		transform.position = clampedPosition;
	}
}

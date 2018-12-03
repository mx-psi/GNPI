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

    /* Translate horizontally (h,v) */
    void translate(float h, float v)
    {
        transform.Translate(
            h * cameraMovementSpeed * Time.deltaTime,
            v * cameraMovementSpeed * Time.deltaTime,
            0
        );
    }

    /* Zoom in/out h units*/
    void zoom(float h)
    {
      transform.Translate(0, 0, h* Time.deltaTime);
    }

	void movement() {
		// Gestión del movimiento de la cámara
		translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		

		// Gestión del zoom
		bool leftClicked = Input.GetButton("Fire1");
		bool rightClicked = Input.GetButton("Fire2");

		if (leftClicked)
            zoom(1);
		else if (rightClicked)
            zoom(-1);
			
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

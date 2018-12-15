using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintableQuad : MonoBehaviour {
	Renderer m_Renderer;
	private float startTimer;
	private float waitTime;

	void OnDestroy() {
		Destroy (m_Renderer);
		Destroy (GetComponent<Renderer>());
	}

	// Use this for initialization
	void Start () {
		m_Renderer = GetComponent<Renderer>();
		startTimer = Time.time;
		waitTime = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		//Debug.Log ("Painted");
		if (Time.time - startTimer > waitTime)
		if (m_Renderer.material.color != null)
			m_Renderer.material.color = ColorController.CurrentColor ();
		else 
			Debug.Log ("Color = NULL");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintableQuad : MonoBehaviour {
	Renderer m_Renderer;
	private float startTimer;
	private float waitTime;

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
			m_Renderer.material.color = Color.blue;
	}
}

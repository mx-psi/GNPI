using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintableQuad : MonoBehaviour {
	Renderer m_Renderer;

	// Use this for initialization
	void Start () {
		m_Renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		//Debug.Log ("Painted");
		m_Renderer.material.color = new Color(Random.value,Random.value,Random.value);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bongoScript : MonoBehaviour {
	AudioSource audioData;

	// Use this for initialization
	void Start () {
		audioData = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		//Debug.Log("Bonged");
		audioData.Play (0);
	}
}

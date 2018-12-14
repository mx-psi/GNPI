using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintableQuad : MonoBehaviour {
	Renderer m_Renderer;
	static int currentColor;
	static private List<Color> colors;
	private float startTimer;
	private float waitTime;

	// Use this for initialization
	void Start () {
		m_Renderer = GetComponent<Renderer>();
		startTimer = Time.time;
		waitTime = 3.0f;
		colors = new List<Color> ();
		colors.Add (Color.blue);
		colors.Add (Color.red);
		colors.Add (Color.yellow);
		currentColor = 0;
	}

	// Change current color to next one
	public static void nextColor(){
		currentColor = (currentColor + 1) % colors.Count;
	}

	// Change current color to previous one
	public static void previousColor(){
		currentColor = (currentColor - 1) % colors.Count;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		//Debug.Log ("Painted");
		if (Time.time - startTimer > waitTime)
			m_Renderer.material.color = colors[currentColor];
	}
}

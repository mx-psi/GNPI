using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasTimerDisableScript : MonoBehaviour {
	private float startTimer;
	public float timer;
	private float fadeOutSpeed;

	// Use this for initialization
	void Start () {
		startTimer = Time.time;
		fadeOutSpeed = 0f;
	}
	
	// Update is called once per frame
    /*
     * Fades out object
     */
	void Update () {
		if (Time.time - startTimer > timer) {
			this.GetComponent<CanvasGroup> ().alpha = Mathf.SmoothDamp(this.GetComponent<CanvasGroup>().alpha, 0f,ref fadeOutSpeed,1f);
			if (this.GetComponent<CanvasGroup> ().alpha < 0.001f)
				this.gameObject.SetActive (false);
		}
			
	}
}

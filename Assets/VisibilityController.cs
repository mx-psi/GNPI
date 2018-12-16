using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour {
	public int index;
	private static int currentModel = 0;
	private static int numOfModels = 3;
	private Renderer m_Renderer;
	private Vector3 initialPosition;
	private bool justBecameVisible = false;

	void Start()
	{
		m_Renderer = GetComponent<MeshRenderer>();
		initialPosition = this.transform.parent.position;
		Debug.Log (initialPosition);
		Update ();
	}

	// Toggle the Object's visibility
	void Update(){
		SetVisibility ();
		if (justBecameVisible) {
			this.transform.parent.position = initialPosition;
		}
	}

	void SetVisibility(){
		if (m_Renderer.enabled == false) {
			if (index == currentModel) {
				m_Renderer.enabled = true;
				justBecameVisible = true;
			} else {
				m_Renderer.enabled = false;
				justBecameVisible = false;
			}
		} else {
			m_Renderer.enabled = index == currentModel;
			justBecameVisible = false;
		}
	}

	public static void NextModel(){
		currentModel = (currentModel + 1) % numOfModels;
	}

	public static void PreviousModel(){
		currentModel = (currentModel + numOfModels - 1) % numOfModels;
	}

	void OnDestroy(){
		Destroy (m_Renderer);
		Destroy (GetComponent<MeshRenderer>());
	}
}

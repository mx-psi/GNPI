using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class GesturesController : MonoBehaviour {
	Controller controller;
	bool zoom = false;

	float distBase;
	float distRel;
	public Camera camera;

	void Start ()
	{
		controller = new Controller();
	}

	void Update ()
	{
		Frame frame = controller.Frame();

		if (frame.Hands.Count == 2) {
			Leap.HandList hands = frame.Hands;
			//(0,-1,0)
			float angle1 = Vector3.Angle(Vector3.down, hands [0].PalmNormal.ToUnity());
			float angle2 = Vector3.Angle(Vector3.down, hands [1].PalmNormal.ToUnity());

			if ((angle1 < 45) && (angle2 < 45)) {
				if (zoom == false) {
					zoom = true;
					distBase = hands [0].PalmPosition.DistanceTo (hands [1].PalmPosition);
				} else {
					distRel = hands [0].PalmPosition.DistanceTo (hands [1].PalmPosition) / distBase;
					Debug.Log (distRel);
					camera.transform.position /= (distRel-1)/5 + 1;
				}
			} else {
				zoom = false;
			}
		}
	}
}
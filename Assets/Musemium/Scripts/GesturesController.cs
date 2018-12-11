using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class GesturesController : MonoBehaviour {
	Controller controller;
	bool zoom = false;

	float distBase;
	float distRel;
	float suavizadoZoom = 5;
	public Camera camera_;
	Vector3 posCamara;

	void Start ()
	{
		controller = new Controller();
		posCamara = camera_.transform.position;
	}

	void Update ()
	{
		Frame frame = controller.Frame();
		string currentScene = SceneManager.GetActiveScene ().name;

		// GESTOS CON DOS MANOS

		if (frame.Hands.Count == 2) {
			Leap.HandList hands = frame.Hands;
			Hand hand0 = hands [0];
			Hand hand1 = hands [1];

			Finger index0 = hand0.Fingers [1];
			Finger index1 = hand1.Fingers [1];
			int numExtended = hand0.Fingers.Extended ().Count + hand0.Fingers.Extended ().Count;
			int numOfFingers = hand0.Fingers.Count + hand1.Fingers.Count;
			bool allExtended = numOfFingers == numExtended;

			if (currentScene == "mainScene") {

				// ZOOM

				float angle1 = Vector3.Angle (Vector3.down, hand0.PalmNormal.ToUnity ());
				float angle2 = Vector3.Angle (Vector3.down, hand1.PalmNormal.ToUnity ());

				if ((angle1 < 45) && (angle2 < 45) && allExtended) {
					if (zoom == false) {
						zoom = true;
						distBase = hand0.StabilizedPalmPosition.DistanceTo (hand1.StabilizedPalmPosition);
					} else {
						distRel = hand0.StabilizedPalmPosition.DistanceTo (hand1.StabilizedPalmPosition) / distBase;
						Vector3 camPos = posCamara;
						camPos.z /= (distRel - 1) / suavizadoZoom + 1;
						camera_.transform.position = camPos;
					}
				} else {
					if (zoom == true) {
						posCamara = camera_.transform.position;
					}
					zoom = false;
				}
			} else {

				// SALIR
				Vector index0center = index0.Bone (Leap.Bone.BoneType.TYPE_PROXIMAL).NextJoint;
				Vector index1center = index1.Bone (Leap.Bone.BoneType.TYPE_PROXIMAL).NextJoint;
				float  index01distance= index0center.DistanceTo (index1center);
				float minCrossAngle = 1;
				float maxCrossAngle = 2;
				float triggerDist = 45;

				if (index0.IsExtended && index1.IsExtended && (numExtended == 2)) {
					float crossAngle = index0.Direction.AngleTo (index1.Direction);
					if ((crossAngle > minCrossAngle) && (crossAngle < maxCrossAngle) &&  index01distance < triggerDist) {
						SceneManager.LoadScene ("mainScene");
					}
				}
			}
		}
	}
}
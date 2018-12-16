using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Leap;

/*
 * Leap gestures management
 */
public class GesturesController : MonoBehaviour {
	Controller controller; // Main controller
	private bool zoom = false; // whether we are zooming right now

	private float baseDist;
	private float relDist;
	private float zoomSmoothing = 5; // zoom smoothing parameter

	public Camera camera_; //camera
	private Vector3 cameraPos; //camera position

	private bool handIsMoving = false; // whether hand is moving

    /*
     * Initialize controller and camera position
     */
	void Start() {
		controller = new Controller();
		cameraPos = camera_.transform.position;
	}

	/*
	 *  Map navigation gesture
	 */
	void MapNavigation(float x, float y){
		float leftRighthreshold = 200;
		float upThreshold = 300;
		float downThreshold = 100;
		float step = 0.03f;

		// moving left
		if ((x < -leftRighthreshold) && (y > downThreshold) && (y < upThreshold)) {
			cameraPos.x -= step;
			camera_.transform.position = cameraPos;
		} 
		// moving right
		else if ((x > leftRighthreshold) && (y > downThreshold) && (y < upThreshold)) { 
			cameraPos.x += step;
			camera_.transform.position = cameraPos;
		} 
		// moving down
		else if ((y < downThreshold)) {
			cameraPos.y -= step;
			camera_.transform.position = cameraPos;
		}
		// moving up
		else if ((y > upThreshold)) {
			cameraPos.y += step;
			camera_.transform.position = cameraPos;
		}
	}

	/*
	 *  Next gesture
	 */
	void Next(string currentScene){
		if (currentScene == "paintingScene") { // if scene is paintingScene update color
			ColorController.NextColor ();
		} else { // if scene is sculptureScene update color
			VisibilityController.NextModel ();
		}
	}

	/*
	 *  Previous gesture
	 */
	void Previous(string currentScene){
		if (currentScene == "paintingScene") { // if scene is paintingScene update color
			ColorController.PreviousColor ();
		} else { // if scene is sculptureScene update color
			VisibilityController.PreviousModel ();
		}
	}

	/*
	 *  Zoom gesture
	 */
	void Zoom(Hand hand0, Hand hand1){
		if (zoom == false) {
			zoom = true; // Start zooming
			baseDist = hand0.StabilizedPalmPosition.DistanceTo (hand1.StabilizedPalmPosition);
		} else { // Zoom in
			relDist = hand0.StabilizedPalmPosition.DistanceTo (hand1.StabilizedPalmPosition) / baseDist;
			Vector3 camPos = cameraPos;
			camPos.z /= (relDist - 1) / zoomSmoothing + 1;
			camera_.transform.position = camPos;
		}
	}

	/*
	 *  Zoom gesture
	 */
	void Exit(){
		SceneManager.LoadScene ("mainScene");
	}

    /*
     * Check for gestures on each frame
     */
	void Update (){
		Frame frame = controller.Frame(); //Frame with hands
		string currentScene = SceneManager.GetActiveScene ().name; // Current scene name

		// One-handed gestures

		if (frame.Hands.Count == 1) {
			Leap.HandList hands = frame.Hands;
			Hand hand0 = hands [0];

            // Main scene gestures
			if (currentScene == "mainScene") {
				float maxRadius = 50;
				float x = hand0.PalmPosition.ToUnity ().x;
				float y = hand0.PalmPosition.ToUnity ().y;
				// If fist is closed
				if (hand0.SphereRadius < maxRadius) {
					MapNavigation (x,y);
				}
			} 
            // Painting and sculpture gestures
			else if ((currentScene == "paintingScene") || (currentScene == "sculptureScene")){
				float minDotProd = 0.85f;
				float velocityThreshold = 1000;

                // If hand is tilted
				if (Mathf.Abs (Vector3.Dot (hand0.PalmNormal.ToUnity (), Vector3.left)) > minDotProd) {
					if (hand0.PalmVelocity.x < -velocityThreshold) { // if moving left
						if (handIsMoving == false) {
							handIsMoving = true;
							Debug.Log ("Next");
							Next (currentScene);
						}
					} else if (hand0.PalmVelocity.x > velocityThreshold) { // if moving right
						if (handIsMoving == false) {
							handIsMoving = true;
							Debug.Log ("Previous");
							Previous (currentScene);
						}
					} else {
						handIsMoving = false;
					}
				} else {
					handIsMoving = false;
				}
			}
		}

		// Two-handed gestures
		else if (frame.Hands.Count == 2) {
			Leap.HandList hands = frame.Hands;
			Hand hand0 = hands [0];
			Hand hand1 = hands [1];

			Finger index0 = hand0.Fingers [1];
			Finger index1 = hand1.Fingers [1];
            
            // number of extended fingers
			int numExtended = hand0.Fingers.Extended ().Count + hand0.Fingers.Extended ().Count;

            // number of fingers
			int numOfFingers = hand0.Fingers.Count + hand1.Fingers.Count;

            // are all fingers extended?
			bool allExtended = numOfFingers == numExtended;

			if (currentScene == "mainScene") {
				float angle1 = Vector3.Angle (Vector3.down, hand0.PalmNormal.ToUnity ());
				float angle2 = Vector3.Angle (Vector3.down, hand1.PalmNormal.ToUnity ());

                // If both hands are extended facing down
				if ((angle1 < 45) && (angle2 < 45) && allExtended) {
					Zoom (hand0, hand1);
				} else { // Stop zooming
					if (zoom == true) {
						cameraPos = camera_.transform.position;
					}
					zoom = false;
				}
			} else {

				// exit to main scene
				Vector index0center = index0.Bone (Leap.Bone.BoneType.TYPE_PROXIMAL).NextJoint;
				Vector index1center = index1.Bone (Leap.Bone.BoneType.TYPE_PROXIMAL).NextJoint;
				float  index01distance= index0center.DistanceTo (index1center);
				float minCrossAngle = 1;
				float maxCrossAngle = 2;
				float triggerDist = 45;
				// If both index fingers are extended
				if (index0.IsExtended && index1.IsExtended && (numExtended == 2)) {
					float crossAngle = index0.Direction.AngleTo (index1.Direction);
					// If both fingers are close and approximately form a perpendicular angle
					if ((crossAngle > minCrossAngle) && (crossAngle < maxCrossAngle) &&  index01distance < triggerDist) {
						Exit ();
					}
				}
			}
		}
	}
}
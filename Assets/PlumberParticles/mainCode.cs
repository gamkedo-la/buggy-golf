using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mainCode : MonoBehaviour {

	private bool midMoving = false;







	//deactivate the not needed screens

	public GameObject[] section = new GameObject[11];

//	public Transform[] locations;
	public Transform[] locations = new Transform[11];

	//variables used to store the current and last screen
	public int currentScreen;
	public int lastScreen;
	public int twoScreensAgo;

	public int numberOfScreens;

	//this variable is used to store the next set
	public Transform nextPosition;

// variable to store the offset value of the nextPosition
	public Transform nextPositionLoc;
	public float nextPositionCameraLocX;
	public float nextPositionCameraLocY;
	public float nextPositionCameraLocZ;

	public Transform postition1;
	public Transform postition2;
	public Transform postition3;
	public Transform postition4;
	public Transform postition5;
	public Transform postition6;
	public Transform postition7;
	public Transform postition8;
	public Transform postition9;
	public Transform postition10;
	public Transform postition11;
	public Transform destination;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;



//	public Vector3 targPos = endMarker.position;

	void Start() {
		numberOfScreens = 9; //set this if the count of screen changes
		startTime = Time.time;
		journeyLength = Vector3.Distance(postition1.position, postition2.position);
		destination = nextPositionLoc;
		currentScreen = 0;
		twoScreensAgo = 5; //sloppy.  needed a number that wasnt going to be next or prev
		lastScreen = 5; //sloppy.  needed a number that wasnt going to be next or prev

	}

	public void updateScreen(){
		Debug.Log ("currentScreen is now" + currentScreen);
		Debug.Log ("lastscreen is now" + lastScreen);
//		Debug.Log ("nextscreen is now" + nextScreen);
//		currentScreen = 0;
//		nextScreen = currentScreen+1;
		lastScreen = currentScreen-1;
	}



	public void moveCameraPosition(){
		if (currentScreen < 9) {
			currentScreen++;
			Debug.Log ("<9 currentScreen is now " + currentScreen);
			//			nextPosition = section[currentScreen].position;
			nextPosition = postition1;

			//EDIT THE LOCATION WITH THE NEEDED CAMERA OFFSETS
			nextPositionCameraLocX = section[currentScreen].transform.position.x;
			nextPositionCameraLocY = section[currentScreen].transform.position.y;
			nextPositionCameraLocZ = section[currentScreen].transform.position.z;
			nextPositionCameraLocY = nextPositionCameraLocY + 7;
			nextPositionCameraLocZ = nextPositionCameraLocZ + -151;

			Debug.Log ("X,Y, and Z should be " + nextPositionCameraLocX + "," +nextPositionCameraLocY + "," + nextPositionCameraLocZ);



			nextPositionLoc.position = new Vector3 (nextPositionCameraLocX,nextPositionCameraLocY,nextPositionCameraLocZ);

			Debug.Log ("after editing the nextPositionLoc.position it is now " + nextPositionLoc.position);

			//			section[currentScreen].transform.position.y = 7;
			//			section[currentScreen].transform.position.z = -151;

			Debug.Log ("section[x] is " + section[currentScreen].transform.position);
			Debug.Log ("section[x]Loc is " + section[currentScreen].transform.position);

			if (currentScreen < 9) {
				//				destination = locations [currentScreen];  //original method for changing loc
				destination = nextPositionLoc;
				//				
			}
		}
		if (currentScreen == 9) {
			currentScreen = 0;
			Debug.Log ("==9 which screen is now" + currentScreen);
			//			destination = locations [currentScreen];
			destination = nextPositionLoc;
		}
	}

	/////////////////////////////////////////BASIC ROADMAP
	//get input from button to define currentScreen  nextScreen()
	//make sure next and previous locations are active
	//redefine destination to camera offset
	//set destination so camera moves


	public void goNext(){
		if (currentScreen >= 0) {
			currentScreen = currentScreen + 1;
		}
		if (currentScreen > numberOfScreens) {
			currentScreen = 0;
		}
		Debug.Log ("goNext is starting optimizeIt with " + currentScreen);

		optimizeIt ();
		defineNextCameraLoc ();
	}


	public void goPrev(){
		if(currentScreen <= numberOfScreens) {
			currentScreen = currentScreen-1;
		}
		if (currentScreen < 0) {
			currentScreen = numberOfScreens;
		} 
		Debug.Log ("goPrev is starting optimizeIt with " + currentScreen);
		optimizeIt ();
		defineNextCameraLoc ();
	}

	public void optimizeIt(){
		Debug.Log ("Optimize it thinks Currenctscreen is now" + currentScreen);

		section[currentScreen].SetActive(true);  //this is to be used to activate sections
		if(currentScreen != twoScreensAgo){
		section[twoScreensAgo].SetActive(false);  //this is to be used to deactivate sections
		}
		twoScreensAgo = lastScreen;
		lastScreen = currentScreen;


	}

	public void defineNextCameraLoc(){
		//EDIT THE LOCATION WITH THE NEEDED CAMERA OFFSETS
		nextPositionCameraLocX = section[currentScreen].transform.position.x;
		nextPositionCameraLocY = section[currentScreen].transform.position.y;
		nextPositionCameraLocZ = section[currentScreen].transform.position.z;
		nextPositionCameraLocY = nextPositionCameraLocY + 7;
		nextPositionCameraLocZ = nextPositionCameraLocZ + -151;

//		Debug.Log ("X,Y, and Z should be " + nextPositionCameraLocX + "," +nextPositionCameraLocY + "," + nextPositionCameraLocZ);
		nextPositionLoc.position = new Vector3 (nextPositionCameraLocX,nextPositionCameraLocY,nextPositionCameraLocZ);
		destination = nextPositionLoc;
	}

	//testScript
	public void go1(){



//		moveCameraPosition ();
//		updateScreen ();
//		optimizeIt ();
	}





	void Update() {
		if(Input.GetKeyDown(KeyCode.RightArrow)){
				go1();
			}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if (currentScreen < 1) {
				Debug.Log ("left arrow says which screen is now " + currentScreen);
				currentScreen = 8;
				Debug.Log ("left arrow says which screen is now " + currentScreen);
			}
			else {
				Debug.Log ("left arrow says which screen is now " + currentScreen);
				currentScreen--;
				Debug.Log ("left arrow says which screen is now " + currentScreen);
				currentScreen--;
				Debug.Log ("left arrow says which screen is now " + currentScreen);

			}
				
			go1();
		}
//		}

//		if(Input.GetKey("1")){
//			//go to position 1
//			midMoving = true;
//			destination = postition1;
////		float distCovered = (Time.time - startTime) * speed;
////		float fracJourney = distCovered / journeyLength;
////		transform.position = Vector3.Lerp(transform.position, endMarker.position, fracJourney);
//		}
//		if(Input.GetKey("2")){
//			//go to position 1
//			midMoving = true;
//			destination = postition2;
//		}
//		if(Input.GetKey("3")){
//			//go to position 1
//			midMoving = true;
//			destination = postition3;
//		}
//		if(Input.GetKey("4")){
//			//go to position 1
//			midMoving = true;
//			destination = postition4;
//		}
//		if(Input.GetKey("5")){
//			//go to position 1
//			midMoving = true;
//			destination = postition5;
//		}
//		if(Input.GetKey("6")){
//			//go to position 1
//			midMoving = true;
//			destination = postition6;
//		}
//		if(Input.GetKey("7")){
//			//go to position 1
//			midMoving = true;
//			destination = postition7;
//		}
		goToPosition(destination);
	}

	void goToPosition(Transform destination){
//		startTime = Time.time;
		journeyLength = Vector3.Distance(postition1.position, postition2.position);
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(transform.position, destination.position, Time.deltaTime * speed);
		
	}

}

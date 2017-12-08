using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement : MonoBehaviour {



		public float speed = .005f;
		public float speedMouse = 7f;
		public float maxDistance = 3.5f;
		public Camera mainCamera ;
		private Vector3 _origin;
	private Vector3 parent_origin;
		public bool move = true;
		public int count = 0;
		public float startTime;

		// Use this for initialization
		void Start () {
			mainCamera = Camera.main;
			_origin = transform.position;
		parent_origin = transform.parent.position; //test
			startTime = Time.time;

		}

		void followMouse(){
			var mouseWorldCoordinates = mainCamera.ScreenPointToRay(Input.mousePosition).origin;

			var originToMouse = mouseWorldCoordinates - _origin;
			originToMouse = Vector3.ClampMagnitude (originToMouse, maxDistance);
			if (count < 300) {
				transform.position = Vector3.Lerp (transform.position, _origin + originToMouse, speedMouse*Time.deltaTime);
				count++;
			} else if(count==600){
				count = 0;
			}else {
				transform.position = _origin;
				count++;
			}

		}

		void upDown(){


			if (count <100) {
				count++;
				if (move) {
					var temp = new Vector3 (transform.position.x,Mathf.PingPong (0.75f*Time.time, 0.3f) - 0.3f, transform.position.z);
					transform.position = Vector3.ClampMagnitude (temp, maxDistance);
				} else {
					var temp2 = new Vector3 (transform.position.x,Mathf.PingPong (0.75f*Time.time, 0.3f) - 0.3f, transform.position.z);
					transform.position = Vector3.ClampMagnitude (temp2, maxDistance);

				}
			} else if (count == 400) {
				count = 0;
			} else {
				transform.position = _origin;
				count++;		
			}



		}

		void sideToSide(){

			if (count <200) {
				count++;
				if (move) {
					var temp = new Vector3 (Mathf.PingPong (Time.time, 1.0f) + 1.0f, transform.position.y, transform.position.z);
					transform.position = Vector3.ClampMagnitude (temp, maxDistance);
				//transform.parent.position =  Vector3.ClampMagnitude (temp, 0.5f); //test
				} else {
					var temp2 = new Vector3 (Mathf.PingPong (Time.time, 1.0f) - 0.5f, transform.position.y, transform.position.z);
					transform.position = Vector3.ClampMagnitude (temp2, maxDistance);
				//transform.parent.position =  Vector3.ClampMagnitude (temp2, 0.5f); //test
				}

			} else if (count == 400) {
				count = 0;
			} else {
				transform.position = _origin;
			transform.parent.position = parent_origin;
				count++;		
			}



		}

		// Update is called once per frame
		void Update () {




			//transform.position.x = Mathf.PingPong(Time.time,2.0f) - 1.0f;
			//side to side eye movements
			//sideToSide();
			upDown ();
			//followMouse ();








			//Get the mouse space in world space
			//var mouseWorldCoordinates = mainCamera.ScreenPointToRay(Input.mousePosition).origin;
			//get Vector pointing from initial position to the target. Vector cant be longer than
			//max distance
			//var originToMouse = mouseWorldCoordinates - _origin;
			//originToMouse = Vector3.ClampMagnitude (originToMouse, maxDistance);


			//Linearly interpolate from current to mouse's position
			//transform.position = Vector3.Lerp(transform.position, _origin + originToMouse, speed * Time.deltaTime);
			//Debug.Log (transform.position);

			//Link to move it for fixed tim
			//http://answers.unity3d.com/questions/34884/move-a-gameobject-for-certain-amount-of-time-and-t.html



	}
}
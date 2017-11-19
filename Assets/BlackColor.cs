using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackColor : MonoBehaviour {

	public float speed = .5f;
	public float maxDistance = 3f;
	public Camera mainCamera ;
	private Vector3 _origin;
	public bool move = true;
	public int count = 0;
	public float startTime;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		_origin = transform.position;
		startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 leftPosition = new Vector3 (transform.position.x+.3f,transform.position.y,transform.position.z);
		Vector3 rightPosition = new Vector3 (transform.position.x-.6f,transform.position.y,transform.position.z);
		if (count >1) {
			leftPosition = new Vector3 (transform.position.x+.6f,transform.position.y,transform.position.z);
		}
	
		/*if (count < 20) {
			if (move) {
				transform.position = Vector3.Lerp (transform.position, leftPosition, transform.position.z);
				//transform.position = Vector3.MoveTowards (transform.position, leftPosition, speed);
				move = false;
			} else {
				transform.position = Vector3.Lerp (transform.position, rightPosition, speed);
				//transform.position = Vector3.MoveTowards (transform.position, rightPosition, speed);
				move = true;

			}
			count++;
			
		} else {
			transform.position = _origin;
		
		
		}*/




		//transform.position.x = Mathf.PingPong(Time.time,2.0f) - 1.0f;
		if (count <200) {
			count++;
			if (move) {
				var temp = new Vector3 (Mathf.PingPong (Time.time, 1.0f) + 1.0f, transform.position.y, transform.position.z);
				transform.position = Vector3.ClampMagnitude (temp, maxDistance);
			} else {
				var temp2 = new Vector3 (Mathf.PingPong (Time.time, 1.0f) - 0.5f, transform.position.y, transform.position.z);
				transform.position = Vector3.ClampMagnitude (temp2, maxDistance);

			}
					
		} else if (count == 800) {
			count = 0;
			//transform.position = _origin;
		} else {
			transform.position = _origin;

			count++;		
		}




			
		
		//if (transform.position.x > 0) {
		//	for (float i = transform.position.x; i < 1.75f; i++) {
		//		transform.Translate (Vector3.right * Time.deltaTime);
		//	}
		
		//} 


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

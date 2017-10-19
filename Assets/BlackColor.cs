using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackColor : MonoBehaviour {

	public float speed = 5f;
	public float maxDistance = 1f;
	public Camera mainCamera ;
	private Vector3 _origin;
	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		_origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Get the mouse space in world space
		var mouseWorldCoordinates = mainCamera.ScreenPointToRay(Input.mousePosition).origin;
		//get Vector pointing from initial position to the target. Vector cant be longer than
		//max distance
		var originToMouse = mouseWorldCoordinates - _origin;
		originToMouse = Vector3.ClampMagnitude (originToMouse, maxDistance);

		//Linearly interpolate from current to mouse's position
		transform.position = Vector3.Lerp(transform.position, _origin + originToMouse, speed * Time.deltaTime);
		Debug.Log (transform.position);

		//Link to move it for fixed tim
		//http://answers.unity3d.com/questions/34884/move-a-gameobject-for-certain-amount-of-time-and-t.html
	}
}

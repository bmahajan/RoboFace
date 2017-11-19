using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonScript : MonoBehaviour {
	//, IPointerDownHandler
	private bool[] mousePressed = { false, false, false };
	public int count = 0;
	float rate = 200.0f;
	//List<MonoBehaviour> delayedUpdatedStuff = new List<MonoBehaviour>();

	void Start(){
		//Debug.Log ("Start");
		InvokeRepeating ("SlowUpdate", 0.0f,rate);
	}

	void SlowUpdate() {
		//Debug.Log ("Updated");

		if (Input.GetMouseButtonDown (0))
			Debug.Log ("clicked");
			//clickFunction();
	}


	private void clickFunction()
	{

		Debug.Log ("in click function\n");
		if (mousePressed [0] || mousePressed [1] || mousePressed [2])
		{
			Debug.Log ("clicked");
			mousePressed[0] = false;
			mousePressed [1] = false;
			mousePressed [2] = false;
		}

	}
		

	void Update()
	{
		//Debug.Log ("update called");

			mousePressed [0] = Input.GetMouseButtonDown (0);
			mousePressed [1] = Input.GetMouseButtonDown (1);
			mousePressed [2] = Input.GetMouseButtonDown (2);


	
		clickFunction ();
	}



}

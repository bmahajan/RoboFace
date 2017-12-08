using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonScript : MonoBehaviour {
	//, IPointerDownHandler
	private bool[] mousePressed = { false, false, false };
	private int counter = 0;
	void Start()
	{
		

	}
	private void clickFunction()
	{
		
		//Debug.Log ("in click function\n");
		if ((mousePressed [0] || mousePressed [1] || mousePressed [2] ))
		{
			
			mousePressed [0] = false;
			mousePressed [1] = false;
			mousePressed [2] = false;
			//this.GetComponent<AudioSource> ().Play ();
		}
		return;
	}

	void Update()
	{
		//Debug.Log ("update called");
		mousePressed[0] = Input.GetMouseButtonDown (0);
		mousePressed [1] = Input.GetMouseButtonDown (1);
		mousePressed [2] = Input.GetMouseButtonDown (2);
		clickFunction ();
	}

	void playSound()
	{
		
	}

}

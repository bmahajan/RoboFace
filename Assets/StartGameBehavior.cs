using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameBehavior: MonoBehaviour {


	public static bool gameStarted = false;
	GameObject[] allObjects;
	// Use this for initialization
	void Start () 
	{
		Pause ();	
		gameStarted = false;
		allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		foreach (GameObject go in allObjects) 
		{
			//Set all objects other than main camera and directional light to inactive
			if (go.tag == "Button" ) {
				go.SetActive (false);
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void Pause()
	{
		Time.timeScale = 0;	

	}

	public void UnPause()
	{
		Time.timeScale = 1;
		gameStarted = true;
		foreach (GameObject go in allObjects)
			go.SetActive (true);
	}

	void OnGUI()
	{
		GUI.contentColor = Color.white;
		GUI.backgroundColor = Color.black;
		GUI.color = Color.white;
		if (!gameStarted)
		{
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 10, 100, 30), "Start")) 
			{
				UnPause ();
			}	
		}

	}
}

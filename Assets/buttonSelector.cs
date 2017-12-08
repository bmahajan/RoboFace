using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class buttonSelector : MonoBehaviour {
	public static List<RaycastHit> listSelectedObjects;
	public static List<string> tagNamesListOfSelectedObjects;
	public static AudioSource[] negativeResponses;
	public static AudioSource[] positiveResponses;
	public static Dictionary<string, int> tagToIndexMap;
	public  static int numSelectedObjects = 0;
	public static int numObjectsToPick = 5;
	public static AudioSource responseToPlay;
	public static float[] sampleOfResponseToPlay;
	public static bool showDialog = false;
	private static bool justInterrupted = false;
	private static RaycastHit currentHitObject ;
	public GUIStyle myGuiStyle;

	public static bool smile = true;
	public static bool sad = false;

	public static AudioSource thankUser;
	public static AudioSource apologizeToUser;

	// Use this for initialization
	void Start () {
		listSelectedObjects = new List<RaycastHit> ();
		tagNamesListOfSelectedObjects = new List<string> ();
		AudioSource[] allResponses = this.GetComponents<AudioSource> ();
		negativeResponses = new AudioSource[10];
		positiveResponses = new AudioSource[10];
		//Copy negative responses to negativeResponse array. The first 10 rsponses are negative
		for (int i = 0; i < 10; i++) 
		{
			negativeResponses [i] = allResponses [i];
		}
		//Copy positive responses to positiveresponses array.
		for (int i = 0; i < 10; i++) {
			positiveResponses [i] = allResponses [i + 10];
		}
		tagToIndexMap = new Dictionary<string, int> ();

		tagToIndexMap.Add ("SurvivalGuide", 0);
		tagToIndexMap.Add ("Flint", 1);
		tagToIndexMap.Add ("Jacket", 2);
		tagToIndexMap.Add ("Gun", 3);
		tagToIndexMap.Add ("Cookies", 4);
		tagToIndexMap.Add ("Rope", 5);
		tagToIndexMap.Add ("Bottle", 6);
		tagToIndexMap.Add ("Knife", 7);
		tagToIndexMap.Add ("Cap", 8);
		tagToIndexMap.Add ("Tent", 9);
		numSelectedObjects = 0;
		numObjectsToPick = 5;

		//For yes/no dialog
		myGuiStyle.hover.textColor = Color.cyan;
		myGuiStyle.normal.background = new Texture2D(2,2, TextureFormat.ARGB32, false);
		myGuiStyle.fontSize = 18;
		myGuiStyle.alignment = TextAnchor.MiddleCenter;
		myGuiStyle.normal.textColor = Color.white;



		//For response after user changes/not change their choice
		AudioSource[] responses= GameObject.FindGameObjectWithTag("Mouth").GetComponents<AudioSource>();
		thankUser = responses [2];
		apologizeToUser = responses [1];

	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (numSelectedObjects < numObjectsToPick) {
			//Pick objects while not picked the required number
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform.tag == "Button") {
						DeselectAllButtons ();
						numSelectedObjects++;
						SelectClickedButton (hit);
						currentHitObject = hit;

					}
				}
			}
		}
		Debug.Log ("\nYou have now made your selecteion");
	}

	void DeselectAllButtons()
	{
		var gameObjectList = GameObject.FindGameObjectsWithTag ("Button");
		foreach(GameObject button in gameObjectList)
		{
			button.GetComponent<buttonSelected>().selected = false;
		}

	}

	void SelectClickedButton(RaycastHit hit)
	{
		hit.transform.gameObject.GetComponent<buttonSelected>().selected = true;
		if ((numSelectedObjects == 2 || numSelectedObjects == 4) && !justInterrupted) {
			//Try to interject 
			string tag = hit.transform.gameObject.transform.GetChild (0).gameObject.transform.tag;
			int index = -1;
			if (tag != null) {
				tagToIndexMap.TryGetValue (tag, out index);
				if (index != -1) {
					
					responseToPlay = negativeResponses [index];
					sampleOfResponseToPlay = new float[responseToPlay.clip.samples * responseToPlay.clip.channels];
					responseToPlay.clip.GetData(sampleOfResponseToPlay, 0);
					responseToPlay.Play ();
					justInterrupted = true;
					//Wait for sound to end
					StartCoroutine(WaitForAudioResponseToFinish(hit));


				}
			}

		} else {
			justInterrupted = false;
			if (numSelectedObjects == 3 || numSelectedObjects == 5) {
				//Praise user for their choice
				string tag = hit.transform.gameObject.transform.GetChild (0).gameObject.transform.tag;
				int index = -1;
				if (tag != null) {
					tagToIndexMap.TryGetValue (tag, out index);
					Debug.Log ("index is " + index);
					if (index != -1) {
						responseToPlay = positiveResponses [index];
						sampleOfResponseToPlay = new float[responseToPlay.clip.samples * responseToPlay.clip.channels];
						responseToPlay.clip.GetData (sampleOfResponseToPlay, 0);
						responseToPlay.Play ();

					}
				}
			} else 
			{
				//responseToPlay = null; //say nothing
			}
			FinalizeUserChoice (hit);
		}
	}

	IEnumerator WaitForAudioResponseToFinish(RaycastHit hit)
	{	//waits till response finishes - then shows dialog box
			while (responseToPlay!= null && responseToPlay.isPlaying) {
				yield return null;
			}
			//Audio has finished playing - so can show pop up box now


		showDialog = true;

	}

	void OnGUI()
	{
		GUI.contentColor = Color.white;
		GUI.backgroundColor = Color.black;
		GUI.color = Color.white;
		GUI.Label (new Rect (0, 0, 200, 30), "Options picked: " + numSelectedObjects, myGuiStyle); //display how many objects user still needs to pick

		if (showDialog) {
			Debug.Log ("show dialog is true");
		
			GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height/2 - 150, 450, 60), " Would you like to change your choice based \n    on my suggestion ?", myGuiStyle);
			if (GUI.Button (new Rect (Screen.width/2 - 70, Screen.height/2 + 10, 100, 30), "Yes")) {
				//user wants to change options so decrement count
				showDialog = false;
				numSelectedObjects -= 1;
				//For emotion
				smile = true;
				sad = false;
				responseToPlay = thankUser;
				sampleOfResponseToPlay = new float[responseToPlay.clip.samples * responseToPlay.clip.channels];;
				responseToPlay.clip.GetData(sampleOfResponseToPlay, 0);
				responseToPlay.Play ();

			}
			if (GUI.Button (new Rect (Screen.width/2 + 50, Screen.height/2 + 10, 100, 30), "No")) {
				//user not want to change option 
				showDialog = false;
				//For emotion
				smile = false;
				sad = true;
				FinalizeUserChoice (currentHitObject);
				responseToPlay = apologizeToUser;
				sampleOfResponseToPlay = new float[responseToPlay.clip.samples * responseToPlay.clip.channels];;
				responseToPlay.clip.GetData(sampleOfResponseToPlay, 0);
				responseToPlay.Play ();


			}
			//showDialog = false;
		}
	
	}

	void FinalizeUserChoice(RaycastHit hit)
	{
		justInterrupted = false;
		//responseToPlay = null;
		listSelectedObjects.Add (hit);
		tagNamesListOfSelectedObjects.Add (hit.transform.gameObject.transform.GetChild (0).gameObject.transform.tag);
		PrintValues ();	
	
	}


	void PrintValues()  
	{
		GameObject child;
		foreach (RaycastHit obj in listSelectedObjects) 
		{
			var hitObject = obj;
			child = hitObject.transform.gameObject.transform.GetChild (0).gameObject;
			Debug.Log( "\n"+child.transform.tag.ToString());

		}
	Debug.Log ("\n");
	}



}


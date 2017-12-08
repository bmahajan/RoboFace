using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthEmotions : MonoBehaviour {


	public static bool smile = true;
	public static bool sad = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		smile = buttonSelector.smile;
		sad = buttonSelector.sad;
		if (smile) 
		{
			//User accepted our suggestion , so smile
			//this.transform.localScale = new Vector3 (1.3F, 0.1F , 0.5F);

		
		}
		if (sad) 
		{
			//User rejected our sggestion, so sad face
			//this.transform.localScale = new Vector3 (1.30F, .3F , 0.5F);
		}
	}
}

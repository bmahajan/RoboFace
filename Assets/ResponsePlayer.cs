using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsePlayer : MonoBehaviour {
	public static AudioSource toSpeak;
	public static float[] sampleOfToSpeak;
	public static GameObject mouth;
	public int sampleLength;

	// Use this for initialization
	void Start () {
		
		toSpeak = null;

		//mouth = GameObject.Find ("Mouth");

	}
	
	// Update is called once per frame
	void Update () 
	{
		
		toSpeak = buttonSelector.responseToPlay;
		sampleOfToSpeak = buttonSelector.sampleOfResponseToPlay;


		if (toSpeak != null) {
			Debug.Log ("to speak is not null");
			sampleLength = sampleOfToSpeak.Length;
			//in every frame get the current sample of the clip from the samples array
			int index = toSpeak.timeSamples * toSpeak.clip.channels;
			if (index < sampleLength) {
            
				float currentSample = sampleOfToSpeak [toSpeak.timeSamples * toSpeak.clip.channels];
				currentSample = Mathf.Abs (currentSample);
				//Debug.Log ("" + currentSample);

				Debug.Log ("open mouth");
				if (currentSample > 0.1) {
					this.transform.localScale = new Vector3 (1.5F, 3 * currentSample, 0.5F);
				} else {
					this.transform.localScale = new Vector3 (1.5F, 0.3F, 0.5F);
				}
			}
		}
	}
}

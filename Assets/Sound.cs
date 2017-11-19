using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	float[] samples;
	public AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		samples = new float[audioSource.clip.samples * audioSource.clip.channels];
		//store sample data from clip in this array
		audioSource.clip.GetData(samples, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//in every frame get the current sample of the clip from the samples array
		float currentSample = samples[audioSource.timeSamples * audioSource.clip.channels];
		if (currentSample > 0.05) {
			transform.localScale = new Vector3 (1.5F, 2.2F * currentSample, 0.5F);
		} else {
			transform.localScale = new Vector3 (1.5F, 0.1F, 0.5F);
		}
		
	}
}

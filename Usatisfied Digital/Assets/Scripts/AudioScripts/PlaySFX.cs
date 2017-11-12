using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour {

	private AudioSource myAudioSource;

	// Use this for initialization
	void Start () {

		myAudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SatisfactionSFX(){
		myAudioSource.Play ();
	}


}

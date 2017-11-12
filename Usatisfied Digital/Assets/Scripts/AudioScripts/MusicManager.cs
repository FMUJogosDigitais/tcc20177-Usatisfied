using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	[Header("Musicas de BackGround")]
	[SerializeField] AudioClip[] bgMusic;

	private AudioSource myAudioSource;

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource> ();
		BgMusic ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BgMusic ()
	{
		int xMusic = Random.Range (0, bgMusic.Length);
		myAudioSource.clip = bgMusic [xMusic];
		myAudioSource.Play ();
	}

	public void StopBGMusic()
	{
		myAudioSource.Stop ();
	}
}

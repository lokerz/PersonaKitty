using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public AudioSource song;
	public AudioClip song1;
	public AudioClip song2;

	void Start () {
		if (PlayerPrefs.GetInt ("SongNumber", 1) == 1)
			song.clip = song1;
		else
			song.clip = song2;

		song.Play ();
	}
	
	public void SaveAudio(int songNumber){
		PlayerPrefs.SetInt ("SongNumber", songNumber);
	}


}

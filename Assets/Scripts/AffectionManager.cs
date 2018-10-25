using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AffectionManager : MonoBehaviour {
	public int[] affectionBorder = { 10, 40, 60, 90 };
	public int[] feedValue = { 6, 5, 4, 3, 3, 3, 3, 2, 2, 2 };
	public int[] petValue = { 0, 1, 1, 1, 2, 2, 1, 1, 1, 1 };
	public int[] playValue = { 0, 0, 1, 2, 3, 4, 5, 4, 3, 2 };

	public static float affectionValue;
	public static int affectionType;
	public static TimeSpan playCooldownTime;
	public static String nextFeedingTime;

	private bool feedCooldown = false;
	private bool playCooldown = false;
	private DateTime lastFeedTime;
	private DateTime lastPlayTime;


	void Start () {
		affectionValue = PlayerPrefs.GetFloat ("AffectionValue", 30);
		PlayerPrefs.SetFloat ("AffectionValue",affectionValue);
		lastPlayTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString ("lastPlayTime",DateTime.Now.AddHours(-2).ToBinary ().ToString ())));
	}
		
	void Update () {
		if (affectionValue < affectionBorder [0])
			affectionType = 1;
		else if (affectionValue < affectionBorder [1])
			affectionType = 2;
		else if (affectionValue < affectionBorder [2])
			affectionType = 3;
		else if (affectionValue < affectionBorder [3])
			affectionType = 4;
		else
			affectionType = 5;
		
		playCooldownTime = lastPlayTime.AddHours(1) - DateTime.Now;



		if (DateTime.Now.Hour < 6)
			nextFeedingTime = (5 - DateTime.Now.Hour) + ":" + (59 - DateTime.Now.Minute) + ":" + (59 - DateTime.Now.Second);
		else if(DateTime.Now.Hour < 11)
			nextFeedingTime = (10 - DateTime.Now.Hour) + ":" + (59 - DateTime.Now.Minute) + ":" + (59 - DateTime.Now.Second);
		else if(DateTime.Now.Hour < 17)
			nextFeedingTime = (10 - DateTime.Now.Hour) + ":" + (59 - DateTime.Now.Minute) + ":" + (59 - DateTime.Now.Second);

		//Debug.Log (playCooldownTime);
		//Debug.Log (nextFeedingTime);

	}
		
	public void Feed(){
		FeedTime ();
		if (!feedCooldown) {
			int feedTemp = feedValue [(int)(affectionValue / 10)];
			affectionValue += feedTemp;
			feedCooldown = true;
		}
	}

	public void Pet(){
		float petTemp = petValue[(int)(affectionValue/10)];
		affectionValue += petTemp;
		petTemp -= (float)petValue[(int)(affectionValue/10)]/4;
		Debug.Log (petTemp + " " + affectionValue);
	}

	public void Play(){
		PlayTime ();
		Debug.Log (playCooldown);
		Debug.Log (lastPlayTime);
		Debug.Log (affectionValue);

		if (!playCooldown) {
			int playTemp = playValue [(int)(affectionValue / 10)];
			affectionValue += playTemp;
			playCooldown = true;
			PlayerPrefs.SetString ("lastPlayTime", DateTime.Now.ToBinary ().ToString ());
			PlayerPrefs.SetFloat ("AffectionValue",affectionValue);
		}
		PlayerPrefs.Save ();
	}

	private void FeedTime(){
		//TimeSpan ts = DateTime.Now - lastFeedTime;
		//if(feedCooldown == false && ts.TotalHours > )

	}

	private void PlayTime(){
		lastPlayTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString ("lastPlayTime",DateTime.Now.AddHours(-2).ToBinary ().ToString ())));
		TimeSpan ts = DateTime.Now - lastPlayTime;
		Debug.Log (ts);
		if (ts.TotalHours >= 1)
			playCooldown = false;
		else
			playCooldown = true;
	}


}

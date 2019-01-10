using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Play : MonoBehaviour {
	public Button playButton;
	public GameObject playTimer;
	public static TimeSpan playCooldownTime;

	public int[] playValue = { 0, 0, 1, 2, 3, 4, 5, 4, 3, 2 };

	private bool playCooldown = false;
	private DateTime lastPlayTime;
	private PointAddition pointAddition;
	private TimeSpan ts;

	void Start () {
		pointAddition = gameObject.GetComponent<PointAddition> ();
		lastPlayTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString ("lastPlayTime",DateTime.Now.AddHours(-2).ToBinary ().ToString ())));

	}

	void Update () {
		playCooldownTime = lastPlayTime.AddHours(1) - DateTime.Now;
		ts = DateTime.Now - lastPlayTime;

		if (ts.TotalHours >= 1)
			playCooldown = false;
		else
			playCooldown = true;
		
		if (playCooldown) {
			playButton.interactable = false;
			playTimer.GetComponent<Text> ().text = string.Format("{00:00:00}", playCooldownTime);
			playTimer.SetActive (true);
		} else {
			playButton.interactable = true;
			playTimer.SetActive (false);
		}
	}

	public void DoPlay(){
		int playTemp;

		if (!playCooldown) {
			int index = (int)(AffectionManager.affectionValue / 10);
			if(index == 10)
				index = 9;
			
			playTemp = playValue [index];

			AffectionManager.affectionValue += playTemp;
			pointAddition.addPoint (playTemp);

			playCooldown = true;
			lastPlayTime = DateTime.Now;
			PlayerPrefs.SetString ("lastPlayTime", DateTime.Now.ToBinary ().ToString ());
			PlayerPrefs.SetFloat ("AffectionValue",AffectionManager.affectionValue);
		}
		PlayerPrefs.Save ();
	}
}

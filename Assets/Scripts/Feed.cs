using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Feed : MonoBehaviour {
	public Button feedButton;
	public GameObject feedTimer;

	public int[] feedValue = { 6, 5, 4, 3, 3, 3, 3, 2, 2, 2 };
	public static String nextFeedingTime;
	private bool feedCooldown = true;
	private bool feedable = false;
	private DateTime lastFeedTime;
	private PointAddition pointAddition;
	private CoinManager coinManager;

	void Start () {
		coinManager = GameObject.Find("CoinManager").GetComponent<CoinManager> ();
		pointAddition = gameObject.GetComponent<PointAddition> ();
		lastFeedTime =  DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString ("LastFeedTime",DateTime.Now.AddDays(-1).ToBinary ().ToString ())));
		Debug.Log (lastFeedTime);

		if (lastFeedTime.Date < DateTime.Now.Date) 
			feedCooldown = false;
		else if (feedCooldown) {
				if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 9 && lastFeedTime.Hour >= 6 && lastFeedTime.Hour < 9) 
					feedCooldown = true;
				else if (DateTime.Now.Hour >= 11 && DateTime.Now.Hour < 13 && lastFeedTime.Hour >= 11 && lastFeedTime.Hour < 13)
					feedCooldown = true;
				else if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 20 && lastFeedTime.Hour >= 17 && lastFeedTime.Hour < 20)
					feedCooldown = true;
				else
					feedCooldown = false;
		}
		
	}

	void Update () {
		if (lastFeedTime.Date < DateTime.Now.Date) 
			feedCooldown = false;
		else if (feedCooldown) {
			if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 9 && lastFeedTime.Hour >= 6 && lastFeedTime.Hour < 9) 
				feedCooldown = true;
			else if (DateTime.Now.Hour >= 11 && DateTime.Now.Hour < 13 && lastFeedTime.Hour >= 11 && lastFeedTime.Hour < 13)
				feedCooldown = true;
			else if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 20 && lastFeedTime.Hour >= 17 && lastFeedTime.Hour < 20)
				feedCooldown = true;
			else
				feedCooldown = false;
		}

		if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 9)
			feedable = true;
		else if (DateTime.Now.Hour >= 11 && DateTime.Now.Hour < 13)
			feedable = true;
		else if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 20)
			feedable = true;
		else {
			feedable = false;
			feedCooldown = false;
		}
			

		if (DateTime.Now.Hour < 6)
			nextFeedingTime = (5 - DateTime.Now.Hour).ToString ("00") + ":" + (59 - DateTime.Now.Minute).ToString ("00") + ":" + (59 - DateTime.Now.Second).ToString ("00");
		else if(DateTime.Now.Hour < 11)
			nextFeedingTime = (10 - DateTime.Now.Hour).ToString ("00") + ":" + (59 - DateTime.Now.Minute).ToString ("00") + ":" + (59 - DateTime.Now.Second).ToString ("00");
		else if(DateTime.Now.Hour < 17)
			nextFeedingTime = (16 - DateTime.Now.Hour).ToString ("00") + ":" + (59 - DateTime.Now.Minute).ToString ("00") + ":" + (59 - DateTime.Now.Second).ToString ("00");
		else if(DateTime.Now.Hour >= 17)
			nextFeedingTime = (29 - DateTime.Now.Hour).ToString ("00") + ":" + (59 - DateTime.Now.Minute).ToString ("00") + ":" + (59 - DateTime.Now.Second).ToString ("00");
		if (feedable && !feedCooldown) {
			feedButton.interactable = true;
			feedTimer.SetActive (false);
		}
		else {
			feedButton.interactable = false;
			feedTimer.GetComponent<Text> ().text = nextFeedingTime;
			feedTimer.SetActive(true);
		}	
			
	}

	public void DoFeed(){
		int feedTemp;
		int index = (int)(AffectionManager.affectionValue / 10);
		if(index == 10)
			index = 9;
		
		feedTemp = feedValue [index];

		if (!feedCooldown && feedable) {
			AffectionManager.affectionValue += feedTemp;
			pointAddition.addPoint (feedTemp);
			coinManager.AddCoin (5);
			feedCooldown = true;
			lastFeedTime = DateTime.Now;
			PlayerPrefs.SetString ("LastFeedTime", DateTime.Now.ToBinary ().ToString ());
		}

		PlayerPrefs.SetFloat("AffectionValue", AffectionManager.affectionValue);
	}
}

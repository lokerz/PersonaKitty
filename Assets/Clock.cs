using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Clock : MonoBehaviour {
	DateTime time;
	String hour, minute;
	Text hourbox, minutebox;
	// Use this for initialization
	void Start () {
		
		hourbox = GameObject.Find("Hour").GetComponent<Text>();
		minutebox = GameObject.Find("Minute").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time = DateTime.Now;
		hour = time.Hour.ToString ();
		minute = time.Minute.ToString ();
		hourbox.text = hour + ":";
		if(time.Minute < 10)
			minutebox.text = "0"+minute;
		else
		minutebox.text = minute;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour {
	Text textbox;
	// Use this for initialization
	
	public void SpeechTrigger(string speech){
		textbox = GetComponentInChildren<Text> ();
		textbox.text = speech;
		StopCoroutine("deactivate");
		StartCoroutine("deactivate");
	}

	IEnumerator deactivate(){
		while (true){
			yield return new WaitForSeconds (3);
			gameObject.SetActive (false);
		}
	}
}

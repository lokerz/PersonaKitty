using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour {
	private Text textbox;
	private SpeechManager speechManager;

	void Start(){
		speechManager = GameObject.Find("SpeechManager").GetComponent<SpeechManager> ();
		gameObject.SetActive (false);
	}

	public void SpeechTrigger(string type){
		gameObject.SetActive (true);
		textbox = GetComponentInChildren<Text> ();
		if (type == "play")
			textbox.text = speechManager.RandomPlayWord ();
		else if (type == "feed")
			textbox.text = speechManager.RandomFeedWord ();
		else if (type == "pet")
			textbox.text = speechManager.RandomPetWord ();
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

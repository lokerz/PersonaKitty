using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour {
	private static string[] playList;
	private static string[] petList;
	private static string[] feedList;

	public TextAsset playDB;
	public TextAsset feedDB;
	public TextAsset petDB;

	void Awake(){
		playList = playDB.text.Split (new[]{"\n","\r"},StringSplitOptions.RemoveEmptyEntries);
		petList = petDB.text.Split (new[]{"\n","\r"},StringSplitOptions.RemoveEmptyEntries);
		feedList = feedDB.text.Split (new[]{"\n","\r"},StringSplitOptions.RemoveEmptyEntries);
	}


	public string RandomPlayWord(){
		int randIndex = UnityEngine.Random.Range (0, playList.Length);

		return (playList [randIndex]);
	}

	public string RandomPetWord(){
		int randIndex = UnityEngine.Random.Range (0, petList.Length);

		return (petList [randIndex]);
	}

	public string RandomFeedWord(){
		int randIndex = UnityEngine.Random.Range (0, feedList.Length);

		return (feedList [randIndex]);
	}
}

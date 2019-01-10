using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class AffectionManager : MonoBehaviour {
	public GameObject afMeter;
	public GameObject afSprite;

	public int[] affectionBorder = { 10, 40, 60, 90 };

	public static float affectionValue;
	public static int affectionType;

	private List<Sprite> heart;

	void Start () {
		affectionValue = PlayerPrefs.GetFloat ("AffectionValue", 30);

		heart = new List<Sprite> ();

		for (int i = 0; i < 3; i++) {
			heart.Add(Resources.Load ("Heart"+i.ToString(), typeof(Sprite)) as Sprite);
		}
	}

	void Update () {
		if (afMeter != null)
			afMeter.GetComponent<Text> ().text = Mathf.Floor(affectionValue).ToString ()+"%";


		if (affectionValue < affectionBorder [0]) {
			affectionType = 1;
			afSprite.GetComponent<Image>().sprite = heart [0];
		} else if (affectionValue < affectionBorder [1]) {
			affectionType = 2;
			afSprite.GetComponent<Image>().sprite = heart [0];
		} else if (affectionValue < affectionBorder [2]) {
			affectionType = 3;
			afSprite.GetComponent<Image>().sprite = heart [1];
		} else if (affectionValue < affectionBorder [3]) {
			affectionType = 4;
			afSprite.GetComponent<Image>().sprite = heart [1];
		} else {
			affectionType = 5;
			afSprite.GetComponent<Image>().sprite = heart [2];
		}

		if (affectionValue > 100)
			affectionValue = 100;
		if (affectionValue < 0) {
			affectionValue = 0;
		}
	}
		

	public void Reset(){
		affectionValue = 30;
		PlayerPrefs.SetFloat("AffectionValue", affectionValue);
		PlayerPrefs.Save();
	}


}

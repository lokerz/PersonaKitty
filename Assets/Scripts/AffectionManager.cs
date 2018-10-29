using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class AffectionManager : MonoBehaviour {
	public GameObject afMeter;
	public bool timerSystem = false;

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
	private bool petCooldown = false;
	private DateTime lastFeedTime;
	private DateTime lastPlayTime;
	private DateTime lastPetTime;
	private int petCount;

	private PointAddition pointAddition;
	private PseudoAnimation pseudoAnimation;

	void Start () {
		pseudoAnimation = GameObject.Find ("Cat").GetComponent<PseudoAnimation> ();
		pointAddition = gameObject.GetComponent<PointAddition> ();
		affectionValue = PlayerPrefs.GetFloat ("AffectionValue", 30);
		lastPlayTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString ("lastPlayTime",DateTime.Now.AddHours(-2).ToBinary ().ToString ())));
		lastPetTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("lastPetTime", DateTime.Now.AddMinutes(-10).ToBinary().ToString())));
		petCount = PlayerPrefs.GetInt("lastPetCount",0);
	}

	void Update () {


		if (afMeter != null)
			afMeter.GetComponent<Text> ().text = Mathf.Floor(affectionValue).ToString ()+"%";


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
			nextFeedingTime = (16 - DateTime.Now.Hour) + ":" + (59 - DateTime.Now.Minute) + ":" + (59 - DateTime.Now.Second);

		//Debug.Log (playCooldownTime);
		//Debug.Log (nextFeedingTime);

	}

	public void Pet()
	{
		petCount = PlayerPrefs.GetInt("lastPetCount", 0);
		lastPetTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("lastPetTime", DateTime.Now.AddMinutes(-10).ToBinary().ToString())));
		TimeSpan ts = DateTime.Now - lastPetTime;
		Debug.Log(ts);

		if (ts.TotalMinutes >= 5)
		{
			petCount = 0;
			PlayerPrefs.SetInt("lastPetCount", petCount);
			petCooldown = false;
		}
		else
			petCooldown = true;

		if (!petCooldown)
		{
			petCooldown = true;
			PlayerPrefs.SetString("lastPetTime", DateTime.Now.ToBinary().ToString());
		}

		int petTemp = (int)Mathf.Floor((float)petValue [(int)(affectionValue / 10)] - (float)petValue [(int)(affectionValue / 10)] / 4 * petCount);

		affectionValue += petTemp;
		pseudoAnimation.Rub ();
		pointAddition.addPoint (petTemp);

		PlayerPrefs.SetFloat("AffectionValue", affectionValue);
		petCount++;
		PlayerPrefs.SetInt("lastPetCount", petCount);
		PlayerPrefs.Save();
		Debug.Log(lastPetTime+ " "+petCooldown+" "+petCount+" "+affectionValue);
	}

	public void Feed(){
		if (!feedCooldown || !timerSystem) {
			int feedTemp = feedValue [(int)(affectionValue / 10)];

			affectionValue += feedTemp;
			pointAddition.addPoint (feedTemp);

			feedCooldown = true;
		}
	}

	public void Play(){
		lastPlayTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("lastPlayTime", DateTime.Now.AddHours(-2).ToBinary().ToString())));
		TimeSpan ts = DateTime.Now - lastPlayTime;
		Debug.Log(ts);
		if (ts.TotalHours >= 1 || !timerSystem)
			playCooldown = false;
		else
			playCooldown = true;

		if (!playCooldown) {
			int playTemp = playValue [(int)(affectionValue / 10)];

			affectionValue += playTemp;
			pointAddition.addPoint (playTemp);

			playCooldown = true;
			PlayerPrefs.SetString ("lastPlayTime", DateTime.Now.ToBinary ().ToString ());
			PlayerPrefs.SetFloat ("AffectionValue",affectionValue);
		}
		PlayerPrefs.Save ();
	}




}

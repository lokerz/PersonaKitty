using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Pet : MonoBehaviour {
	public Button petButton;
	public GameObject petTimer;
	public Animator anim;
	public static bool petMad;

	private int[] petValue = { 1, 1, 1, 1, 2, 2, 1, 1, 1, 1 };
	private DateTime lastPetTime;
	private bool petCooldown = false;
	private int petCount;
	private PointAddition pointAddition;

	void Start () {
		pointAddition = gameObject.GetComponent<PointAddition> ();
		lastPetTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("lastPetTime", DateTime.Now.AddMinutes(-10).ToBinary().ToString())));
		petCount = PlayerPrefs.GetInt("lastPetCount",0);
	}
	public void DoPet()
	{
		int petTemp;
		int index;
		petCount = PlayerPrefs.GetInt("lastPetCount", 0);
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
			lastPetTime = DateTime.Now;
			PlayerPrefs.SetString("lastPetTime", DateTime.Now.ToBinary().ToString());
		}
		index = (int)(AffectionManager.affectionValue / 10);
		if(index == 10)
			index = 9;

		petTemp = (int)Mathf.Floor((float)petValue [index] - (float)petValue [index] / 4 * petCount);

		if (petTemp == 0)
			petTemp = 1;
		
		if (petTemp < 0) {
			anim.SetBool ("isMad", true);
			StopCoroutine ("ResetIdle");
			StartCoroutine ("ResetIdle");
			petMad = true;
		}

		if (petTemp <= -4)
			petTemp = -4;
		
		AffectionManager.affectionValue += petTemp;
		pointAddition.addPoint (petTemp);

		PlayerPrefs.SetFloat("AffectionValue", AffectionManager.affectionValue);
		petCount++;
		PlayerPrefs.SetInt("lastPetCount", petCount);
		PlayerPrefs.Save();
		Debug.Log(lastPetTime+ " "+petCooldown+" "+petCount+" "+AffectionManager.affectionValue);
	}

	IEnumerator ResetIdle(){
		while (true){
			yield return new WaitForSeconds (1);
			anim.SetBool ("isMad", false);
		}
	}
}

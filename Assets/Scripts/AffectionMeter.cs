using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AffectionMeter : MonoBehaviour {
	public GameObject afMeter;


	void Update () {
		if (gameObject.GetComponent<Toggle> ().isOn)
			afMeter.SetActive(true);
		else
			afMeter.SetActive(false);
	}
}

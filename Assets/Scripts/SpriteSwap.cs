using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour {

	public Sprite box1;
	public Sprite box2;

	void Update () {
		if (gameObject.GetComponent<Toggle> ().isOn)
			gameObject.GetComponent<Image> ().sprite = box2;
		else
			gameObject.GetComponent<Image> ().sprite = box1;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {
	public GameObject BackgroundObject;

	private Sprite background;
	void Start(){
		background = Resources.Load (PlayerPrefs.GetString ("background", "bg"), typeof (Sprite)) as Sprite;
		BackgroundObject.GetComponent<SpriteRenderer> ().sprite = background;
	}


	public void LoadBG(string bg){
		background = Resources.Load (bg, typeof(Sprite)) as Sprite;
		BackgroundObject.GetComponent<SpriteRenderer> ().sprite = background;
		PlayerPrefs.SetString ("background", bg);
	}
}

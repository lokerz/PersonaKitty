using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {
	public GameObject coinBox;
	public Text coinText;

	public static int coin;
	public Text coinShop;

	private bool isMoving = false;
	private Vector3 pos;

	void Start () {
		PlayerPrefs.GetInt ("Coins", 0);
		pos = coinBox.transform.position;
		coinBox.SetActive (false);
	}

	void Update(){
		if(isMoving)
			coinBox.transform.Translate(0f, 50*Time.deltaTime, 0f);
		coinShop.text = coin.ToString ();
	}

	public void AddCoin(int x){
		coin += x;
		coinBox.SetActive (true);
		coinBox.transform.position = pos;
		coinText.text = "+" + x.ToString ();
		isMoving = true;
		PlayerPrefs.SetInt ("Coins", coin);
		StopCoroutine("deactivate");
		StartCoroutine("deactivate");
	}

	IEnumerator deactivate(){
		yield return new WaitForSeconds (3);
		isMoving = false;
		coinBox.SetActive (false);
	}
}

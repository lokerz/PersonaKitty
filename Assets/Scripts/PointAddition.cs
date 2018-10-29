using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAddition : MonoBehaviour {
	public GameObject plusBox;
	public Color textColorPlus;
	public Color textColorMinus;

	private Text textBox;
	private bool isMoving = false;
	private Vector3 pos;
	private PseudoAnimation pseudoAnimation;

	void Start () {
		pseudoAnimation = GameObject.Find ("Cat").GetComponent<PseudoAnimation> ();
		pos = plusBox.transform.position;
		textBox = plusBox.GetComponent<Text> ();
		plusBox.SetActive (false);
	}

	void Update(){
		if(isMoving)
			plusBox.transform.Translate(0f, 50*Time.deltaTime, 0f);
	} 

	public void addPoint(int point){
		plusBox.SetActive (true);
		plusBox.transform.position = pos;
		isMoving = true;
		if (point >= 0) {
			textBox.text = "+" + point.ToString ();
			textBox.color = textColorPlus; 
			StopCoroutine("deactivate");
			StartCoroutine("deactivate");
		} else {
			textBox.text = point.ToString ();
			textBox.color = textColorMinus; 
			StopCoroutine("deactivate");
			StartCoroutine("deactivate");
		}
	}

	IEnumerator deactivate(){
		while (true){
			yield return new WaitForSeconds (3);
			isMoving = false;
			plusBox.SetActive (false);
			pseudoAnimation.Idle ();
		}
	}
}

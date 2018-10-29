using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drag : MonoBehaviour {
	public float speed;
	public float offset;

	private bool isDragged = false;
	private Vector3 inputPosition;
	private PseudoAnimation pseudoAnimation;

	// Use this for initialization
	void Start () {
		pseudoAnimation = GameObject.Find ("Cat").GetComponent<PseudoAnimation> ();
		offset += 10;
	}
	
	// Update is called once per frame
	void Update () {
		
		Cursor.lockState = CursorLockMode.Confined;
		inputPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);


		if (isDragged && Input.GetMouseButtonUp (0)) {
			isDragged = false;
			pseudoAnimation.Idle ();

		}
			
		if (Input.GetMouseButtonDown (0) && ((inputPosition - transform.position).magnitude <= offset)) {
				
			if (!isDragged)
				isDragged = true;
		}

		if (isDragged) {
			pseudoAnimation.Drag ();
			transform.position = Vector2.Lerp (transform.position, inputPosition, speed);

		}
	}
}

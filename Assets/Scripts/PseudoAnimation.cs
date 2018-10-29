using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoAnimation : MonoBehaviour {
	private SpriteRenderer spriteRenderer;

	public Sprite idle;
	public Sprite rub;
	public Sprite drag;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	public void Rub(){
		Debug.Log("rubbed");
		spriteRenderer.sprite = rub;
	}
	public void Drag(){
		spriteRenderer.sprite = drag;
	}
	public void Idle(){
		Debug.Log("idle");
		spriteRenderer.sprite = idle;
	}
}

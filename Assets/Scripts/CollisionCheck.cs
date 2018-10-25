using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Ball")
			Physics2D.IgnoreCollision (other.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D> ());
	}
}

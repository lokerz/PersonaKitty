using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour {
	private Quaternion rot;

	public void Rotate(GameObject x){
		x.transform.Rotate (0, 0, -90);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFix : MonoBehaviour {

	void Awake(){
		#if UNITY_STANDALONE
		Screen.SetResolution(405,648,false);
		Screen.fullScreen =false ;
		#endif
	}
}

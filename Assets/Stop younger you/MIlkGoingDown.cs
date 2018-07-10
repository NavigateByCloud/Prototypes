using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIlkGoingDown : MonoBehaviour {
	float width = 1f;
	float initLocalPosY = 0.36f;
	// Use this for initialization
	void Start () {
	//	initLocalPosY = transform.
	}
	
	// Update is called once per frame
	void Update () {
		float y = transform.parent.localPosition.y;
		Debug.Log(y);
		float squareS = width*width-y*y;
		if(squareS > 0)
			transform.localScale = Vector3.one* Mathf.Sqrt(width*width-y*y);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeInAndOut : MonoBehaviour {
	[SerializeField]float endValue = 0.5f, duration = 0.3f;
	[SerializeField]int repeatTime = 100000000;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().DOFade(endValue,duration).SetLoops(repeatTime,LoopType.Yoyo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

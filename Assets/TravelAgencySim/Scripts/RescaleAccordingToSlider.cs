using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RescaleAccordingToSlider : MonoBehaviour {
	public Slider slider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = slider.value * Vector3.one;
	}
}

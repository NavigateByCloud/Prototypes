using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocateTarget : MonoBehaviour {
	public Transform object1, object2;

	public float CameraSizeMin = 6.23f;
    public float speed = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (object1.position + object2.position) * 0.5f;

        Vector3 distance = object1.position - object2.position;
        float k = 0.5f;
		float targetSize = CameraSizeMin + k * distance.magnitude;

		float oldSize = Camera.main.orthographicSize;
		float x = (targetSize - oldSize) / Mathf.Abs(targetSize - oldSize);

		float newSize = Mathf.Lerp(oldSize, targetSize, Time.deltaTime * speed);
		Camera.main.orthographicSize = newSize;
	}
}

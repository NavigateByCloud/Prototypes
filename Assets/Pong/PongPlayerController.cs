using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerController : MonoBehaviour {
	public bool isPlayerRight;
	KeyCode UpKey,downKey, LeftKey, RightKey;
	Vector2 max;
	Vector2 min;
	public static float speed = 10f;

	// Use this for initialization
	public void initPos () {
		Bounds bounds = GetComponent<SpriteRenderer>().bounds;
		max = CameraExtensions.OrthographicBounds(Camera.main).max - bounds.extents;
		min = CameraExtensions.OrthographicBounds(Camera.main).min + bounds.extents;
		//min.x += bounds.size.x;

		if(isPlayerRight){
			UpKey = KeyCode.UpArrow;
			downKey = KeyCode.DownArrow;
			LeftKey = KeyCode.LeftArrow;
			RightKey = KeyCode.RightArrow;
			transform.position = new Vector3(max.x,0,0);
		}else{
			UpKey = KeyCode.W;
			downKey = KeyCode.S;
			LeftKey = KeyCode.A;
			RightKey = KeyCode.D;
			transform.position = new Vector3(min.x,0,0);
		}

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 input = Vector3.zero;

		if(Input.GetKey(UpKey)){
			input.y = 1;
		}

		if(Input.GetKey(downKey)){
			input.y = -1;
		}

//		if(Input.GetKey(LeftKey)){
//			input.x = -1;
//		}else if(Input.GetKey(RightKey)){
//			input.x = 1;
//		}


		if(input.magnitude > 0)
		{
			Vector3 movement = transform.position+input*speed*Time.deltaTime;
			if(movement.x > max.x)
			{
				movement.x = max.x - 0.1f;
			}else if(movement.x < min.x)
			{
				movement.x = min.x + 0.1f;
			}

			if(movement.y > max.y)
			{
				movement.y = max.y - 0.1f;
			}
			else if(movement.y < min.y){
				movement.y = min.y + 0.1f;
			}


			transform.position = movement;
		}
	}
}

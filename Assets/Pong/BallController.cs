using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	SpriteRenderer sr;
	Rigidbody2D rb;
	Vector3 dir;

	ScoreCounter scoreCounter;
	bool isBouncing = false;


	public void initPos(PongPlayerController playerController, ScoreCounter _scoreCounter){
		isBouncing = false;
		scoreCounter = _scoreCounter;
		rb = GetComponent<Rigidbody2D>();

		Vector3 pos = playerController.transform.position;

		sr = GetComponent<SpriteRenderer>();
		float radius = sr.bounds.extents.x+playerController.GetComponent<SpriteRenderer>().bounds.extents.x;
		if(playerController.isPlayerRight){
			pos.x -= radius;
			dir = new Vector3(-1,0,0);
		}else{
			pos.x += radius;
			dir = new Vector3(1,0,0);
		}

		transform.position = pos;
		Debug.Log("set parent to player paddle");
		transform.SetParent(playerController.transform);
	}

	public void Shot(){
		transform.parent = transform.parent.parent;
		rb.velocity = dir*10f;
		isBouncing = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
		if(transform.position.x > bounds.max.x - sr.bounds.extents.x
			||transform.position.x < bounds.min.x + sr.bounds.extents.x){
			Debug.Log("ball goes out of view");
			bool isRightPlayerLose;
			if(transform.position.x > 0){
				isRightPlayerLose = true;
			}else{
				isRightPlayerLose = false;
			}

			scoreCounter.UpdateScore(isRightPlayerLose);
		}else if(transform.position.y >= bounds.max.y
			||transform.position.y <= bounds.min.y){
			rb.velocity = new Vector2(rb.velocity.x,-rb.velocity.y);
		}



	}

	void OnCollisionEnter2D(Collision2D coll){
		if(isBouncing){
			Vector2 forceDir = transform.position - coll.transform.position;
			rb.velocity = forceDir.normalized*10f;
		}


	}
}

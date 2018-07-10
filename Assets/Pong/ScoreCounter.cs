using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GameState{
	READYTOSHOT,
	GAMING,
	END,
}

public class ScoreCounter : MonoBehaviour {
	public Text txtScore, txtNodification;
	float PlayerLeftScore, PlayerRightScore;
	public GameObject preBall;
	[SerializeField]BallController ballController;
	public PongPlayerController playerRight, playerLeft;
	[SerializeField]GameState state;
	// Use this for initialization
	void Start () {
		PlayerLeftScore = PlayerRightScore = 0;
		txtScore.text = PlayerLeftScore.ToString()+" : "+PlayerRightScore.ToString();

		StartGame();
	}


	void StartGame(){
		playerRight.initPos();
		playerLeft.initPos();

		//random a player to start playing ball
		GameObject go_ball = Instantiate(preBall);
		ballController = go_ball.GetComponent<BallController>();
		if(Random.value > 0.5f){
			//player right has the ball
			ballController.initPos(playerRight,this);

			//go_ball.transform.position = playerRight.position + go_ball.GetComponent<SpriteRenderer>();
		}else{
			ballController.initPos(playerLeft,this);
		}

		state = GameState.READYTOSHOT;
		txtNodification.text = "Press [SPACE] To Shot the Ball!";
	}

	public void UpdateScore(bool isRightPlayerLoseAPoint){
		Destroy(ballController.gameObject);

		if(isRightPlayerLoseAPoint){
			PlayerLeftScore ++;
			txtNodification.text = "Left Player got a point!";
		}else{
			PlayerRightScore ++;
			txtNodification.text = "Right Player got a point!";
		}
		txtScore.text = PlayerLeftScore.ToString()+" : "+PlayerRightScore.ToString();
		state = GameState.END;
	}

	// Update is called once per frame
	void Update () {
		if(state == GameState.READYTOSHOT){
			if(Input.GetKeyDown(KeyCode.Space)){
				ballController.Shot();
				txtNodification.text = "";
				state = GameState.GAMING;
			}
		}else if(state == GameState.END){
			if(Input.GetKeyDown(KeyCode.Space)){
				//Start();
				txtNodification.text = "Press [SPACE] To Begin the next Round!";
				StartGame();
			}
		}
	}
}

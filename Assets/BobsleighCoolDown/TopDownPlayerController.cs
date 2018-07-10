using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownPlayerController : MonoBehaviour {
	float playerSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {




		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}

		if (BobsleighController.isPause)
			return;


		if (Input.GetKeyDown(KeyCode.LeftShift)){
			playerSpeed = 10;
		}else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			playerSpeed = 5;
		}

		transform.rotation = Quaternion.identity;
		if(Input.GetKey("up"))//Press up arrow key to move forward on the Y AXIS
		{
			transform.Translate(0,playerSpeed * Time.deltaTime,0);
		}
		if(Input.GetKey("down"))//Press up arrow key to move forward on the Y AXIS
		{
			transform.Translate(0,-playerSpeed * Time.deltaTime,0);
		}
		if(Input.GetKey("left"))//Press up arrow key to move forward on the Y AXIS
		{
			transform.Translate(-playerSpeed * Time.deltaTime,0 ,0);
		}
		if(Input.GetKey("right"))//Press up arrow key to move forward on the Y AXIS
		{
			transform.Translate(playerSpeed * Time.deltaTime,0 ,0);
		}
	}
}

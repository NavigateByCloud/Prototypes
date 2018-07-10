using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

//	[SerializeField]Transform player1,player2;
	// Use this for initialization
	void Start () {
		//go_redLine.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && OneBlockMove.isPause == false){
			SceneManager.LoadScene("Gaming");
		}

//		if(Input.GetKeyDown(KeyCode.R)){
//			SceneManager.LoadScene(0);
//		}
	}
}

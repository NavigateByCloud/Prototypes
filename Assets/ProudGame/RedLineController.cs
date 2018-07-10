using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RedLineController : MonoBehaviour {
	[SerializeField]Text winText, spaceText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OneBlockMove.isPause == true && Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene("Proud");
			OneBlockMove.isPause = false;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log(coll.name+" wins!");
		if(OneBlockMove.isPause == false){
			OneBlockMove.isPause = true;
			winText.DOText( coll.name+" wins!",0.5f);
			spaceText.DOText("Press [R] to restart",0.5f);
		}

	}


}

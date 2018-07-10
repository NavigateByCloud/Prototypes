using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CharacterDying : MonoBehaviour {
	//Transform m_CeilingCheck;
	public static Text txtInfo;
	public static Image mask;
	public static bool isPause = false;
	public static GameObject btnReplay;
	Vector3 initpos;
	Vector3 initBtnReplayPos;
	// Use this for initialization
	void Start () {
		initpos = transform.position;
		txtInfo = GameObject.Find ("txtEndGame").GetComponent<Text>();
		mask = GameObject.Find ("Mask").GetComponent<Image>();
		btnReplay = GameObject.Find ("Button");
		initBtnReplayPos = btnReplay.transform.position;
		//m_CeilingCheck = transform.Find("CeilingCheck");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (isPause) {
			return;
		}

		if (coll.gameObject.name.Contains("tetrisbrick") ){

			if (coll.transform.parent.GetComponent<Boxes> ().enabled) {
				if (coll.transform.position.y < transform.position.y){
					Debug.Log (Mathf.Abs (coll.transform.position.x - transform.position.x));
					if( Mathf.Abs (coll.transform.position.x - transform.position.x) < 0.4f) {
						txtInfo.text = "Robot die!";
						gamePause ();
					}
				}

			}
		}else if(coll.gameObject.name.Equals("Exit")){
			//Debug.Log("Robot escape!");
			txtInfo.text = "Robot Escape!";
			gamePause ();
		}

	}

	public static void tetrixLose(){
		txtInfo.text = "Tetrix lose!";
		gamePause ();
	}


	public static void gamePause(){
		mask.DOFade (0.5f, 0.5f);
		//Time.timeScale = 0;
		isPause = true;
		btnReplay.transform.DOMove (txtInfo.transform.position - new Vector3 (0, 100, 0), 0.5f);
		btnReplay.transform.DOScale (0.8f*Vector3.one, 0.5f);
	}

	public void rePlay(){
		 
		gamePause ();

		Boxes.reset ();
		transform.position = initpos;

		StartCoroutine (waitforDestory());

	}

	IEnumerator waitforDestory(){
		Boxes[] boxes = FindObjectsOfType (typeof(Boxes)) as Boxes[];

		foreach (Boxes box in boxes) {
			Destroy (box.gameObject);
			yield return new WaitForSeconds (0.2f);
		}


		SpawnBox sb = FindObjectOfType (typeof(SpawnBox)) as SpawnBox;
		sb.SpawnNewBox ();

		txtInfo.text = "";
		mask.DOFade (0, 0.5f);
		btnReplay.transform.DOMove (initBtnReplayPos, 0.5f);
		btnReplay.transform.DOScale (0.3f*Vector3.one, 0.5f);

		isPause = false;
	}
}

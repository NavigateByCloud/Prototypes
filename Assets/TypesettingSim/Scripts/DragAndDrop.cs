using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class DragAndDrop : MonoBehaviour
{  
	private bool _mouseState;
	private GameObject target;
	public Vector3 screenSpace;
	public Vector3 offset;
	public Text text;
	public Transform stick;
	public GameObject indication;
	string[] passages;
	// Use this for initialization
	void Start ()
	{
		text.text = "Key words:";
		passages = new string[]{"game","truemind","impediments","remove","love","Love"};
	}

	void GameEnd(){
//		Debug.Log ("in");
		indication.GetComponent<Text> ().text = "You grab key words: ";
		bool iswin = false;
		foreach (string s in passages) {

			if (text.text.Contains (s)) {
				indication.GetComponent<Text> ().text += s+ " ";
				iswin = true;
			}
		}
		indication.GetComponent<Text> ().text += ", press R to grab more. ";
		indication.SetActive (true);
		if(!iswin)
			indication.GetComponent<Text> ().text = "You didn't grab any meaningful words. Press R to retry.";
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}

		if (indication.activeInHierarchy && Input.GetKeyDown (KeyCode.Space)) {
			indication.SetActive (false);
			stick.DOLocalMoveX (19, 0.5f, true).SetLoops (15, LoopType.Yoyo).OnComplete(GameEnd);
			//text.text = "Key words:"+passages[Random.Range(0,passages.Length-1)];
		}
		//
		if (indication.activeInHierarchy) {
			return;

		}

		// Debug.Log(_mouseState);
		if (Input.GetMouseButtonDown (0)) {

			RaycastHit hitInfo;
			target = GetClickedObject (out hitInfo);
			if (target != null && target.name.Length < 2) {
				//_mouseState = true;
				screenSpace = Camera.main.WorldToScreenPoint (target.transform.position);
				offset = target.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
				//Debug.Log (target.transform.position);\
				if (text.text.Contains (target.name)) {
					int index = text.text.IndexOf (target.name);
					target.transform.position = text.transform.position;
				}
				//target.transform.position = Vector3.zero;
				text.text += target.name; 
			}
		}
//		if (Input.GetMouseButtonUp (0)) {
//			_mouseState = false;
//		}
//		if (_mouseState) {
//			//keep track of the mouse position
//			var curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
//
//			//convert the screen mouse position to world point and adjust with offset
//			var curPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;
//
//			//update the position of the object in the world
//			target.transform.position = curPosition;
//		}
	}


	GameObject GetClickedObject (out RaycastHit hit)
	{
		GameObject target = null;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction * 10, out hit)) {
			target = hit.collider.gameObject;
		}


        

		return target;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PasserByGenerator : MonoBehaviour {
	public GameObject prePasserBy;
	static float timeGap = 1.5f;
	float timeCounter = 0;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (BobsleighController.isPause)
			return;

		if (transform.rotation != Quaternion.identity)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime);
			return;
		}

		if (timeCounter < timeGap){
			timeCounter+= Time.deltaTime;
		}else{
			timeCounter = 0;
			GeneratePasserBy();
		}

	}


	void GeneratePasserBy(){

		int numGenerate = Random.Range(1,3);

		for(int i = 0; i < numGenerate; i++){
			int lineIndex = Random.Range(0,2);

			Vector2 pos = Vector2.one;
			Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
			pos.x = Random.value*(bounds.max.x-bounds.min.x)+bounds.min.x;
			pos.y = lineIndex == 1? bounds.max.y:bounds.min.y;
			GameObject newPasserBy = Instantiate(prePasserBy,pos,Quaternion.identity);
			newPasserBy.GetComponent<NPCMoveController>().isForward = lineIndex == 0;
			newPasserBy.transform.GetChild(0).Rotate(new Vector3(0, 0, 180 * lineIndex));
			//_sr = GetComponent<SpriteRenderer>();
			newPasserBy.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
		}


	}

	

}





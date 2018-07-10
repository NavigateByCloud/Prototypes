using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RelocateScreen : MonoBehaviour {
	Transform player;
	SpawnBox spawnBox;
	float CameraSizeMin = 3, CameraSizeMax = 8;
	[SerializeField]float speed = 10;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		spawnBox = GameObject.Find("CreateBlockPoint").GetComponent<SpawnBox>();
	}
	
	// Update is called once per frame
	void Update () {
		if (CharacterDying.isPause) {
			return;
		}
		Vector3 pos = player.position - new Vector3 (0, 1, 0);
		transform.position = 0.5f*(pos + spawnBox.movingBlock.position);

		Vector3 distance = pos - spawnBox.movingBlock.position;

		float k = (CameraSizeMax - CameraSizeMin)/(16f-1f);
		float targetSize = CameraSizeMin+k*distance.magnitude;

		float oldSize = Camera.main.orthographicSize;
		float x =  (targetSize - oldSize)/Mathf.Abs(targetSize - oldSize);

		float newSize = Mathf.Lerp(oldSize,targetSize,Time.deltaTime*speed);
		Camera.main.orthographicSize = newSize;

	}
}

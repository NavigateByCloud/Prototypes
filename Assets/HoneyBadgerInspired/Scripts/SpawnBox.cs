using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour {

	public GameObject[] boxList;
	public Transform movingBlock;
	void Start () {
		SpawnNewBox();
	}

	public void SpawnNewBox() {
		int i = Random.Range(0, boxList.Length);
		movingBlock = Instantiate(boxList[i], transform.position, Quaternion.identity).transform;
	}
}
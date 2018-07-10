using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {
	public GameObject pre_block;
	float lastPosY;
    int index = 1;
    Color[] colors = { Color.green,Color.cyan, Color.blue, Color.magenta };
	public BlockController blockController;
    bool isfalling = true;
	[SerializeField]float timeCount = 0;
   // public bool isGameEnd = false;
	// Use this for initialization
	void Start () {
		lastPosY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (blockController.isGameEnd)
            return;

		if(timeCount <= 0){
			GenerateNewBlock();
			timeCount = Random.value+blockController.listCount/10f;
		}else{
			timeCount -= Time.deltaTime;
		}


	}

	void GenerateNewBlock(){
		float posY = lastPosY -0.1f- Random.value;

		GameObject go_block = Instantiate(pre_block,new Vector3(transform.position.x, posY,0),Quaternion.identity);
        int random = Random.Range(0, colors.Length);
        go_block.GetComponent<SpriteRenderer>().color = colors[random];
        float random2 = Random.value;
        isfalling = !isfalling;
        go_block.GetComponent<Block>().OnInit(isfalling,blockController.listCount);
		//go_block.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-1);
		go_block.name = "block" + index.ToString();
        index++;

		lastPosY = posY;
	}
}

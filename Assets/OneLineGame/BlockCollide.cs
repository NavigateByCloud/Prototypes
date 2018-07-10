using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollide : MonoBehaviour {
    
	
    void OnTriggerEnter2D(Collider2D other)
    {
      
        BlockController blockController = GameObject.Find("Player").GetComponent<BlockController>();
        if (blockController.isGameEnd)
            return;
        Debug.Log(this.name + " trigger with " + other.name);
        if(other.GetComponent<BlockCollide>() == null)
        {
            if (other.name == "Death")
            {
                blockController.gameEnd();
                return;
            }
            blockController.ColliderWith(other.transform, other.GetComponent<Block>().isFalling);
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        BlockController blockController = GameObject.Find("Player").GetComponent<BlockController>();
        if (blockController.isGameEnd)
            return;
        Debug.Log(this.name + " collide with " + other.transform.name);
        if (other.transform.GetComponent<BlockCollide>() == null)
        {
            if (other.transform.name == "Death")
            {
                blockController.gameEnd();
                return;
            }
            blockController.ColliderWith(other.transform, other.transform.GetComponent<Block>().isFalling);
        }

    }

}

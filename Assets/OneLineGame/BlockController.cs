using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour {
	float speed = -0.2f;
	bool isCollided = false;
	//GameObject collidedBlock;
	[SerializeField] List<Transform> tBlocks;
    const float GAB_BETWEEN_BLOCKS = 0.06f;
    public GameObject preParticle;
	public int listCount{
		get{
			return tBlocks.Count;
		}
	}
    float startY = 0;
    public Text txtScore;
    int score;
    public bool isGameEnd = false;
    //int iFirstBlockIndexInList = 0;
	// Use this for initialization
	void Start () {
		tBlocks = new List<Transform>();
		tBlocks.Add(transform.GetChild(0));
        
		//collidedBlock = null;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position += new Vector3(0,speed*Time.deltaTime,0);
        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(0);

        if (isGameEnd)
        {
            return;
        }

        //bool isForward;
		//switch sequance
		if(Input.GetKeyDown(KeyCode.UpArrow)){
            reorderBlocks(true);
		}else if(Input.GetKeyDown(KeyCode.DownArrow)){
            reorderBlocks(false);
		}

	}

    void reorderBlocks(bool isForward)
    {
        // Vector3 vStoredPos;
        
        Transform tMovedBlock;
        if (isForward)
        {

            tMovedBlock = tBlocks[0];
            tBlocks.RemoveAt(0);
            tBlocks.Add(tMovedBlock);
            
        }
        else
        {
            //the bottom one up to the top and others go down by one gap
            tMovedBlock = tBlocks[tBlocks.Count - 1];
            tBlocks.Remove(tMovedBlock);
            tBlocks.Insert(0, tMovedBlock);
        }
        //tBlocks[0].GetComponent<Collider2D>().enabled = true;

        orderBlocksByIndexInList();
       // Debug.Log("first block turns to be "+iFirstBlockIndexInList);
    }
	
    public void ColliderWith(Transform other,bool isFalling)
    {
        if (isFalling)
        {
            ColliderWithFalling(other);
        }
        else
        {
            ColliderWithUp(other);
        }
    }


    public void ColliderWithFalling(Transform other)
    {
        //Debug.Log(other.name+" enter");
        isCollided = true;
        Transform tBlock = other;

        if (tBlock.GetComponent<SpriteRenderer>().color
            != tBlocks[0].GetComponent<SpriteRenderer>().color)
        {
            startY--;
            //be the first of the line
            //other.GetComponent<Collider2D>().enabled = false;
            tBlocks[0].GetComponent<Collider2D>().enabled = false;

            tBlock.SetParent(transform);
            tBlock.gameObject.AddComponent<BlockCollide>();
            tBlock.GetComponent<Rigidbody2D>().gravityScale = 0;
            tBlock.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            tBlocks.Insert(0, tBlock);


        }
        else
        {
            startY++;
            //Create Color
            ParticleSystem.MinMaxGradient color = new ParticleSystem.MinMaxGradient();
            color.mode = ParticleSystemGradientMode.Color;
            color.color = tBlock.GetComponent<SpriteRenderer>().color;

            //Assign the color to your particle
            ParticleSystem particle1 = Instantiate(preParticle, tBlock.position, Quaternion.identity, transform.root).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule particleMain1 = particle1.main;
            particleMain1.startColor = color;
            particle1.Play();


            //destroy both
            Destroy(tBlocks[0].gameObject);
            Destroy(other.gameObject);
            tBlocks.RemoveAt(0);
            tBlocks[0].GetComponent<Collider2D>().enabled = true;

            score++;
            txtScore.text = score.ToString();
            //Debug.Log(tBlocks[0].gameObject.name);
        }
        Debug.Log(startY);
        orderBlocksByIndexInList();
    }
    public void ColliderWithUp(Transform other)
    {
        //Debug.Log(other.name+" enter");
        isCollided = true;
        Transform tBlock = other;
        int theCollidedIndex = tBlocks.Count - 1;

        if (tBlock.GetComponent<SpriteRenderer>().color
            != tBlocks[theCollidedIndex].GetComponent<SpriteRenderer>().color)
        {
            //be the last of the line
            //other.GetComponent<Collider2D>().enabled = false;
            tBlocks[theCollidedIndex].GetComponent<Collider2D>().enabled = false;

            tBlock.SetParent(transform);
            tBlock.gameObject.AddComponent<BlockCollide>();
            tBlock.GetComponent<Rigidbody2D>().gravityScale = 0;
            tBlock.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            tBlocks.Add(tBlock);


        }
        else
        {
            //Create Color
            ParticleSystem.MinMaxGradient color = new ParticleSystem.MinMaxGradient();
            color.mode = ParticleSystemGradientMode.Color;
            color.color = tBlock.GetComponent<SpriteRenderer>().color;

            //Assign the color to your particle
            ParticleSystem particle1 = Instantiate(preParticle, tBlock.position, Quaternion.identity, transform.root).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule particleMain1 = particle1.main;
            particleMain1.startColor = color;
            particle1.Play();


            //destroy both
            Destroy(tBlocks[theCollidedIndex].gameObject);
            Destroy(other.gameObject);
            
            tBlocks.RemoveAt(theCollidedIndex);
            Debug.Log(tBlocks.Count - 1);
            tBlocks[tBlocks.Count - 1].GetComponent<Collider2D>().enabled = true;

            score++;
            txtScore.text = score.ToString();
            //Debug.Log(tBlocks[0].gameObject.name);
        }
        orderBlocksByIndexInList();
    }
    public Text txtdie;
    public void gameEnd()
    {
        txtdie.enabled = true;
        isGameEnd = true;
        foreach(Transform tblock in tBlocks)
        {
            tblock.GetComponent<SpriteRenderer>().DOFade(0.2f, 0.8f).SetLoops(-1);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
		
    }

    void orderBlocksByIndexInList()
    {
        //int middle = Mathf.FloorToInt(tBlocks.Count / 2f);
        //Debug.Log(middle);
        for (int i = 0; i < tBlocks.Count; i++)
        {
            //tBlocks[i].localPosition = new Vector3(0, -GAB_BETWEEN_BLOCKS * i, 0);
			DOTween.Kill(tBlocks[i],true);
            tBlocks[i].DOLocalMoveY(-GAB_BETWEEN_BLOCKS * (startY+i), 0.1f);
        }
    }

}

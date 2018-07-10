using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowerGenerator : MonoBehaviour {
    public GameObject preFlower;
    public Transform tplayer;
    float timer = 0;
    float gap = 2f;
    float blackPercent = 1f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (FlowerPlayer.isGameEnd || Flower.isPause)
            return;



        timer += Time.deltaTime;
        if(timer > gap)
        {
            timer = 0;
            //new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0);
            bool isBlack = Random.value < blackPercent - FlowerPlayer.numBlackPetal*0.01f;
            GenerateFlower(isBlack);
        }


	}

    void GenerateFlower(bool isBlack)
    {
        Vector3 pos = Vector2.one;
        Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
        pos.x = Random.value * (bounds.max.x - bounds.min.x) + bounds.min.x;
        pos.y = bounds.max.y;
        pos.z = 0;
        float radian = Random.Range(0, 2 * Mathf.PI);

        Vector3 dir = tplayer.position - pos;
        Flower flower = Instantiate(preFlower, pos, Quaternion.identity, transform).GetComponent<Flower>();

        float _blackPercent = blackPercent - FlowerPlayer.numBlackPetal * 0.01f;
        float size = isBlack? 
            Random.Range(_blackPercent,_blackPercent * 2):
            Random.Range(1-_blackPercent, (1-_blackPercent) * 2);
        
        flower.initFlower(dir.normalized, isBlack,size+1);
        
    }

    public void ClearUp()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Flower>().DestroyFlower();
            //Destroy(child.gameObject);
        }

        
    }

    public void Continue()
    {
        ClearUp();
        blackPercent -= (8-FlowerPlayer.numBlackPetal)*0.1f;
        //tplayer.GetComponent<FlowerPlayer>().ResetPlayer();
    }

    public void Restart()
    {
        ClearUp();
        blackPercent = 1f;
        //tplayer.GetComponent<FlowerPlayer>().ResetPlayer();
    }
}

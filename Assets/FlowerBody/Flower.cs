using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flower : MonoBehaviour {
    [SerializeField] GameObject prePetal;
    Transform[] t_petals;
    [SerializeField] Vector3 dir = Vector3.zero;
    public bool isDestorying = false;
    public static bool isPause = false;
    // Use this for initialization
    void Start() {




    }

    public void initFlower(Vector3 _dir, bool isBlack, float _size)
    {

        t_petals = new Transform[8];
        dir = _dir;
        //Debug.Log(dir);
        for (int i = 0; i < 8; i++)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 360 / 8f * i));
            t_petals[i] = Instantiate(prePetal, transform.position, rotation, transform).transform;
            if (isBlack)
            {
                t_petals[i].GetComponent<SpriteRenderer>().color = Color.black;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.black;
            }
        }

        transform.localScale = Vector3.one * _size;
    }

    // Update is called once per frame
    void Update() {
        if (FlowerPlayer.isGameEnd || isDestorying || isPause)
            return;

        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 30f));

        transform.Translate(dir*5f * Time.deltaTime);
	}

    public void DestroyFlower()
    {
        isDestorying = true;

        transform.Find("field").GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < 8; i++)
        {
            Transform t_petal = t_petals[i];
            Vector3 endValue = t_petal.position + t_petal.up * 2f;
            t_petal.GetComponent<Collider2D>().enabled = false;

            t_petals[i].DOMove(endValue, 0.5f);
            t_petal.DOScale(0, 0.3f).SetDelay(0.1f);
            transform.Find("field").GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            transform.Find("field").DOScale(transform.localScale.x * 1.1f, 1f);
            t_petal.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetDelay(0.2f).OnComplete(()=>
            {
                Destroy(gameObject);
            });

            
        }

        //transform.Find("field").GetComponent<SpriteRenderer>().DOFade\

        
    }
}

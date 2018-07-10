using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FlowerPlayer : MonoBehaviour {
    public float speed;
    int health = 8;
    public static int numBlackPetal = 8;
    public GameObject prePetal;
    List<Transform> t_attractions;
    public Transform tMask;
    Vector3 initpos;
    [SerializeField] FlowerGenerator flowerGenerator;
    [SerializeField]Text textIndicator, textDescription;
    //FlowerGenerator

    public static bool isGameEnd;
	// Use this for initialization
	void Start () {
        health = 8;
        numBlackPetal = 8;
        t_attractions = new List<Transform>();
        isGameEnd = true;
        initpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            numBlackPetal = 8;
            flowerGenerator.Restart();
            textDescription.DOText("Flowers", 1f);
            textIndicator.DOText("Use WASD or Arrows | Press [SPACE] to begin", 1f);
            ResetPlayer();
            GetComponent<SpriteRenderer>().DOFade(0, 0.2f);
            transform.position = initpos;
            GetComponent<SpriteRenderer>().DOFade(1, 0.2f).SetDelay(0.2f);
        }

        if (FlowerPlayer.isGameEnd)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
            {
                if (FlowerPlayer.numBlackPetal > 0)
                {
                    flowerGenerator.Continue();
                    textDescription.DOText(" ", 1f);
                    textIndicator.DOText(" ", 1f);
                }

                ResetPlayer();
                isGameEnd = false;
            }
            return;
        }

        if (Flower.isPause)
            return;

        tMask.position = transform.position;

        Vector3 movement = Vector3.zero;
        Vector3 max = CameraExtensions.OrthographicBounds(Camera.main).max;
        Vector3 min = CameraExtensions.OrthographicBounds(Camera.main).min;

        float x = Input.GetAxis("Horizontal");
        float nextX = transform.position.x + x * speed * Time.deltaTime;
        //if (nextX+1f < max.x && nextX-1f > min.x)
        //{
        movement.x = nextX;//x * speed * Time.deltaTime;//transform.Translate(x * speed * Time.deltaTime, 0, 0);
        //}
        


        float y= Input.GetAxis("Vertical");
        float nextY = transform.position.y + y * speed * Time.deltaTime;
        //if (nextY+1f < max.y && nextY-1f > min.y)
        //{
        movement.y = nextY;//y * speed * Time.deltaTime;
        //}

        if(movement.magnitude > 0)
        {
            //print(movement);
            transform.position += movement;
            if(movement.x > max.x)
            {
                movement.x = max.x;
            }else if(movement.x < min.x)
            {
                movement.x = min.x;
            }

            if(movement.y > max.y)
            {
                movement.y = max.y;
            }
            else if(movement.y < min.y){
                movement.y = min.y;
            }


            transform.position = movement;

            transform.Rotate(new Vector3(0, 0, -x*10f));
        }
        

        if (t_attractions.Count > 0)
        {
            Vector3 dir = Vector3.zero;
            foreach (Transform t_attraction in t_attractions)
            {
                if (t_attraction)
                {
                    Vector3 attrack = t_attraction.position - transform.position;
                    float force = t_attraction.localScale.x / attrack.magnitude;
                    dir += attrack.normalized * force;
                }
            }

            transform.Translate(dir * Time.deltaTime * speed*1.5f);
        }
        

    }

    private Transform ove()
    {
        return transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameEnd||Flower.isPause)
            return;

        //Debug.Log(collision.collider.name);
        Flower flower = collision.collider.transform.parent.GetComponent<Flower>();
        if (flower&&flower.isDestorying == false)
        {
            Flower.isPause = true;
            flower.DestroyFlower();

            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 360 / 8f * (8 - health)));
            GameObject go_petal = Instantiate(prePetal, transform);
            go_petal.transform.localRotation = rotation;
            go_petal.transform.position = transform.position;
            go_petal.GetComponent<SpriteRenderer>().color = collision.collider.GetComponent<SpriteRenderer>().color;
            go_petal.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            go_petal.transform.localScale = Vector3.zero;

            if (go_petal.GetComponent<SpriteRenderer>().color == Color.white)
            {
                numBlackPetal--;
            }

            if (t_attractions.Contains(flower.transform) == true)
            {
                //print("attraction remove:" + flower.name);
                t_attractions.Remove(flower.transform);
            }

            health--;

            go_petal.transform.DOScale(1f, 0.3f).SetDelay(0.6f).OnComplete(()=>
            {
                Flower.isPause = false;
                //generate a petal?
                if (health == 0)
                {
                    //print("game ends!");
                    //show the indication
                    if (numBlackPetal == 0)
                    {
                        textDescription.DOText("Darkness is no longer here.", 1f);
                        textIndicator.DOText("Press [R] to restart", 1f);
                    }
                    else
                    {
                        textDescription.DOText("Your generation ends. But you've made some changes...", 1f);
                        textIndicator.DOText("Press [SPACE] to the next generation...", 1f);
                    }
                    isGameEnd = true;
                }
            });

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Flower flower = collision.transform.parent.GetComponent<Flower>();
        if (flower)
        {
            if(t_attractions.Contains(flower.transform) == false)
            {
                //print("attraction add:" + flower.name);
                t_attractions.Add(flower.transform);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Flower flower = collision.transform.parent.GetComponent<Flower>();
        if (flower)
        {
            if (t_attractions.Contains(flower.transform) == true)
            {
                //print("attraction remove:" + flower.name);
                t_attractions.Remove(flower.transform);
            }

        }
    }

    public void ResetPlayer()
    {
        foreach(Transform t_petal in transform)
        {
            //Transform t_petal = child;
            Vector3 endValue = t_petal.position + t_petal.up * 2f;
            t_petal.GetComponent<Collider2D>().enabled = false;

            t_petal.DOMove(endValue, 0.5f);
            t_petal.DOScale(0, 0.5f);
            t_petal.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetDelay(0.2f).OnComplete(() =>
            {
                Destroy(t_petal.gameObject);
            });
        }

        
        float red = 251 - (251 - 49) / 8f * numBlackPetal;
        float greenAndBlue = 203 - (203 - 45) / 8f * numBlackPetal;
        Color newColor = new Color(red/255f,greenAndBlue/255f,greenAndBlue/255f,2f);
        Camera.main.DOColor(newColor, 0.5f);

         red = 249 - (249 - 133) / 8f * numBlackPetal;
         greenAndBlue = 108 - (108 - 103) / 8f * numBlackPetal;
        newColor = new Color(red / 255f, greenAndBlue / 255f, greenAndBlue / 255f, 2f);

        tMask.GetComponent<SpriteRenderer>().DOFade(31 / 255f + (8-numBlackPetal) * 0.1f, 1f);

        health = 8;
        numBlackPetal = 8;
        t_attractions.Clear();
        
        t_attractions = new List<Transform>();
        print(t_attractions.Count);
        
    }
}

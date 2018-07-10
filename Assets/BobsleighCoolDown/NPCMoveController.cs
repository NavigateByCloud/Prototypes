using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPCMoveController : MonoBehaviour {
	public bool isForward;
    public Transform tAiming;
    float _speed;
    float timer = 0;
    protected SpriteRenderer _sr;
    public static bool isPause = false;
    //protected bool isPause = false;
    [SerializeField]protected float pauseTime = 0;
    // Use this for initialization
    void Start () {
        _speed = isForward ? 1 : -2;
        _sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (BobsleighController.isPause)
            return;


        timer += Time.deltaTime;
        if (timer > 1f)
        {
            Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
            bounds.Expand(0.3f);
            float posY = transform.position.y;
            //Debug.Log()
            if (posY > bounds.max.y || posY < bounds.min.y)
            {
                Destroy(gameObject);
                return;
            }
        }

        Move(_speed);

	}

    protected void Move(float speed)
    {
        
        if (pauseTime > 0)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime < 0)
            {
                pauseTime = 0;
            }

            if (transform.rotation != Quaternion.identity)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime);
           
            return;
        }

        if (transform.rotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime);
            return;
        }

        float x = -1;//isForward==false? -1f:1f;
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 2f, x * transform.up, 5f);
        float xMovement = 0;
        float minDis = Mathf.Infinity;
        if (hit.Length > 0)
        {

            for (int i = 0; i < hit.Length; i++)
            {

                Transform t = hit[i].transform;
                if (t != transform)
                {
                    Vector3 v1 = t.position - transform.position;
                    Vector3 v2 = tAiming.position - transform.position;

                    float fDot = Vector3.Dot(v1.normalized, v2.normalized);
                    if (fDot > Mathf.Cos(Mathf.Deg2Rad * 35))
                    {
                        //Debug.Log(t.name + "in view");
                        float newDis = v1.magnitude;
                        if(newDis < minDis)
                        {
                            minDis = newDis;
                            float a = v1.x > 0 ? 1 : -1;
                            xMovement = -x * (2.75f * Mathf.Tan(Mathf.Deg2Rad * 35)-Mathf.Abs(v1.x)) /(Mathf.Abs(v1.y)/speed);
                        }
                    }
                }

            }
        }

        //Debug.Log(transform.name + speed);
        transform.Translate(xMovement * Time.deltaTime, speed * Time.deltaTime, 0);
    }

    bool isComplete = true;
    static float increaseTime = 2f;
    void OnCollisionEnter2D(Collision2D collider)
    {

        pausingTime();
    }

    protected void pausingTime()
    {
        if (isComplete)
        {
            isComplete = false;
            pauseTime += increaseTime;
            float duration = 0.2f;
            _sr.DOFade(0.5f, duration).SetLoops(Mathf.FloorToInt(increaseTime/duration), LoopType.Yoyo).OnComplete(PauseTweenComplete);


        }
    }

    void PauseTweenComplete()
    {
        isComplete = true;
    }

}

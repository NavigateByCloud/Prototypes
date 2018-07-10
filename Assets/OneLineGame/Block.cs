using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour {
    public bool isFalling = true;
    // Use this for initialization
    public void OnInit(bool _isFalling,int count) {
        Bounds orthographicBounds = CameraExtensions.OrthographicBounds(Camera.main);
        isFalling = _isFalling;
        if (isFalling){
            transform.position = orthographicBounds.center + new Vector3(0, orthographicBounds.extents.y, 10);
            StartCoroutine(WaitAndPrint(0.02f * (30 - count)));
            
            //GetComponent<Rigidbody2D>().gravityScale = 0.02f * (30 - count);
            //Debug.Log("falling with "+GetComponent<Rigidbody2D>().gravityScale);
        }
        else
        {
            transform.position = orthographicBounds.center + new Vector3(0, -orthographicBounds.extents.y, 10);
            //GetComponent<Rigidbody2D>().gravityScale = -0.02f * (30 - count);
            StartCoroutine(WaitAndPrint(-0.02f * (30 - count)));
            //Debug.Log("up with " + GetComponent<Rigidbody2D>().gravityScale);
        }
	    
        //Bounds bounds = 
       
	}

    IEnumerator WaitAndPrint(float gravityScale)
    {
        int x = isFalling ? -1 : 1;
        transform.DOPunchPosition(new Vector3(0, 0.1f * x, 0), 0.5f);//.SetLoops(2, LoopType.Yoyo);
        yield return new WaitForSeconds(0.55f);
        //print("WaitAndPrint " + Time.time);
        GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    }

    // Update is called once per frame
    void Update () {

		

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class FishTopDownController : MonoBehaviour {
	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;
	public Transform[] Skeleton_Joints;
	public KeyCode UpKey, LeftKey, RightKey;
	Rigidbody rb;
	Quaternion originalRotation;
	float fFat = 0.1f;
	const float MINFAT = 0.08f, MAXFAT = 0.25f;
	public FoodGenerator foodGen;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		originalRotation = rb.rotation;
//		Debug.Log(transform.rotation);
		//Skeleton_Joints
	}

	// Update is called once per frame
	void Update () {

		if(FoodGenerator.isGameEnd){
			return;
		}
		speed = 1f/transform.localScale.x;
		rotationSpeed = 10f/transform.localScale.x;


		if (BobsleighController.isPause)
			return;

		bool isMove = false;
		if(Input.GetKey(UpKey)){
			isMove = true;
			float translation = speed;
			translation *= Time.deltaTime;
			transform.Translate(0, 0, translation);
//			Debug.Log(transform.forward);
			//rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
		}

		float rotation = 0;
		if(Input.GetKey(LeftKey)){
			isMove = true;
			rotation = -rotationSpeed;
			//rotation *= Time.deltaTime;
			//transform.Rotate(0, rotation, 0);

		}

		if(Input.GetKey(RightKey)){
			isMove = true;
			rotation +=  1* rotationSpeed;
		}
		rotation *= Time.deltaTime;

		transform.Rotate(0, rotation, 0);
		//Debug.Log(transform.rotation);

		if(isMove){
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			if(transform.localScale.x > MINFAT){
				transform.localScale -= new Vector3(0.002f*Time.deltaTime,0,0);
				
			}
			//transform.rotation = Quaternion.Euler()
			//rb.MoveRotation(Quaternion.Euler(rb.rotation.x,originalRotation.y,originalRotation.z));
		}

//		for(int i =1; i<Skeleton_Joints.Length; i++){
//			//joint.
//		
//			if(Skeleton_Joints[i].transform.rotation.eulerAngles.y > 140){
//				if(Skeleton_Joints[i].transform.rotation.eulerAngles.y < 220){
//					Skeleton_Joints[i].Rotate(0,rotation,0);
//				}else{
//					if(rotation < 0){
//						Skeleton_Joints[i].Rotate(0,rotation,0);
//					}
//				}
//			}else {
//				if(rotation > 0){
//					Skeleton_Joints[i].Rotate(0,rotation,0);
//				}
//
//			}
//			
//				
//		}


	}



	void OnTriggerEnter(Collider col){
		//Debug.Log(col.name);
		if(col.transform.name.Contains("food")){
			SoundManger.instance.PlaySingle(SoundManger.instance.Sounds[0]);
			col.transform.DOScale(Vector3.zero,0.5f).OnComplete(()=>{
				Destroy(col.gameObject);
				
			});
			col.enabled = false;
			if(transform.localScale.x > MINFAT && transform.localScale.x < MAXFAT){
				transform.localScale = transform.localScale + new Vector3(0.05f,0,0);
			}else{
				
			}
			
		}
	}
}

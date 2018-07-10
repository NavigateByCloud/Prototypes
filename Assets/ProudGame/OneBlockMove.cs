using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class OneBlockMove : MonoBehaviour {
	public Transform t_background;
	const float f_step = 0.05f;
	[SerializeField]bool isPlayer1 = false;
	Collider2D m_collider;
	KeyCode upKey, downKey, leftKey, rightKey;
	public static bool isPause = false;

	// Use this for initialization
	void Start () {
		if(isPlayer1){
			upKey = KeyCode.UpArrow;
			downKey = KeyCode.DownArrow;
			leftKey = KeyCode.LeftArrow;
			rightKey = KeyCode.RightArrow;
		}else{
			upKey = KeyCode.W;
			downKey = KeyCode.S;
			leftKey = KeyCode.A;
			rightKey = KeyCode.D;
		}
		m_collider = t_background.GetComponent<Collider2D>();


	}
	
	// Update is called once per frame
	void Update () {
		if(isPause)
			return;

		if (Input.GetKey("escape"))
			Application.Quit();
		

		if(m_collider.bounds.Contains(transform.position)){

			if(Input.GetKeyUp(upKey)){
				transform.Translate(new Vector3(0,f_step,0));
			}
			if(Input.GetKeyUp(downKey)){
				transform.Translate(new Vector3(0,-f_step,0));
			}
			if(Input.GetKeyUp(leftKey)){
				transform.Translate(new Vector3(-f_step,0,0));
				//if(m_collider.bounds.Contains(transform.position)){

					t_background.transform.RotateAround(transform.position,Vector3.forward,90f);
				//}
			}
			if(Input.GetKeyUp(rightKey)){
				transform.Translate(new Vector3(f_step,0,0));
				//if(m_collider.bounds.Contains(transform.position)){
					t_background.transform.RotateAround(transform.position,Vector3.forward,-90f);

				//}
			}


		}else{
			
			if(Input.GetKey(upKey)){
				transform.Translate(new Vector3(0,f_step*Time.deltaTime*50f,0));
			}
			if(Input.GetKey(downKey)){
				transform.Translate(new Vector3(0,-f_step*Time.deltaTime*50f,0));
			}
			if(Input.GetKey(leftKey)){
				transform.Translate(new Vector3(-f_step*Time.deltaTime*50f,0,0));
			}
			if(Input.GetKey(rightKey)){
				transform.Translate(new Vector3(f_step*Time.deltaTime*50f,0,0));

			}
		}





	}
}

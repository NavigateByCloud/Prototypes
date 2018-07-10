using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FoodGenerator : MonoBehaviour {
	public Transform[] walls;
	float maxX,maxY,minX,minY;
	float timer = 0f, gap = 2f;
	public GameObject prefab_Food;
	public Image bar,clock;
	const float gamingTime = 60f;
	float gameEndTimer = 0;
	bool isStart = false;
	public Text txtTitle,txtInstruction,txtInstruction1,txtInstruction2,txtInstruction3;
	public Image panel;
	public static bool isGameEnd = false;
	public Transform player1,player2;

	// Use this for initialization
	void Start () {
		txtInstruction.DOFade(0.2f,1f).SetLoops(-1,LoopType.Yoyo);	
		//walls = transform.chi
		Bounds bounds = CameraExtensions.OrthographicBounds(Camera.main);
		//Transform[] children = transform.GetComponentsInChildren<Transform>();
		walls[0].position = bounds.center+new Vector3(bounds.extents.x,-bounds.center.y,0);
		walls[0].localScale = new Vector3(2,2,bounds.size.y);
		walls[1].position = bounds.center+new Vector3(-bounds.extents.x,-bounds.center.y,0);
		walls[1].localScale = new Vector3(2,2,bounds.size.y);
		maxX = walls[0].position.x-2;
		minX = walls[1].position.x+2;

		walls[2].position = bounds.center+new Vector3(0,-bounds.center.y,bounds.extents.y);
		walls[2].localScale = new Vector3(bounds.size.x,2,2);
		walls[3].position = bounds.center+new Vector3(0,-bounds.center.y,-bounds.extents.y);
		walls[3].localScale = new Vector3(bounds.size.x,2,2);
		maxY = walls[2].position.z-2;
		minY = walls[3].position.z+2;

		float width = 0;
		bar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,0f);// = new Vector3(maxX-minX,1,1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("escape"))
			Application.Quit();
		
		if(Input.GetKeyDown(KeyCode.Space)){
			isGameEnd = false;
			clock.DOFade(0.5f,1f);
			isStart = true;
			panel.DOFade(0.2f,2f);
			txtTitle.DOText("",2f);
			DOTween.Kill(txtInstruction);
			txtInstruction.DOText("",2f);
			txtInstruction1.DOText("",2f);
			txtInstruction2.DOText("",2f);
			txtInstruction3.DOText("",2f);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
			isGameEnd = false;
		}

		if(isStart == false || isGameEnd){
			return;
		}

		if(gameEndTimer<gamingTime){
			gameEndTimer += Time.deltaTime;
//			sb.value = gameEndTimer/gamingTime;

//			Debug.Log(Screen.width*gameEndTimer/gamingTime);
			clock.fillAmount = 1-gameEndTimer/gamingTime;
			bar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width*gameEndTimer/gamingTime);//bar.rectTransform.rect.width = Screen.width*gameEndTimer/gamingTime; //= new Vector3((maxX-minX)*gameEndTimer/gamingTime,1f,1f);
		}else{
			isGameEnd = true;
			panel.DOFade(0.6f,0.5f);
			string winner = player1.localScale.x > player2.localScale.x? player1.name:player2.name;
			txtTitle.DOText("Game end!",0.5f).OnComplete(()=>{
				txtTitle.DOText(winner + " win!",0.5f);
				//DOTween.Kill(txtInstruction);
				txtInstruction.DOText("Press [R] to Replay",0.5f);
			});

			//isStart = false;
			return;
		}

		if(timer<gap){
			timer+=Time.deltaTime;

		}else{
			timer = 0;
			gap = 3f+Random.value;
			float posX = Random.Range(minX,maxX);
			float posY = Random.Range(minY,maxY);
			//Debug.Log(minY+"+"+maxY);

			GameObject go = Instantiate(prefab_Food,new Vector3(posX,0f,posY),Quaternion.Euler(90f,0f,Random.Range(0f,360f)));
			go.transform.localScale = Vector3.one*0.1f;
			go.transform.DOScale(Vector3.one,1.5f);
			SoundManger.instance.PlaySingle(SoundManger.instance.Sounds[1]);

		}
		//float x = walls;


	}
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BobsleighController : NPCMoveController {
	[SerializeField]float fPressure = 100f;
	float fGrowingSpeed = 10f, fReducingSpeed = 4f;
	float maxPressure = 100f, minPressure = 0f;
	bool isPressureGrowing = false;
	public Color cMinPressure,cMaxPressure;
	public SpriteRenderer outCircle;
	public Text txtScore,txtMiles;
	//float miles;
	//SpriteRenderer _sr;
	Color originalColor;
	float originalAlpha;
	float fMilesTotoal = 100;
	public static bool isPause = false;


	// Use this for initialization
	void Start () {
		isPause = false;
	   // miles = fMilesTotoal;
		isForward = false;
		_sr = GetComponent<SpriteRenderer>();
		originalAlpha = _sr.color.a;
		originalColor = outCircle.color;

	}
	
	// Update is called once per frame
	void Update () {
		float miles = fMilesTotoal + transform.position.y;
		if(miles < 0)
		{
			miles = 0;
			// txtMiles.text = Mathf.FloorToInt(miles).ToString() + "m left";
			if (fPressure <= 0)
			{
				txtMiles.text = "Cooldown succeeds!";
			}
			else
			{
				txtMiles.text = "Cooldown fails!";
			}
			isPause = true;
		}
		


		if (isPause)
			return;

		txtMiles.text = Mathf.FloorToInt(miles).ToString() + "m left";

		if (!isPressureGrowing){
			fPressure-= Time.deltaTime*fReducingSpeed;
			if(fPressure < 0f){
				fPressure = 0;
			}
			
		}else {
			fPressure+=Time.deltaTime*fGrowingSpeed;
			//if(fPressure > 150f){
			//	fPressure = 150f;
			//}

		}

		_sr.color = Color.Lerp(cMaxPressure,cMinPressure,fPressure/maxPressure);
		txtScore.text = Mathf.RoundToInt(fPressure).ToString();
	   
		//txtScore.color = _sr.color; 
		Move(-1f);

	}



	void OnTriggerEnter2D(Collider2D collider){

		isPressureGrowing = true;
		//outCircle.DOFade(originalAlpha-0.5f,0.2f).SetLoops(-1,LoopType.Yoyo);
		Color endValue = cMinPressure;
		endValue.a = originalAlpha - 0.5f;
		outCircle.DOColor(endValue, 0.2f).SetLoops(-1, LoopType.Yoyo);
	}


	void OnTriggerExit2D(Collider2D collider)
	{
		isPressureGrowing = false;

		DOTween.Kill(outCircle);
		outCircle.DOColor(originalColor, 0.2f).SetDelay(0.2f);
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		pausingTime();
	}

}

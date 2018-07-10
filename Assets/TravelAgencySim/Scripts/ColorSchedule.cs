using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ColorSchedule : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {
	Image image;
	public static bool isColoring = false;
	Color storedColor;
	public Text description;
	public Activity activity;
	public static int index = 0;
	static GameObject btnSubmit;
	public string cityName;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		storedColor = image.color;
		description = GetComponentInChildren<Text> ();
		btnSubmit = GameObject.Find ("BtnSubmit");

		//btnSubmit.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reset(){
		index = 0;
		description.text = "";
		storedColor = Color.white;
		isColoring = false;
		image.color = storedColor;
	}

	public void OnPointerClick(PointerEventData eventData){
		addNewColor ();
	}

	void addNewColor(){
		if (ShowCityInfo.color == Color.white) {
			return;
		}

		if (storedColor == Color.white && ShowCityInfo.color != Color.white) {
			index++;Debug.Log (index);
			if (index >= 45) {
				btnSubmit.GetComponent<Button>().interactable = true;
			}
		}
		image.color = ShowCityInfo.color;
		storedColor = image.color;
		activity = ShowCityInfo.activity;
		cityName = MouseOverShowInfo.selectedName;
		description.text = ShowCityInfo.activity+" @"+ MouseOverShowInfo.selectedName;
	}

	public void OnPointerEnter(PointerEventData eventData){
		if (isColoring) {
			addNewColor ();
		} else {
			image.color = Color.grey;
		}
	}

	public void OnPointerExit(PointerEventData eventData){
		if (isColoring) {
			addNewColor ();
		} else {
			
			image.color = storedColor;
		}
	}

	public void OnPointerDown(PointerEventData eventData){
		isColoring = true;
	}

	public void OnPointerUp(PointerEventData eventData){
		isColoring = false;
	}
}

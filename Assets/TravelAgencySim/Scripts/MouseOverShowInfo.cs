using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum AttractionType{
	Natural_Beauty,
	Cultural_and_Historic,
}

public class MouseOverShowInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IBeginDragHandler,IDragHandler,IDropHandler,IPointerClickHandler{
	//Button button;
	public static Transform infoNode;
	public int numStar;
	public AttractionType type;
	public int cost;
	public Vector2Int openTime,costTime;
	public string name, description;
	//public int timeSpentEstimate;
	bool isEnter = false;
	GameObject prefab;
	GameObject draggingBlock;
	Transform t_Canvas;
	public static bool isSelecting = false;
	public static string selectedName = "";
	//public GameObject buttons;

	// Use this for initialization
	void Start () {
		infoNode = GameObject.Find ("PanelInfo").transform;
		prefab = Resources.Load<GameObject> ("Prefabs/Timeblock1");
		//infoNode.position = new Vector3 (-10000, -10000, 0);
	//	button = this.GetComponent<Button> ();
		t_Canvas = GameObject.Find("Canvas").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnBeginDrag(PointerEventData eventData){
		//draggingBlock = Instantiate (prefab, eventData.position, Quaternion.identity,t_Canvas);

	}

	public void OnDrag(PointerEventData eventData){
		//draggingBlock.transform.position = eventData.position;

	}

	public void OnDrop(PointerEventData eventData){
		//draggingBlock = null;
		//Debug.Log ("Drop");
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		isSelecting = true;
		selectedName = this.name.Substring (0);
		infoNode.GetComponent<ShowCityInfo> ().showButtons ();
		//buttons.SetActive (true);
		Debug.Log (selectedName);
	}



	public void OnPointerEnter(PointerEventData eventData)
	{
		//If your mouse hovers over the GameObject with the script attached, output this message
		if (isSelecting) {
			return;
		}
		Debug.Log("Mouse is over "+this.name);
		//show the info
		//infoNode.position = transform.position + infoNode.GetComponent<RectTransform>().rect.size;
		isEnter = true;

		infoNode.GetComponent<ShowCityInfo> ().show (name, description, cost, openTime, costTime, numStar);


	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//The mouse is no longer hovering over the GameObject so output this message each frame
		Debug.Log("Mouse is no longer on "+this.name);
		if (isEnter && isSelecting == false) {
			isEnter = false;
			infoNode.GetComponent<ShowCityInfo> ().hide ();
			//infoNode.position = new Vector3 (-10000, -10000, 0);
		}
		//reveal

	}
}

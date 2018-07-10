using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndExpand : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler {
	public bool dragOnsurfaces = true;
	private GameObject m_DraggingIcon;
	private RectTransform m_DraggingPlane;
	public Sprite icon;

	// Use this for initialization
	public void OnBeginDrag (PointerEventData eventData) {
//		Debug.Log("begin");
		//var canvas = GameObject.Find ("Canvas");
		var canvas = FindInparents<Canvas>(gameObject);
		if (canvas == null)
			return;

		m_DraggingIcon = new GameObject ("icon");
		m_DraggingIcon.transform.SetParent (canvas.transform,false);
		m_DraggingIcon.transform.SetAsLastSibling ();

		var image = m_DraggingIcon.AddComponent<Image> ();
		image.sprite = GetComponent<Image> ().sprite;
		image.color = GetComponent<Image> ().color;
		//image.SetNativeSize ();

		if (dragOnsurfaces)
			m_DraggingPlane = transform as RectTransform;
		else
			m_DraggingPlane = canvas.transform as RectTransform;
	}


	public void OnDrag(PointerEventData eventData){
		if (m_DraggingIcon != null) {
			SetDraggedPosition (eventData);
		}

	}

	void SetDraggedPosition(PointerEventData data){
//		Debug.Log("in");
		if (dragOnsurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null) {
			
			m_DraggingPlane = data.pointerEnter.transform as RectTransform;
			Debug.Log (m_DraggingPlane);
		}
		var rt = m_DraggingIcon.GetComponent<RectTransform> ();
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle (m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos)) {
			//GetComponent<RectTransform> ().position = globalMousePos;
			Vector3 pos = GetComponent<RectTransform> ().position;
			if (!_isVertical) {
				
				GetComponent<RectTransform> ().position = new Vector3 (pos.x, globalMousePos.y, pos.z);

			} else {

				GetComponent<RectTransform> ().position = new Vector3 (globalMousePos.x, pos.y, pos.z);
			}

			_line1.GetComponent<DragAndExpand> ().updateLength ();
			_line2.GetComponent<DragAndExpand> ().updateLength ();

			rt.position = globalMousePos;
			Debug.Log (globalMousePos);
			//rt.rotation = m_DraggingPlane.rotation;
		}

			


	}

	public void OnEndDrag(PointerEventData eventData){
		if (m_DraggingIcon != null)
			Destroy (m_DraggingIcon);

	}

	static public T FindInparents<T>(GameObject go) where T:Component{
		if (go == null)
			return null;

		var comp = go.GetComponent<T> ();

		if (comp != null)
			return comp;

		Transform t = go.transform.parent;
		while (t != null && comp == null) {

			comp = t.gameObject.GetComponent<T> ();
			t = t.parent;
		}


		return comp;

	}

	RectTransform _line1,_line2,poin1,point2;
	bool _isVertical;
	RectTransform rt;
	RectTransform _text;

	void Start(){
		rt = GetComponent<RectTransform> ();
	}

	public void initLine(RectTransform line1, RectTransform line2, bool isVertical,RectTransform text){
		_line1 = line1;
		_line2 = line2;
		_isVertical = isVertical;
		_text = text;
	}
		
	public void updateLength(){
		Vector2 oldPos = rt.anchoredPosition;
		Vector2 newPos = (_line1.anchoredPosition + _line2.anchoredPosition) * 0.5f;
		float length = Vector2.Distance (_line1.anchoredPosition, _line2.anchoredPosition);

		if (_isVertical) {
			rt.anchoredPosition = new Vector2 (oldPos.x, newPos.y);
			rt.sizeDelta = new Vector2 (10, length);

			_text.anchoredPosition = new Vector2 (_text.anchoredPosition.x, newPos.y);
			_text.sizeDelta = new Vector2 (_text.sizeDelta.x, length);

		} else {
			rt.anchoredPosition = new Vector2 (newPos.x, oldPos.y);
			rt.sizeDelta = new Vector2 (length, 10);

			_text.anchoredPosition = new Vector2 (newPos.x,_text.anchoredPosition.y);
			_text.sizeDelta = new Vector2 (length,_text.sizeDelta.y);
		}

	}

	void UpdateTexts(){


	}
}

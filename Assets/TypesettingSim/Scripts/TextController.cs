using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	List<RectTransform> outlines,vectors;

	public GameObject preLine, prePoint;

	public RectTransform[] rts;

	public RectTransform text;

	// Use this for initialization
	void Start () {
		outlines = new List<RectTransform> ();
		vectors = new List<RectTransform> ();

		Vector2 pos = text.anchoredPosition;
		float width = text.rect.width;
		float height = text.rect.height;
		//Debug.Log (text.r);
		addLine ( pos + new Vector2 (0, height/2f), false, width);
		addLine ( pos + new Vector2 (width/2, 0), true, height);
		addLine ( pos + new Vector2 (0, -height/2f), false, width);
		addLine ( pos + new Vector2 (-width/2, 0), true, height);

		bool isVertical = false;
		for (int i =0;i< 4;i++) {
			int before = i - 1 < 0 ? 3 : i - 1;
			int after = i + 1 < 4 ? i + 1 : 0;
			outlines[i].GetComponent<DragAndExpand> ().initLine (outlines [before], outlines [after],isVertical,text);
			isVertical = !isVertical;

		}

		addPoint (pos + new Vector2 (width / 2, height / 2));
		addPoint (pos + new Vector2 (-width / 2, height / 2));
		addPoint (pos + new Vector2 (width / 2, -height / 2));
		addPoint (pos + new Vector2 (-width / 2, -height / 2));

	}

	void addLine(Vector2 pos, bool isVertical,float length){
		RectTransform newLine = Instantiate (preLine).GetComponent<RectTransform>();

		newLine.SetParent (transform);
		newLine.anchoredPosition = pos;
		newLine.localScale = Vector3.one;

		if (isVertical) {
			
			newLine.sizeDelta = new Vector2 (10, length);

		} else {
			newLine.sizeDelta = new Vector2 (length, 10);
		}

		outlines.Add (newLine);
	}

	void addPoint(Vector2 pos){
		RectTransform newpoint = Instantiate (prePoint).GetComponent<RectTransform>();

		newpoint.SetParent (transform);
		newpoint.anchoredPosition = pos;
		newpoint.localScale = Vector3.one;
		vectors.Add(newpoint);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

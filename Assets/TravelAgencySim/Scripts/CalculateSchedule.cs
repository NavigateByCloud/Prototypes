using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateSchedule : MonoBehaviour {
	public Transform day1,day2,day3;
	//ColorSchedule[] day1cs,day2cs,day3cs;
	ColorSchedule[][] dayCss;
	// Use this for initialization
	int numday = 1;
	int index = 0;
	public Text txtprogress;
	public Image hungerBar, ExcitBar, TiredBar;
	List<string> visitedPlaces;

	void Start () {
		Debug.Log ("in");
		hungerBar.transform.localScale = ExcitBar.transform.localScale = TiredBar.transform.localScale = new Vector3 (0.5f, 1, 1);
		hungerBar.color = ExcitBar.color = TiredBar.color = Color.green;
		dayCss = new ColorSchedule[3][];
		//day1cs = new ColorSchedule[];
		dayCss[0] = day1.GetComponentsInChildren<ColorSchedule> ();
		dayCss[1] = day2.GetComponentsInChildren<ColorSchedule> ();
		dayCss[2] = day3.GetComponentsInChildren<ColorSchedule> ();
		visitedPlaces = new List<string> ();
		visitedPlaces.Add ("Boryeong");
		Debug.Log (visitedPlaces.Capacity);
		//index = 11;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void aftermath(){
		if (GetComponentInChildren<Text> ().text.Equals ("Reschedule")) {
			GetComponent<Button> ().interactable = false;
			resetSchedult ();

		} else {
			GetComponent<Button> ().interactable = false;
			StartCoroutine ("followschedule");

		}
	}

	void resetSchedult(){
		for(int i = 0; i < dayCss.Length;i++){

			for (int j = 0; j < dayCss [i].Length; j++) {
				dayCss [i] [j].reset ();
			}
		}
		GetComponentInChildren<Text> ().text = "Submit";
	}

	IEnumerator followschedule(){
		int startTime = (numday == 1) ? 11 : 6;
		startTime += index;
		string start_time = (startTime > 12) ? (startTime - 12).ToString () + "pm" : startTime.ToString ()+"am";


		ColorSchedule cc = dayCss [numday - 1] [index];
		string end = cc.description.text;

		float x;
		if (cc.activity == Activity.travel) {
			MouseOverShowInfo info = GameObject.Find ("Btn" + cc.cityName).GetComponent<MouseOverShowInfo> ();
			AttractionType at = info.type;
			Vector2Int openTime = info.openTime;

			if (openTime.x < startTime && openTime.y + 12 > startTime) {
				txtprogress.text = "Day" + numday.ToString () + "\n " + start_time + ":" + "Sally " + end;
				x = ExcitBar.transform.localScale.x;
				if (!visitedPlaces.Contains (cc.cityName)) {
					//cal the commute
					visitedPlaces.Add(cc.cityName);
					if (at == AttractionType.Natural_Beauty) {
						x += 0.1f;
					} else {
						x += 0.05f;
					}
				}

				//Debug.Log (visitedPlaces. - 1);
				if (visitedPlaces.Capacity > 0 && visitedPlaces [visitedPlaces.Count - 1] != cc.cityName) {
					string lastcityname = visitedPlaces [visitedPlaces.Capacity - 1];
					Transform lastCity = GameObject.Find ("Btn" + lastcityname).transform;
					Transform thisCity = GameObject.Find ("Btn" + cc.cityName).transform;
					float dis = Vector3.Distance (lastCity.position, thisCity.position);
					Debug.Log ("distance:"+dis);
				}

				ExcitBar.transform.localScale = new Vector3 (x + 0.01f, 1, 1);
			} else {
				txtprogress.text = "Day" + numday.ToString () + "\n " + start_time + ":" + "Sally " + end+", but it's closed.";
				x = ExcitBar.transform.localScale.x;
				if (x - 0.2f < 0) {
					x = 0.2f;
				}
				ExcitBar.transform.localScale = new Vector3 (x - 0.2f, 1, 1);
			}

			x = hungerBar.transform.localScale.x;
			if (x + 0.1f > 1) {
				txtprogress.text = "Day" + numday.ToString () + "\n " + start_time + ":" + "Sally " + end+", but she is too hungry to go on. Failed.";
				//return;
				yield return new WaitForSeconds (1.0f);
				//restart
				GetComponentInChildren<Text>().text = "Reschedule";
				GetComponent<Button> ().interactable = true;
				Debug.Log("End");
				x = 0.9f;
			}

			hungerBar.transform.localScale = new Vector3 (x + 0.1f, 1, 1);


			x = TiredBar.transform.localScale.x;
			if (x + 0.1f > 1) {
				txtprogress.text = "Day" + numday.ToString () + "\n " + start_time + ":" + "Sally " + end+", but she is too tired to go on. Failed.";
				//return;
				yield return new WaitForSeconds (1.0f);
				//restart
				GetComponentInChildren<Text>().text = "Reschedule";
				GetComponent<Button> ().interactable = true;
				Debug.Log("End");
				x = 0.9f;
			}
			TiredBar.transform.localScale = new Vector3 (x + 0.1f, 1, 1);


		} else if (cc.activity == Activity.haveMeal) {
			x = hungerBar.transform.localScale.x;
			if(x - 0.5f < 0){
				x = 0;
			}else{
				x-=0.5f;
			}
			hungerBar.transform.localScale = new Vector3 (x, 1, 1);
		} else if (cc.activity == Activity.accomodate) {
			x = TiredBar.transform.localScale.x;
			if(x - 0.5f < 0){
				x = 0;
			}else{
				x-=0.5f;
			}
			TiredBar.transform.localScale = new Vector3 (x, 1, 1);
		}

		Debug.Log (txtprogress.text);

		index++;
		if (index >= dayCss[numday - 1].Length) {
			index = 0;
			numday++;
			if (numday > 3) {
				//return;
				yield return new WaitForSeconds (1.0f);
				//restart
				GetComponentInChildren<Text>().text = "Reschedule";
				GetComponent<Button> ().interactable = true;
				Debug.Log("End");
			} else {
				yield return new WaitForSeconds (1.0f);
				StartCoroutine ("followschedule");
			}

		}

		yield return new WaitForSeconds (1.0f);
		StartCoroutine ("followschedule");

	}
}

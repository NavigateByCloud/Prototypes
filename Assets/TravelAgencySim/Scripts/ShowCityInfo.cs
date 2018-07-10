using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Activity{
	travel,
	accomodate,
	haveMeal
}

public class ShowCityInfo : MonoBehaviour {
	public Text txtdescription, txtname, txtcost, txttime;
	public GameObject[] stars;
	public GameObject buttons;

	public static Color color = Color.white;
	public static Activity activity;
	// Use this for initialization
	void Start () {
		hide ();

		Button btnAdd = buttons.transform.Find ("Add").GetComponent<Button>();
		Button btnAccomodate = buttons.transform.Find ("accomodate").GetComponent<Button>();
		Button btnDinner = buttons.transform.Find ("dinner").GetComponent<Button>();
		Button btnCancel = buttons.transform.Find ("Cancel").GetComponent<Button>();
		btnAdd.onClick.AddListener (OnClickAddSite);
		btnAccomodate.onClick.AddListener (OnClickAccomodate);
		btnDinner.onClick.AddListener (OnClickDinner);
		btnCancel.onClick.AddListener (CancelSelect);
		buttons.SetActive (false);
	}

	 void OnClickAddSite(){
		activity = Activity.travel;
		color = buttons.transform.Find ("Add").GetComponent<Image> ().color;
		Debug.Log ("OnClickAddSite");
	}

	 void OnClickDinner(){
		activity = Activity.haveMeal;
		color = buttons.transform.Find ("dinner").GetComponent<Image> ().color;
		Debug.Log ("OnClickAddDinner");
	}

	 void OnClickAccomodate(){
		activity = Activity.accomodate;
		color = buttons.transform.Find ("accomodate").GetComponent<Image> ().color;
		Debug.Log ("OnClickAddHotel");
	}

	public void CancelSelect(){
		MouseOverShowInfo.isSelecting = false;
		MouseOverShowInfo.selectedName = "";
		buttons.SetActive (false);
		color = Color.white;
		hide();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void showButtons(){
		buttons.SetActive (true);

	}

	public void hideButtons(){
		buttons.SetActive (false);
	}

	public void show(string name, string description, int cost, Vector2Int openTime, Vector2Int costTime, int numStar){
		txtname.text = name;
		txtdescription.text = description;
		string costIcon = "";
		for (int i = 0; i < cost; i++) {
			costIcon += "$";
		}
		txtcost.text = costIcon;

		txttime.text = openTime.x + "am-" + openTime.y + "pm. cost " + costTime.x + "-" + costTime.y+"h"; 
		for (int i = 0; i < 5; i++) {
			if (i < numStar) {
				stars [i].SetActive (true);
			} else {
				stars [i].SetActive (false);
			}
		}

	}

	public void hide(){
		txtcost.text = txtname.text = txtdescription.text = txttime.text = "";
		for (int i = 0; i < 5; i++) {
			stars [i].SetActive (false);
		}
	}
}

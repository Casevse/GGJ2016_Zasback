using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {


	public Sprite checkBox;
	public Sprite checkBoxSelected;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buttonPlay(){
		Application.LoadLevel("Game");
	}

	public void buttonHistory(){
		Application.LoadLevel("History");
	}

	public void buttonControls(){
		Application.LoadLevel("Controls");
	}

	public void buttonOptions(){
		Application.LoadLevel("Options");
	}

	public void buttonExit(){
		Application.Quit();
	}

	public void buttonEffects(){
		int effectsQuit = PlayerPrefs.GetInt ("Effects");
		Image image;
		if (effectsQuit != null) {
			image = GameObject.Find ("Effects").GetComponent<Image>();
			if (effectsQuit == 0) {
				PlayerPrefs.SetInt ("Effects", 1);
				image.sprite = checkBoxSelected;
			} else {
				PlayerPrefs.SetInt ("Effects", 0);
				image.sprite = checkBox;
			}
		} else {
			PlayerPrefs.SetInt ("Effects", 1);
			image.sprite = checkBoxSelected;
		}


	}

	public void buttonMusic(){
		int musicQuit = PlayerPrefs.GetInt ("Music");
		Image image;
		if (musicQuit != null) {
			image = GameObject.Find ("Music").GetComponent<Image>();
			if (musicQuit == 0) {
				PlayerPrefs.SetInt ("Music", 1);
				image.sprite = checkBoxSelected;
			} else {
				PlayerPrefs.SetInt ("Music", 0);
				image.sprite = checkBox;
			}
		} else {
			PlayerPrefs.SetInt ("Music", 1);
			image.sprite = checkBoxSelected;
		}

	}
}

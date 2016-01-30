using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

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
}

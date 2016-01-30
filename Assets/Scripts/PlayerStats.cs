using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int fat;
	public int maxFat;
	public float percentBarWidth;
	public float percentBarHeight;

	public int padding_Top_Bar;
	public int padding_Left_Bar;

	private int modifyFat;


	public int progress;
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;

	private float screenX;
	private float screenY;
	private float widthBar;
	private float heightBar;
	private float fatBar;


	// Use this for initialization
	void Start () {
		modifyFat = 0;
		float percent = percentBarWidth / 100;
		widthBar = Screen.width*percent;
		percent = percentBarHeight/100;
		heightBar = Screen.height*percent;
	}
	
	// Update is called once per frame
	void Update () {
		if (IsDead ()) {
			Debug.Log ("Has muerto");
		} else {

			screenX = Screen.width / 5;
			screenY = Screen.height / 5;

			fatBar = (widthBar * fat) / maxFat;

			if (modifyFat < 0) {
				fat -=progress;
				if (fat <= 0)
					modifyFat = 0;
				else
					modifyFat+=progress;
			}else if (modifyFat > 0) {
				fat+=progress;
				if (fat >= maxFat)
					modifyFat = 0;
				else
					modifyFat-=progress;
			} else
				modifyFat = 0;

		}
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect((int)(screenX*0.1f)+padding_Left_Bar, (int)(screenY*0.1f)+padding_Top_Bar, widthBar, heightBar), progressBarEmpty);
		GUI.DrawTexture (new Rect((int)(screenX*0.1f)+padding_Left_Bar, (int)(screenY*0.1f)+padding_Top_Bar, fatBar, heightBar), progressBarFull);
	}

	public void AddFat(int value){
		modifyFat += value;
	}

	public void RemoveFat(int value){
		modifyFat -= value;
	}

	public bool IsDead(){
		if (fat <= 0)
			return true;
		else
			return false;

	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {


	public Sprite musicOn;
    public Sprite musicOff;
    public Sprite effectsOn;
    public Sprite effectsOff;
    // Use this for initialization
    void Start () {
        Image image = GameObject.Find("Effects").GetComponent<Image>();
        if (PlayerPrefs.GetInt("Effects") == 0 || PlayerPrefs.GetInt("Effects") == 2) {     
            PlayerPrefs.SetInt("Effects", 2);
            image.sprite = effectsOn;
        }else {
            PlayerPrefs.SetInt("Effects", 1);
            image.sprite = effectsOff;
        }

        image = GameObject.Find("Music").GetComponent<Image>();

        if (PlayerPrefs.GetInt("Music") == 0 || PlayerPrefs.GetInt("Music") == 2) {
            PlayerPrefs.SetInt("Music", 2);
            SoundSingleton.Singleton.PlayMusicIntro();
            image.sprite = musicOn;
        }
        else {
            PlayerPrefs.SetInt("Music", 1);
            image.sprite = musicOff;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buttonPlay(){
        GameManager.round = 0;
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel("Game");
	}

	public void buttonControls(){
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel("Controls");
	}

	public void buttonOptions(){
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel("Options");
	}

	public void buttonExit(){
        Application.Quit();
	}

	public void buttonReturn(){
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel("Menu");
	}

    public void buttonAbout() {
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel("AboutUs");
    }

	public void buttonEffects(){
        Image image= GameObject.Find ("Effects").GetComponent<Image>();
			if (PlayerPrefs.GetInt("Effects") == 2) {
                PlayerPrefs.SetInt ("Effects", 1);
                image.sprite = effectsOff;
			} else {
				PlayerPrefs.SetInt ("Effects", 2);
                SoundSingleton.Singleton.PlayButton();
                image.sprite = effectsOn;
			}

	}

	public void buttonMusic(){
        Image image = GameObject.Find("Music").GetComponent<Image>();

			if (PlayerPrefs.GetInt("Music") == 2) {
				PlayerPrefs.SetInt ("Music", 1);
                SoundSingleton.Singleton.StopMusicGame();
                SoundSingleton.Singleton.PlayButton();
                image.sprite = musicOff;
			} else {
				PlayerPrefs.SetInt ("Music", 2);
                SoundSingleton.Singleton.PlayMusicIntro();
                SoundSingleton.Singleton.PlayButton();
                image.sprite = musicOn;
			}


	}
}

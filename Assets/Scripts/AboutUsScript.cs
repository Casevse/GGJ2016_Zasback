using UnityEngine;
using System.Collections;

public class AboutUsScript : MonoBehaviour {

	public void buttonReturn(){
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel("Menu");
	}
}

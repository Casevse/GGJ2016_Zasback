using UnityEngine;
using System.Collections;

public class SoundSingleton : MonoBehaviour {

	private static SoundSingleton   Instance;

	private AudioSource AudioManager;

	public AudioClip powerUp;

	public static SoundSingleton Singleton
	{
		get
		{
			if(Instance == null)
			{
				Instance = GameObject.FindObjectOfType<SoundSingleton>();
				DontDestroyOnLoad(Instance.gameObject);
			}

			return Instance;
		}
	}

	void Awake(){
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		AudioManager = GetComponent<AudioSource> ();
	}

	public void PlayPowerUp(){
		AudioManager.PlayOneShot (powerUp);
	}
		
}
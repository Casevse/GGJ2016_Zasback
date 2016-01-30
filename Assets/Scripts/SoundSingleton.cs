using UnityEngine;
using System.Collections;

public class SoundSingleton : MonoBehaviour {

	private static SoundSingleton   Instance;

	private AudioSource AudioManager;

	public AudioClip powerUp;
	public AudioClip hitPlayer;
	public AudioClip enemySmash;
	public AudioClip attackPlayer;
	public AudioClip clickMenu;
	public AudioClip shotCob;

	public bool muteMusic;

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
		if(PlayerPrefs.GetInt("Effects") == 1)
			AudioManager.PlayOneShot (powerUp);
	}

	public void PlayHitPlayer(){
		if(PlayerPrefs.GetInt("Effects") == 1)
		AudioManager.PlayOneShot (hitPlayer);
	}

	public void PlaySmashEnemy(){
		if(PlayerPrefs.GetInt("Effects") == 1)
		AudioManager.PlayOneShot (enemySmash);
	}

	public void PlayAttackPlayer(){
		if(PlayerPrefs.GetInt("Effects") == 1)
		AudioManager.PlayOneShot (attackPlayer);
	}
		
	public void PlayClickMenu(){
		if(PlayerPrefs.GetInt("Effects") == 1)
		AudioManager.PlayOneShot (clickMenu);
	}

	public void PlayShotCob(){
		if(PlayerPrefs.GetInt("Effects") == 1)
		AudioManager.PlayOneShot (shotCob);
	}

}
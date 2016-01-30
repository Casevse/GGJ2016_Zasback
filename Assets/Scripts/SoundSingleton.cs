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
	public AudioClip shotCub;

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

	public void PlayHitPlayer(){
		AudioManager.PlayOneShot (hitPlayer);
	}

	public void PlaySmashEnemy(){
		AudioManager.PlayOneShot (enemySmash);
	}

	public void PlayAttackPlayer(){
		AudioManager.PlayOneShot (attackPlayer);
	}
		
	public void PlayClickMenu(){
		AudioManager.PlayOneShot (clickMenu);
	}
	public void PlayShotCub(){
		AudioManager.PlayOneShot (shotCub);
	}

}
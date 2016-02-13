using UnityEngine;
using System.Collections;

public class SoundSingleton : MonoBehaviour {

    private static SoundSingleton Instance;

    private AudioSource AudioManager;

    public AudioClip powerUp;
    public AudioClip hitPlayer;
    public AudioClip enemySmash;
    public AudioClip attackPlayer;
    public AudioClip key;
    public AudioClip button;

    public AudioClip musicGame;
    public AudioClip musicIntro;

    public bool muteMusic;

    public static SoundSingleton Singleton {
        get {
            if (Instance == null) {
                Instance = GameObject.FindObjectOfType<SoundSingleton>();
                DontDestroyOnLoad(Instance.gameObject);
            }

            return Instance;
        }
    }

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start() {
        AudioManager = GetComponent<AudioSource>();
    }

    public void PlayPowerUp() {
        if (PlayerPrefs.GetInt("Effects") == 2)
            AudioManager.PlayOneShot(powerUp);
    }

    public void PlayHitPlayer() {
        if (PlayerPrefs.GetInt("Effects") == 2)
            AudioManager.PlayOneShot(hitPlayer);
    }

    public void PlaySmashEnemy() {
        if (PlayerPrefs.GetInt("Effects") == 2)
            AudioManager.PlayOneShot(enemySmash);
    }

    public void PlayAttackPlayer() {
        if (PlayerPrefs.GetInt("Effects") == 2)
            AudioManager.PlayOneShot(attackPlayer);
    }

    public void PlayKey() {
        if (PlayerPrefs.GetInt("Effects") == 2)
            AudioManager.PlayOneShot(key);
    }

    public void PlayButton() {
        if (PlayerPrefs.GetInt("Effects") == 2)
            AudioManager.PlayOneShot(button);
    }

    public void PlayMusicGame() {
        if (PlayerPrefs.GetInt("Music") == 2) {
            AudioManager.clip = musicGame;
            AudioManager.Play();
            AudioManager.loop = true;
        }
    }

    public void PlayMusicIntro() {
        if (PlayerPrefs.GetInt("Music") == 2) {
            if(AudioManager.clip != musicIntro) {
                AudioManager.clip = musicIntro;
                AudioManager.Play();
                AudioManager.loop = true;
            }
        }
    }

    public void StopMusicGame() {
        AudioManager.Stop();
    }
}
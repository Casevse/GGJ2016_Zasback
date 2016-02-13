using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerStats playerStats;
    public Button btnReplay;
    public Button btnExit;
    public Button btnNext;
    public Text text;

    public static int enemiesAlive = 0;
    public static bool endGame = false;
    public static bool hasWin = false;
    public static int round = 0;

    private bool waitingNextRound;

    // Enemies generator.
    public Enemy[] enemies;
    private float nextRespawn;
    public Transform[] respawnPoints;

    // Power ups generation.
    public PowerUp[] powerUps;
    private float nextPowerUp;
    public Transform[] powerUpPoints;

    private void Awake() {
        waitingNextRound = false;
        endGame = false;
        hasWin = false;
        btnReplay.gameObject.SetActive(false);
        btnExit.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        btnNext.gameObject.SetActive(false);
        enemiesAlive = 0;
        SoundSingleton.Singleton.PlayMusicGame();
        nextPowerUp = Time.time + Random.Range(24.0f, 36.0f);
    }
	
	private void Update() {
	    if (playerStats.IsDead() && !endGame) {
            endGame = true;
            btnReplay.gameObject.SetActive(true);
            btnExit.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }

        if (hasWin && !waitingNextRound) {
            round++;
            waitingNextRound = true;
            btnNext.gameObject.SetActive(true);
            endGame = true;
        }

        if (endGame) {
            SoundSingleton.Singleton.StopMusicGame();
            return;
        }

        if (Time.time > nextRespawn && enemiesAlive < 6) {
            RespawnEnemy();
            nextRespawn = Time.time + Random.Range(4.0f, 6.0f);
            enemiesAlive++;
        }

        if (Time.time > nextPowerUp) {
            RespawnPowerUp();
            nextPowerUp = Time.time + Random.Range(24.0f, 36.0f);
        }
	}

    private void RespawnEnemy() {
        if (enemies.Length > 0) {
            int index = Random.Range(0, enemies.Length);
            Enemy enemy = enemies[index].GetComponent<Enemy>();


            Transform respawnPoint = null;
            if (respawnPoints.Length > 0) {
                respawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
            }

            Vector3 respawnPointPosition;
            if (respawnPoint != null) {
                respawnPointPosition = respawnPoint.position;
            } else {
                respawnPointPosition = new Vector3(0.0f, 7.0f, 0.0f);
            }

            GameObject newEnemy = Instantiate(enemy.gameObject, respawnPointPosition, Quaternion.identity) as GameObject;
        }
    }

    private void RespawnPowerUp() {
        if (powerUps.Length > 0) {
            int index = Random.Range(0, powerUps.Length);
            PowerUp powerUp = powerUps[index];

            Transform powerUpPoint = null;
            if (powerUpPoints.Length > 0) {
                powerUpPoint = powerUpPoints[Random.Range(0, powerUpPoints.Length)];
            }

            Vector3 powerUpPointPosition;
            if (powerUpPoint != null) {
                powerUpPointPosition = powerUpPoint.position;
            }
            else {
                powerUpPointPosition = new Vector3(-7.0f, 7.0f, 0.0f);
            }

            GameObject newPowerUp = Instantiate(powerUp.gameObject, powerUpPointPosition, Quaternion.identity) as GameObject;
            Rigidbody2D rigidbody = newPowerUp.GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                float direction = powerUpPointPosition.x > 0.0f ? -1.0f : 1.0f;
                rigidbody.AddForce(new Vector2(direction * Random.Range(4.0f, 6.0f), Random.Range(5.0f, 6.0f)), ForceMode2D.Impulse);
            }
        }
    }

    public void ReplayScene() {
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel (Application.loadedLevel);
    }

    public void ExitScene() {
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel ("Menu");
    }

    public void BeginNextRound() {
        SoundSingleton.Singleton.PlayButton();
        Application.LoadLevel(Application.loadedLevel);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerStats playerStats;
    public Button btnReplay;
    public Button btnExit;

    // Enemies generator.
    public Enemy[] enemies;
    private float nextRespawn;
    public Transform[] respawnPoints;

    private bool endGame = false;

    private void Awake() {
        endGame = false;
        btnReplay.gameObject.SetActive(false);
        btnExit.gameObject.SetActive(false);
    }
	
	private void Update() {
	    if (playerStats.IsDead() && !endGame) {
            endGame = true;
            btnReplay.gameObject.SetActive(true);
            btnExit.gameObject.SetActive(true);
        }

        if (Time.time > nextRespawn) {
            RespawnEnemy();
            nextRespawn = Time.time + Random.Range(4.0f, 6.0f);
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


            if (enemy is Cob) {
                Debug.Log("Cob " + Time.time);
                
            } else if (enemy is Tomato) {
                Debug.Log("Tomato " + Time.time);
            } else if (enemy is EggPlant) {
                Debug.Log("EggPlant " + Time.time);
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

    public void ReplayScene() {
		Application.LoadLevel (Application.loadedLevel);
    }

    public void ExitScene() {
		Application.LoadLevel ("Menu");
    }
}

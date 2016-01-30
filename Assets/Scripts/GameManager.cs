using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public PlayerStats playerStats;
    public Button btnReplay;
    public Button btnExit;

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
	}

    public void ReplayScene() {
		Application.LoadLevel (Application.loadedLevel);
    }

    public void ExitScene() {
		Application.LoadLevel ("Menu");
    }
}

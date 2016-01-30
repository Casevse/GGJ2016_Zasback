using UnityEngine;
using System.Collections;

public class LogicKey : MonoBehaviour {

	public float rate;

	private float movDown;


	// Use this for initialization
	void Start () {
		movDown = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		if (movDown > 0.0f) {
			this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y - movDown * Time.deltaTime);
			movDown -= 0.1f;
		} else
			movDown = 0.0f;

	}

	public void DownKey(float speed){
		rate = rate + (speed / 10);
		movDown = rate;

	}

	void OnTriggerEnter2D(Collider2D  colision){
		if (colision.gameObject.tag == "Player") {
			Debug.Log ("Fin deljuego");
		}
	}
}

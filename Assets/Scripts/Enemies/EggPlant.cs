using UnityEngine;
using System.Collections;

public class EggPlant : Enemy {

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		initEnemy (1, 1.0f, 1, 0f);
		rigidBody.gravityScale += fallSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.endGame) return;
        if (falling && onFloor) {
			rigidBody.gravityScale -= fallSpeed;
			falling = false;
		} else if (!falling && onFloor) {
			if(side){
				rigidBody.velocity = new Vector2(movSpeed, 0.0f);
			}
			else{
				rigidBody.velocity = new Vector2(-movSpeed, 0.0f);
			}
		} 
	}
}

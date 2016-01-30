using UnityEngine;
using System.Collections;

public class Cob : Enemy {

	private Rigidbody2D rigidBody;
	private float timeShoot;
	private float delayShoot;
	public Bullet bullet;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		initEnemy (1, 1.5f, 1, 1f);
		rigidBody.gravityScale += fallSpeed;
		timeShoot = Time.time - 1.0f;
		delayShoot = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
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

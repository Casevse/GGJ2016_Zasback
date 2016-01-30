using UnityEngine;
using System.Collections;

public class Cob : Enemy {

	private Rigidbody2D rigidBody;
	private float timeShoot;
	private float delayShoot;
	public GameObject bullet;
	public int bulletSpeed;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		initEnemy (1, 1.5f, 1, 1f);
		rigidBody.gravityScale += fallSpeed;
		timeShoot = Time.time;
		delayShoot = 2.0f;
		bulletSpeed = 4;
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

			if((Time.time - timeShoot) > delayShoot){
				shoot(side);
				timeShoot = Time.time;
			}
		}
	}

	void shoot(bool s){
		GameObject clone;
		if (s) {
			Vector3 aux = new Vector3(transform.position.x + 0.5f, transform.position.y, 0.0f);
			clone = (GameObject) Instantiate (bullet, aux, transform.rotation);
			clone.GetComponent<Rigidbody2D>().velocity = new Vector2(4.0f, 0.0f);
		} else {
			Vector3 aux = new Vector3(transform.position.x - 0.5f, transform.position.y, 0.0f);
			clone = (GameObject) Instantiate (bullet, aux, transform.rotation);
			clone.GetComponent<Rigidbody2D>().velocity = new Vector2(-4.0f, 0.0f);
		}
	}
}

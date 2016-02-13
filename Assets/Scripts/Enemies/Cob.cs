using UnityEngine;
using System.Collections;

public class Cob : Enemy {

	private Rigidbody2D rigidBody;
	private float timeShoot;
	private float delayShoot;
	public GameObject bullet;
	public float bulletSpeed;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		initEnemy ();
		rigidBody.gravityScale += fallSpeed;
		timeShoot = Time.time;

        //Tiempo de disparo aleatorio
        float randDelay = Random.Range(3.0f, 4.0f);
		delayShoot = randDelay - (0.1f * numberRound);
        if(delayShoot < 2.0f) {
            delayShoot = 2.0f;
        }

        //Velocidad de la bala
        float randBullet = Random.Range(6.0f, 8.0f);
		bulletSpeed = randBullet;
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
			clone = Instantiate (bullet, aux, transform.rotation) as GameObject;
			clone.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0.0f);
		} else {
			Vector3 aux = new Vector3(transform.position.x - 0.5f, transform.position.y, 0.0f);
			clone = Instantiate (bullet, aux, transform.rotation) as GameObject;
			clone.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0.0f);
		}
	}
}

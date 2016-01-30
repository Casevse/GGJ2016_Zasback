using UnityEngine;
using System.Collections;

public class Tomato : Enemy {

	private Rigidbody2D rigidBody;
	private float height;
	private float timeJump;
	private float delayJump;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		initEnemy (1, 200.0f, 1, 0.5f);
		initParamsTomato ();
	}

	private void initParamsTomato(){
		rigidBody.gravityScale += fallSpeed;
		height = 600.0f;
		timeJump = Time.time - 1.0f;
		delayJump = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (falling && onFloor) {
			rigidBody.gravityScale -= fallSpeed;
			falling = false;
		} else if (!falling && onFloor && (Time.time - timeJump) > delayJump) {
			if(side){
				rigidBody.AddForce(new Vector2(movSpeed, height));
			}
			else{
				rigidBody.AddForce(new Vector2(-movSpeed, height));
			}
			onFloor = false;
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Floor" && !onFloor) {
			timeJump = Time.time;
			onFloor = true;
		} else if (col.gameObject.tag == "Wall") {
			side = !side;
		} else if(col.gameObject.tag == "Player"){
			PlayerStats stats = col.gameObject.GetComponent<PlayerStats>();
			if(stats != null){
				stats.RemoveFat(damage);
				timeDamage = Time.time;
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			if((Time.time - timeDamage) > delayDamage){
				PlayerStats stats = coll.gameObject.GetComponent<PlayerStats>();
				if(stats != null){
					stats.RemoveFat(damage);
					timeDamage = Time.time;
				}
			}
		}
	}
}

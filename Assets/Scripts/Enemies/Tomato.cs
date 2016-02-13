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
		initEnemy ();
		initParamsTomato ();
	}

	private void initParamsTomato(){
		rigidBody.gravityScale += fallSpeed;

        //Velocidad de movimiento aleatoria
        float randMov = Random.Range(200.0f, 300.0f);
        movSpeed = randMov + (10.0f * numberRound);

        //Random de la altura de salto
        float randAltura = Random.Range(500.0f, 700.0f);
		height = randAltura + (10.0f * numberRound);

        if (height > 900.0f) {
            height = 900.0f;
        }

        //Random del retraso entre salto y salto
        float randDelay = Random.Range(1.0f, 2.5f);
		timeJump = Time.time - 1.0f;
        delayJump = randDelay;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.endGame) return;
        if (falling && onFloor) {
			rigidBody.gravityScale -= fallSpeed;
			falling = false;
		} else if (!falling && onFloor && (Time.time - timeJump) > delayJump) {
			if (side) {
				rigidBody.AddForce(new Vector2(movSpeed, height));
			}
			else{
				rigidBody.AddForce(new Vector2(-movSpeed, height));
			}
			onFloor = false;
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
        if (GameManager.endGame) {
            return;
        }

        if (col.gameObject.tag == "Floor" && !onFloor) {
			timeJump = Time.time;
			onFloor = true;
		} else if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Key") {
			side = !side;
		} else if(col.gameObject.tag == "Player") {
			PlayerStats stats = col.gameObject.GetComponent<PlayerStats>();
			if(stats != null){
				stats.RemoveFat(damage);
				timeDamage = Time.time;
			}
		} else if (col.gameObject.tag == "Enemy") {
            if (col.contacts[0].normal.y != -1.0f) {
                side = !side;
            }
        }
    }

	void OnCollisionStay2D(Collision2D coll) {
        if (GameManager.endGame) {
            return;
        }

        if (coll.gameObject.tag == "Player") {
			if((Time.time - timeDamage) > delayDamage){
				PlayerStats stats = coll.gameObject.GetComponent<PlayerStats>();
				if(stats != null){
					stats.RemoveFat(damage);
					timeDamage = Time.time;
				}
			}
        }
        else if (coll.gameObject.tag == "Enemy") {
            if (coll.contacts[0].normal.y == -1.0f) {
                Rigidbody2D rigidbody2D = coll.gameObject.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null) {
                    if (side) {
                        rigidbody2D.AddForce(new Vector2(-100.0f, 100.0f));
                    }
                    else {
                        rigidbody2D.AddForce(new Vector2(100.0f, 100.0f));
                    }
                }
            }
            else {
                side = !side;
            }
        }
        else if (coll.gameObject.tag == "Wall") {
            if ((Time.time - timeStay) > delayStay) {
                side = !side;
                timeStay = Time.time;
            }
        }
        else if (coll.gameObject.tag == "Key") {
            if (coll.contacts[0].normal.y == 1.0f) {
                if (transform.position.x < coll.gameObject.transform.position.x) {
                    rigidbody.AddForce(new Vector2(-100.0f, 100.0f));
                }
                else {
                    rigidbody.AddForce(new Vector2(100.0f, 100.0f));
                }
                onFloor = true;
            }
        }
    }
}

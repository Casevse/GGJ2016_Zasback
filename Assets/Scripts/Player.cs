using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public bool movingRight = true;
    public float normalSpeed = 6.0f;
    public float jumpSpeed = 6.0f;
    

    private bool jumping = false;
    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private bool action;
    private float speed;
    
    public enum AttackPhase {
       NONE, BEGIN, MIDDLE, END
    }
    public AttackPhase attackPhase;
    private float nextAttackPhase;

    public enum FlipPhase {
        NONE, TOUCHING, FLIPPLING
    }
    public FlipPhase flipPhase;
    private float nextFlipPhase;

	private void Start() {
        jumping = false;
        rigidbody = this.GetComponent<Rigidbody2D>();
        direction = new Vector2();
        attackPhase = AttackPhase.NONE;
        nextAttackPhase = 0.0f;

        flipPhase = FlipPhase.NONE;
        nextFlipPhase = 0.0f;
	}

	private void Update() {
        
        if (movingRight) {
            direction.x = 1.0f;
        } else {
            direction.x = -1.0f;
        }

        if (attackPhase == AttackPhase.END && Time.time > nextAttackPhase) {
            speed += normalSpeed * Time.deltaTime;
            if (speed > normalSpeed) {
                speed = normalSpeed;
                attackPhase = AttackPhase.NONE;
            }
        }

        if (flipPhase == FlipPhase.FLIPPLING && Time.time > nextFlipPhase) {
            flipPhase = FlipPhase.NONE;
        }

        if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)) {
            action = true;
        } else {
            action = false;
        }

        if ((int)attackPhase > 0) {
            if (Time.time > nextAttackPhase) {
                if (attackPhase == AttackPhase.BEGIN) {
                    rigidbody.velocity = new Vector2(0.0f, 0.0f);
                    rigidbody.AddForce(new Vector2(0.0f, -jumpSpeed * 0.8f), ForceMode2D.Impulse);
                    attackPhase = AttackPhase.MIDDLE;
                }
            }
        } else {
            if (action) {
                if (flipPhase == FlipPhase.TOUCHING) {
                    flipPhase = FlipPhase.FLIPPLING;
                    movingRight = !movingRight;
                    nextFlipPhase = Time.time + 1.0f;
                }
                else if (!jumping) {
                    rigidbody.AddForce(new Vector2(0.0f, jumpSpeed), ForceMode2D.Impulse);
                    jumping = true;
                }
                else {
                    if (attackPhase == AttackPhase.NONE) {
                        attackPhase = AttackPhase.BEGIN;
                        nextAttackPhase = Time.time + 0.05f;
                    }
                }
            }
        }
	}

    private void FixedUpdate() {
        if (attackPhase != AttackPhase.MIDDLE) {
            rigidbody.velocity = new Vector2(direction.x * speed, rigidbody.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.contacts[0].normal.y == 1.0f) {
            jumping = false;
            if (attackPhase == AttackPhase.MIDDLE) {
                nextAttackPhase = Time.time + 0.2f;
                speed = normalSpeed * 0.05f;

                // Hit the enemy.
                if (coll.gameObject.tag == "Enemy") {
                    // TODO: Hacer daño, esperar a que Aitor no toque enemigos.

                    Enemy enemy = coll.gameObject.GetComponent<Enemy>();
                    if (enemy != null) {
                        enemy.setLife(enemy.getLife() - 1);
                    }
                }
            }
            attackPhase = AttackPhase.END;
        }

        if (coll.gameObject.tag == "Wall") {
            if (flipPhase == FlipPhase.NONE) {
                flipPhase = FlipPhase.TOUCHING;
            }
        }
    }


}

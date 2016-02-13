using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public bool movingRight = false;
    public float normalSpeed = 6.0f;
    public float jumpSpeed = 6.0f;
    private Animator animator;
    

    private bool jumping = false;
    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private bool action;
    private float speed;
    private PlayerStats playerStats;
    
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

    private void Awake() {
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        movingRight = false;
    }

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
        if (GameManager.endGame) {
            return;
        }
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

        if (playerStats.IsDead() == false) {
            if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)) {
                action = true;
            }
            else {
                action = false;
            }
        }

        if ((int)attackPhase > 0) {
            if (Time.time > nextAttackPhase) {
                if (attackPhase == AttackPhase.BEGIN) {
					SoundSingleton.Singleton.PlayAttackPlayer ();
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

                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;

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

        if (animator != null) {
            animator.SetBool("movingRight", movingRight);
            animator.SetBool("jumping", jumping);
            animator.SetBool("attacking", (attackPhase == AttackPhase.MIDDLE));
        }
	}

    private void FixedUpdate() {
        if (attackPhase != AttackPhase.MIDDLE && playerStats.IsDead() == false) {
            rigidbody.velocity = new Vector2(direction.x * speed, rigidbody.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.contacts[0].normal.y == 1.0f) {
            jumping = false;
            if (attackPhase == AttackPhase.MIDDLE) {
                nextAttackPhase = Time.time + 0.1f;
                speed = normalSpeed * 0.5f;

                // Hit the enemy.
                if (coll.gameObject.tag == "Enemy") {
                    Enemy enemy = coll.gameObject.GetComponent<Enemy>();
                    if (enemy != null) {
						SoundSingleton.Singleton.PlaySmashEnemy ();
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

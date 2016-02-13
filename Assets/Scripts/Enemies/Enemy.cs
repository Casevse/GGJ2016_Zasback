using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int life;
	public float movSpeed;
	public int damage;
	public float fallSpeed;
	protected bool onFloor;
	protected bool side;
	protected bool falling;
	protected float timeDamage;
	protected float delayDamage;
    protected float timeStay;
    protected float delayStay;
    protected int numberRound;

    protected Rigidbody2D rigidbody;

    private void Awake() {
        // Halo doesn't work in 2D.
        /*Component halo = gameObject.GetComponent("Halo");
        if (halo != null) {
            halo.GetComponent<Renderer>().sortingLayerName = "Foreground";
            halo.GetComponent<Renderer>().sortingOrder = 0;
        }*/
    }

	// Use this for initialization
	protected void Start () {
       
	}

	protected void initEnemy(){
		life = 1; //Solo tienen un toque de vida
        numberRound = GameManager.round;

        //Velocidad de movimiento aleatoria
        float randMov = Random.Range(1.0f, 2.0f);
		movSpeed = randMov + (0.1f * numberRound);

        //Danyo de los enemigos
        int randDmg = Random.Range(1, 11);
		damage = randDmg + (1 * numberRound);
        Debug.Log(damage);

        //Velocidad de caida
        float randFall = Random.Range(0.0f, 1.0f);
		fallSpeed = randFall + (0.1f * numberRound);
		onFloor = false;

        //Selecciona el lado al que comienza a ir aleatoriamente
        int rand = Random.Range(1, 3); //Rand para el lado en el que comienza
        if (rand == 1) {
			side = true; //derecha
		} else {
			side = false; //izquierda
		}
		falling = true;
		timeDamage = Time.time;
		delayDamage = 1.0f;
        timeStay = Time.time;
        delayStay = 0.5f;

        rigidbody = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

	}

	public void setLife(int l){
		life = l;
        if (life <= 0) {
            // Kill the enemy.
            // TODO: Animación de muerte para cada enemigo.
            Destroy(this.gameObject);
            GameManager.enemiesAlive--;
        }
	}
	public int getLife(){
		return life;
	}

	public void setMovSpeed(float mS){
		movSpeed = mS;
	}
	public float getMovSpeed(){
		return movSpeed;
	}

	public void setDamage(int dmg){
		damage = dmg;
	}
	public int getDamage(){
		return damage;
	}

	public void setFallSpeed(float fS){
		fallSpeed = fS;
	}
	public float getFallSpeed(){
		return fallSpeed;
	}

	public void setOnFloor(bool onF){
		onFloor = onF;
	}
	public bool getOnFloor(){
		return onFloor;
	}

	public void setSide(bool s){
		side = s;
	}
	public bool getSide(){
		return side;
	}

	protected void OnCollisionEnter2D (Collision2D col)
	{
        if (GameManager.endGame) {
            return;
        }

        if (col.gameObject.tag == "Floor" && !onFloor) {
            onFloor = true;
        } else if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Key") {
            side = !side;
        } else if (col.gameObject.tag == "Player") {
            PlayerStats stats = col.gameObject.GetComponent<PlayerStats>();
            if (stats != null) {
                stats.RemoveFat(damage);
                timeDamage = Time.time;
            }
        }
        else if (col.gameObject.tag == "Enemy") {
            if (col.contacts[0].normal.y != -1.0f) {
                side = !side;
            }
        }
    }

	protected void OnCollisionStay2D(Collision2D coll) {
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
		} else if (coll.gameObject.tag == "Enemy") {
            if (coll.contacts[0].normal.y == -1.0f) {
                Rigidbody2D rigidbody2D = coll.gameObject.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null) {
                    if (side) {
                        rigidbody2D.AddForce(new Vector2(-100.0f, 100.0f));
                    } else {
                        rigidbody2D.AddForce(new Vector2(100.0f, 100.0f));
                    }
                }
            }
        } else if (coll.gameObject.tag == "Wall") {
            if((Time.time - timeStay) > delayStay) {
                side = !side;
                timeStay = Time.time;
            }
        } else if(coll.gameObject.tag == "Key") {
            if (coll.contacts[0].normal.y == 1.0f) {
                if (transform.position.x < coll.gameObject.transform.position.x) {
                    rigidbody.AddForce(new Vector2(-100.0f, 100.0f));
                }
                else {
                    rigidbody.AddForce(new Vector2(100.0f, 100.0f));
                }
            }
        }
    }
}

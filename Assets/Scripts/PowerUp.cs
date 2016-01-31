using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {


	public int fatMin, fatMax;
	public Sprite[] sprites;
	public Texture2D tex;

	private Renderer renderer;
	private int valueFat;
    private Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
		if(sprites.Length > 0){
			int sprite = Random.Range (0, sprites.Length);
			GetComponent<SpriteRenderer> ().sprite  = sprites [sprite];
			transform.localScale = new Vector2 (0.23f, 0.23f);
			float width = GetComponent<SpriteRenderer> ().sprite.textureRect.width;
			float height = GetComponent<SpriteRenderer> ().sprite.textureRect.height;
			GetComponent<BoxCollider2D> ().size = new Vector2 (width/100,height/100);
			if (fatMax == 0)
				fatMax = 20;
			valueFat = Random.Range (fatMin,fatMax);


		}
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.GetComponent<PlayerStats> ().AddFat (valueFat);
			SoundSingleton.Singleton.PlayPowerUp ();
			Destroy (this.gameObject);
		} 
	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.gameObject.tag == "Floor") {
			GetComponent<BoxCollider2D> ().isTrigger = true;
			GetComponent<Rigidbody2D> ().isKinematic = true;

		}else if (collision.gameObject.tag == "Player") {
			collision.gameObject.GetComponent<PlayerStats> ().AddFat (valueFat);
			SoundSingleton.Singleton.PlayPowerUp ();
			Destroy (this.gameObject);
		} 
	}

    protected void OnCollisionStay2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy") {
            if (coll.contacts[0].normal.y == -1.0f) {
                Rigidbody2D rigidbody2D = coll.gameObject.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null) {
                    if (transform.position.x > coll.gameObject.transform.position.x) {
                        rigidbody2D.AddForce(new Vector2(-100.0f, 100.0f));
                    }
                    else {
                        rigidbody2D.AddForce(new Vector2(100.0f, 100.0f));
                    }
                }
            } else if (coll.contacts[0].normal.y == 1.0f) {
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {


	public int fatMin, fatMax;
	public Sprite[] sprites;
	public Texture2D tex;

	private Renderer renderer;
	private int valueFat;

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
}

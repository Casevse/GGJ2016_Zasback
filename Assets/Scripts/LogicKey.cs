using UnityEngine;
using System.Collections;

public class LogicKey : MonoBehaviour {

	//public float rate;
	public float jumpForce = 4.0f;

	private float movDown;
	private Rigidbody2D rigidbody;

	public enum KeyPhase {
		NONE, JUMP, END
	}

	private KeyPhase keyPhase;
	private float force;

	void Start () {
		rigidbody = this.GetComponent<Rigidbody2D> ();
		keyPhase = KeyPhase.NONE;
	}

	void Update () {

		if (keyPhase != KeyPhase.END) {
			if (keyPhase == KeyPhase.JUMP) {
				rigidbody.velocity = new Vector2(0.0f, 0.0f);
				rigidbody.AddForce(new Vector2(-(jumpForce/10), +force * 0.9f), ForceMode2D.Impulse);
				keyPhase = KeyPhase.NONE;
			}
		}
	}

	public void DownKey(float speed){
		keyPhase = KeyPhase.JUMP;
		force = jumpForce+(speed/10);
	}

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Floor") {
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

	void OnTriggerEnter2D(Collider2D  colision){
		if (colision.gameObject.tag == "Player") {
            GameManager.hasWin = true;
            SoundSingleton.Singleton.PlayKey();
            Destroy(this.gameObject);
			keyPhase = KeyPhase.END;
		}
	}
		
}

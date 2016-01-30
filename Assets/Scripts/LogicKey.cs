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



	// Use this for initialization
	void Start () {
		//movDown = 0.0f;
		rigidbody = this.GetComponent<Rigidbody2D> ();
		keyPhase = KeyPhase.NONE;
	}

	// Update is called once per frame
	void Update () {

		if (keyPhase != KeyPhase.END) {
			if (keyPhase == KeyPhase.JUMP) {
				rigidbody.velocity = new Vector2(0.0f, 0.0f);
				rigidbody.AddForce(new Vector2(-(jumpForce/10), +force * 0.9f), ForceMode2D.Impulse);
				keyPhase = KeyPhase.NONE;
			}
		}


		/*	 

		if (movDown > 0.0f) {


			//this.GetComponent<Rigidbody2D> ().isKinematic = false;

			//this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y - movDown * Time.deltaTime);
			movDown -= 0.1f;
		} else {
			//this.GetComponent<Rigidbody2D> ().isKinematic = true;
			movDown = 0.0f;
		}*/

	}

	public void DownKey(float speed){
		//rate = rate + (speed / 10);
		keyPhase = KeyPhase.JUMP;
		force = jumpForce+(speed/10);
		//movDown = rate;

	}

	void OnTriggerEnter2D(Collider2D  colision){
		if (colision.gameObject.tag == "Player") {
			Debug.Log ("Fin deljuego");
			keyPhase = KeyPhase.END;
		}
	}
		
}

using UnityEngine;
using System.Collections;

public class LogicKey : MonoBehaviour {

	public GameObject key;
	public GameObject player;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			float speed=collision.relativeVelocity.magnitude;
			key.GetComponent<LogicKey>.downKey (speed);
		}
	}
}

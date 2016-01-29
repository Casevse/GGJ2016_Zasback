using UnityEngine;
using System.Collections;

public class LogicWall : MonoBehaviour {

	public GameObject key;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision colision){
		if (colision.gameObject.tag == "Player") {
			float speed = colision.relativeVelocity.magnitude;
			key.GetComponent<LogicKey>.DownKey (speed);
		}
	}
}

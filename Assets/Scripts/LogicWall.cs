﻿using UnityEngine;
using System.Collections;

public class LogicWall : MonoBehaviour {

	public GameObject key;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D  colision){
		if (colision.gameObject.tag == "Player") {
			float speed=2;
            if (key != null) {
                key.GetComponent<LogicKey>().DownKey(speed);
            }
		}
	}
}

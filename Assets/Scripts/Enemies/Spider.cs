using UnityEngine;
using System.Collections;

public class Spider : Enemy {

	// Use this for initialization
	void Start () {
		initEnemy (1, 1.0f, 1, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (falling && onFloor) {
			Physics.gravity = new Vector2 (0, -9.8f);
			falling = false;
		} else if (!falling && onFloor) {
			if(side){
				transform.Translate(movSpeed * Vector2.right * Time.deltaTime);
			}
			else{
				transform.Translate(movSpeed * Vector2.left * Time.deltaTime);
			}
		}
	}
}

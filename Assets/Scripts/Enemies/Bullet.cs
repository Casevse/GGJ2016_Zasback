using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Wall") {
			Destroy(this);
		} 
		else if(col.gameObject.tag == "Player"){
			//Do damage
			Destroy (this);
		}
	}
}

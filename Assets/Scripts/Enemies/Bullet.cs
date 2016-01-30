using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage;

	// Use this for initialization
	void Start () {
		damage = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Wall") {
			Destroy(gameObject);
		} 
		else if(col.gameObject.tag == "Player"){
			PlayerStats stats = col.gameObject.GetComponent<PlayerStats>();
			if(stats != null){
				stats.RemoveFat(damage);
			}
			Destroy (gameObject);
		}
	}
}

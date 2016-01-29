using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	protected int life;
	protected float movSpeed;
	protected int damage;
	protected float fallSpeed;
	protected bool onFloor;
	protected bool side;
	protected bool falling;

	// Use this for initialization
	protected void Start () {
	
	}

	protected void initEnemy(int l, float mS, int dmg, float fS){
		int rand = Random.Range (1, 3);
		life = l;
		movSpeed = mS;
		damage = dmg;
		fallSpeed = fS;
		onFloor = false;
		if (rand == 1) {
			side = true; //derecha
		} else {
			side = false; //izquierda
		}
		falling = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setLife(int l){
		life = l;
	}
	public int getLife(){
		return life;
	}

	public void setMovSpeed(float mS){
		movSpeed = mS;
	}
	public float getMovSpeed(){
		return movSpeed;
	}

	public void setDamage(int dmg){
		damage = dmg;
	}
	public int getDamage(){
		return damage;
	}

	public void setFallSpeed(float fS){
		fallSpeed = fS;
	}
	public float getFallSpeed(){
		return fallSpeed;
	}

	public void setOnFloor(bool onF){
		onFloor = onF;
	}
	public bool getOnFloor(){
		return onFloor;
	}

	public void setSide(bool s){
		side = s;
	}
	public bool getSide(){
		return side;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Floor" && !onFloor) {
			onFloor = true;
		} else if (col.gameObject.tag == "Wall") {
			side = !side;
		}
	}
}

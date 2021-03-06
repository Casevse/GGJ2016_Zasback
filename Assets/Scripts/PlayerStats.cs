﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public int fat;
	public int maxFat;

	private int modifyFat;

	public Image barFull;
	public Image barEmpty;
	public Text barText;

	public int progress;

	private float screenX;
	private float screenY;
	private float widthBar;
	private float heightBar;
	private float fatBar;

	private Color colorBar;

    private float invulnerableTime = 0.0f;
    private SpriteRenderer spriteRenderer;
    private Player player;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

	// Use this for initialization
	void Start () {
		modifyFat = 0;
		heightBar = barEmpty.rectTransform.rect.height;
		widthBar = barEmpty.rectTransform.rect.width;

		colorBar.r = ((float)(211*100)/255)/100;
		colorBar.g = ((float)(47*100)/255)/100;
		colorBar.b = ((float)(47*100)/255)/100;
		colorBar.a = ((float)(255*100)/255)/100;

		barText.text = "Greasy";
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > invulnerableTime) {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            Color color = spriteRenderer.color;
            color.a = 1.0f;
            spriteRenderer.color = color;
        } else {
            // Parpadeo
            Color color = spriteRenderer.color;
            color.a = 0.75f + Mathf.Sin((invulnerableTime - Time.time) * 16.0f) * 0.25f;
            spriteRenderer.color = color;
        }

		if (!IsDead ()) {
			barFull.rectTransform.sizeDelta = new Vector2 ((widthBar * fat) / 100,heightBar);
			if (fat > maxFat / 2) {
				float g = (164 * (maxFat - fat) / 50 + 47);
				colorBar.g = ((float)(g * 100) / 255) / 100;	
			} else {
				float r = 211 - (164 * ((maxFat / 2) -fat)) / (maxFat / 2); //(164 * (fat-(maxFat/2)) / 50 + 47);
				colorBar.r = ((float)(r * 100) / 255) / 100;
			}

			barFull.color =colorBar;

			if(fat >= maxFat*0.75 )
				barText.text = "Greasy";
			else if(fat >= maxFat*0.5 )
				barText.text = "Fat";
			else if(fat >= maxFat*0.25 ) 
				barText.text = "Normal";
			else
				barText.text = "Thin";
			
			if (modifyFat < 0) {
				fat -=progress;
				if (fat <= 0)
					modifyFat = 0;
				else
					modifyFat+=progress;
			}else if (modifyFat > 0) {
				if (fat >= maxFat)
					modifyFat = 0;
				else {
					modifyFat -= progress;
					fat += progress;
				}
			} else {
                modifyFat = 0;
            }
		} else {
            Destroy(this.gameObject);
        }
	}


	public void AddFat(int value) {
		modifyFat += value;

	}

	public void RemoveFat(int value) {
        if (player.attackPhase != Player.AttackPhase.END) {
            modifyFat -= value;
            SoundSingleton.Singleton.PlayHitPlayer();
            this.gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            invulnerableTime = Time.time + 0.5f;
        }
    }

	public bool IsDead() {
		if (fat <= 0)
			return true;
		else
			return false;

	}

}

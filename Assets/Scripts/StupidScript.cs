using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StupidScript : MonoBehaviour {

    public float splashTime = 4.0f;
    public SpriteRenderer spriteRenderer;

    private float nextSplash;
    private float fadeTime;

	// Use this for initialization
	void Start () {
        nextSplash = Time.time + splashTime;
        fadeTime = nextSplash - 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > fadeTime) {
            Color color = spriteRenderer.color;
            color.a = 1.0f - (nextSplash - Time.time);
            spriteRenderer.color = color;
        }

	    if (Time.time > nextSplash) {
            Application.LoadLevel("Menu");
        }

	}
}

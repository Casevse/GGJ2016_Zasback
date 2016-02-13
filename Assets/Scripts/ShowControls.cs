using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowControls : MonoBehaviour {

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    private SpriteRenderer spriteRenderer;

	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite1;
    }
	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)) {
            if (spriteRenderer.sprite == sprite1) {
                spriteRenderer.sprite = sprite2;
            } else if (spriteRenderer.sprite == sprite2) {
                spriteRenderer.sprite = sprite3;
            } else {
                Application.LoadLevel("Menu");
            }
        }
	}
}

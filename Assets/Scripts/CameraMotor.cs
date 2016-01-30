using UnityEngine;
using System.Collections;

public class CameraMotor : MonoBehaviour {

    private Camera camera;

    private void Awake() {
        camera = GetComponent<Camera>();
    }

	private void Update () {
        Debug.Log(Screen.width);
        camera.orthographicSize = Screen.width / 100.0f;
	}

}

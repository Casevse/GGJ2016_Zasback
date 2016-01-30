using UnityEngine;
using System.Collections;

public class CameraMotor : MonoBehaviour {
    public int PIXELS_TO_UNITS = 24;

    private Camera camera;

    private void Awake() {
        camera = GetComponent<Camera>();
    }

	private void Update () {
        float TARGET_WIDTH = 960.0f;
        float TARGET_HEIGHT = 540.0f;

        float desiredRatio = TARGET_WIDTH / TARGET_HEIGHT;
        float currentRatio = (float)Screen.width / (float)Screen.height;

        if (currentRatio >= desiredRatio) {
            // Our resolution has plenty of width, so we just need to use the height to determine the camera size
            Camera.main.orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS;
        }
        else {
            // Our camera needs to zoom out further than just fitting in the height of the image.
            // Determine how much bigger it needs to be, then apply that to our original algorithm.
            float differenceInSize = desiredRatio / currentRatio;
            Camera.main.orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS * differenceInSize;
        }
    }

}

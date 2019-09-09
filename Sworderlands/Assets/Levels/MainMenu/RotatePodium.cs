using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePodium : MonoBehaviour {

    [SerializeField, Tooltip("Sets whether the camera focuses on the podium or not")]private bool focusCamera = false;
    [SerializeField, Tooltip("Point the camera looks at when not focused")] private Transform centerScreenCameraTransform;
    [SerializeField, Tooltip("Speed at which the podium rotates")] private int rotateSpeed = 5;
    private Vector3 centerScreenCameraPoint;
    private Vector3 focusCameraPoint;
    private Vector3 rotatePoint;
    private MainMenuCamera menuCamera;

	// Use this for initialization
	void Start () {
        menuCamera = Camera.main.GetComponent<MainMenuCamera>();
        centerScreenCameraPoint = centerScreenCameraTransform.position;
        focusCameraPoint = Level.player.transform.position + Vector3.up;

        rotatePoint = focusCameraPoint;

        if (focusCamera)
            menuCamera.transform.LookAt(focusCameraPoint);

        else
            menuCamera.transform.LookAt(centerScreenCameraPoint);
    }
	
	// Update is called once per frame
	void Update () {

        if (!menuCamera.isSmoothLookAtRunning())
        {
            menuCamera.transform.RotateAround(rotatePoint, Vector3.up, rotateSpeed * Time.deltaTime);
            centerScreenCameraPoint = centerScreenCameraTransform.position;

            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

            if (Input.GetKeyDown("f"))
                setFocusCamera(true);

            if (Input.GetKeyDown("u"))
                setFocusCamera(false);
        }
    }

    public void setFocusCamera(bool focused)
    {
        focusCamera = focused;

        if (focusCamera)
            StartCoroutine(menuCamera.smoothLookAt(focusCameraPoint));

        else
            StartCoroutine(menuCamera.smoothLookAt(centerScreenCameraPoint));
    }
}

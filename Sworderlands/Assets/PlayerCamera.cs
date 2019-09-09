using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private Vector3 defaultPosition;
    private Player player;
    private Vector3 rotatePoint;
    [SerializeField] private float cameraTurnSpeed = 1f;

	// Use this for initialization
	void Start () {

        player = Level.player;
        defaultPosition = player.getDefaultCameraPosition();
        transform.position = defaultPosition;

        rotatePoint = player.transform.position + Vector3.up;
        gameObject.transform.LookAt(rotatePoint);

    }
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(rotatePoint, Vector3.up, Input.GetAxis("Horizontal") * cameraTurnSpeed);
    }
}

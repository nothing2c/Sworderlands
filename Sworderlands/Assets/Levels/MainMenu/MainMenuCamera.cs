using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {

    private bool smoothLookAtRunning;
    [SerializeField, Tooltip("Speed at which the camera turns")] private float turnSpeed = 2;

	// Use this for initialization
	void Start () {
        smoothLookAtRunning = false;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public IEnumerator smoothLookAt(Vector3 point)
    {
        smoothLookAtRunning = true;

        bool matching = false;
        Quaternion rotation = Quaternion.LookRotation(point - transform.position);

        while (!matching)
        {
            if (transform.eulerAngles == rotation.eulerAngles)
                matching = true;
            else
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);

            yield return null;
        }

        smoothLookAtRunning = false;
        yield return null;
    }

    public bool isSmoothLookAtRunning()
    {
        return smoothLookAtRunning;
    }
}

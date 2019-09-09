using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    [SerializeField, Tooltip("Speed at which missile moves")] private float missleSpeed = 7;
    private float missileTime;

	// Use this for initialization
	void Start () {
        missileTime = 5;
	}
	
	// Update is called once per frame
	void Update () {
        missileTimer();

        transform.Translate(transform.forward * missleSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    void missileTimer()
    {
        missileTime -= Time.deltaTime;

        if (missileTime <= 0)
            Destroy(gameObject);
    }
}

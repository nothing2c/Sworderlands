using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private GameObject defaultCameraObj;
    [SerializeField] private GameObject aimingCameraObj;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 getDefaultCameraPosition()
    {
        return defaultCameraObj.transform.position;
    }
}

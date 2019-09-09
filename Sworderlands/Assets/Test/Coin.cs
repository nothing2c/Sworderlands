using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, Collectable {
    
    private SphereCollider col;
        
    // Use this for initialization
    void Start () {
        col = gameObject.GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, 5 * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");

        if (other.gameObject.tag.Equals("Player"))
            onCollect(other.gameObject);
    }

    public void onCollect(GameObject collector)
    {
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        collector.gameObject.GetComponent<MeshRenderer>().material = mat;

        Destroy(gameObject);
    }
}

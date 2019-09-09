using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grasslands : Level {

    // Use this for initialization
    new void Awake () {

        base.Awake();

        checkpoints = new List<Vector3>();
        enemies = new List<Enemy>();

        player.transform.position = spawnPoint.transform.position;
        player.transform.forward = spawnPoint.transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void reloadLevel()
    {
        throw new System.NotImplementedException();
    }
}

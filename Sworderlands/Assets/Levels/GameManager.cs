using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        if (!PlayerPrefs.HasKey("musicVol"))
            PlayerPrefs.SetFloat("musicVol", 1f);

        if (!PlayerPrefs.HasKey("voiceVol"))
            PlayerPrefs.SetFloat("voiceVol", 1f);

        if (!PlayerPrefs.HasKey("sfxVol"))
            PlayerPrefs.SetFloat("sfxVol", 1f);

        if (!PlayerPrefs.HasKey("toggleAim"))
            PlayerPrefs.SetInt("toggleAim", 1);

        PlayerPrefs.Save();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

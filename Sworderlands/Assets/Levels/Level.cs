using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Super class that is used to store references to various assets in levels.
 * For the levelMusic, the Main Camera must have an AudioSource component.
 * The clip is then dragged levelMusic field on this component.
 * All subclasses of Level that overide its Start and Awake methods should call
 * this classes Start and Awake methods to initialize all of this classes variables*/

public abstract class Level : MonoBehaviour{

    [SerializeField, Tooltip("Background music for the level")] protected AudioClip levelMusic;
    // AudioSource on the Main Camera
    private AudioSource musicSource;

    [SerializeField, Tooltip("Initial spawn point of the level")] protected Transform spawnPoint;

    // Global Variables that are used to store references to imortant assets in a level

    /// <summary>
    /// List of all checkpoints in a level
    /// </summary>
    public static List<Vector3> checkpoints;
    /// <summary>
    /// List of all enemies in a level
    /// </summary>
    public static List<Enemy> enemies;
    /// <summary>
    /// The player
    /// </summary>
    public static Player player;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        checkpoints = new List<Vector3>();
        enemies = new List<Enemy>();
    }

    protected void Start()
    {
        setMusic();
        Settings.musicSource = musicSource;
    }

    public void setCheckpoint (int index)
    {

    }

    /// <summary>
    /// Sets the clip on the Main Cameras AudioSource component to the selected levelMusic, adjusts the volume
    /// based on the musicVolume in Settings and plays it
    /// </summary>
    private void setMusic()
    {
        musicSource = Camera.main.GetComponent<AudioSource>();
        musicSource.clip = levelMusic;
        musicSource.volume = Settings.getMusicVolume();
        musicSource.loop = true;
        musicSource.Play(0);
    }

    public abstract void reloadLevel();
}

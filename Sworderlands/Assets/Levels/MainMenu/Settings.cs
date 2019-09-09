using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Used to control the volume levels for all AudioSources amongst
 * other game settings.
 * All values are stored in PlayerPrefs*/

public class Settings : MonoBehaviour {

    // Enum containing the different types of sound
    public enum SoundType { music, voice, sfx };

    /// <summary>
    /// AudioSource on the Main Camera
    /// </summary>
    public static AudioSource musicSource;

    /// <summary>
    /// List of all AudioSources that play voices
    /// </summary>
    public static List<AudioSource> voiceSources = new List<AudioSource>();

    /// <summary>
    /// List of all AudioSources that play sfx
    /// </summary>
    public static List<AudioSource> sfxSources = new List<AudioSource>();


    /// <summary>
    /// Sets the current value in PlayerPrefs key musicVol and updates the AudioSource, musicSource
    /// </summary>
    public static void setMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVol", volume);
        updateSounds(SoundType.music);
    }

    /// <summary>
    /// Returns the current value in the PlayerPrefs key musicVol
    /// </summary>
    public static float getMusicVolume()
    {
        return PlayerPrefs.GetFloat("musicVol");
    }

    /// <summary>
    /// Sets the current value in PlayerPrefs key voiceVol and updates all AudioSources in the voiceSources List
    /// </summary>
    public static void setVoiceVolume(float volume)
    {
        PlayerPrefs.SetFloat("voiceVol", volume);
        updateSounds(SoundType.voice);
    }

    /// <summary>
    /// Returns the current value in the PlayerPrefs key voiceVol
    /// </summary>
    public static float getVoiceVolume()
    {
        return PlayerPrefs.GetFloat("voiceVol");
    }

    /// <summary>
    /// Sets the current value in PlayerPrefs key sfxVol and updates all AudioSources in the sfxSources List
    /// </summary>
    public static void setSfxVolume(float volume)
    {
        PlayerPrefs.SetFloat("sfxVol", volume);
        updateSounds(SoundType.sfx);
    }

    /// <summary>
    /// Returns the current value in the PlayerPrefs key sfxVol
    /// </summary>
    public static float getSfxVolume()
    {
        return PlayerPrefs.GetFloat("sfxVol");
    }

    /// <summary>
    /// Sets the current value in PlayerPrefs key toggleAim (0 = false, 1 = true)
    /// </summary>
    public static void setToggleAim(bool toggle)
    {
        if (toggle)
            PlayerPrefs.SetInt("toggleAim", 1);

        else
            PlayerPrefs.SetInt("toggleAim", 0);
    }

    /// <summary>
    /// Returns the current value in the PlayerPrefs key toggleAim (0 = false, 1 = true)
    /// </summary>
    public static bool getToggleAim()
    {
        int toggle = PlayerPrefs.GetInt("toggleAim");

        if (toggle == 1)
            return true;

        else if (toggle == 0)
            return false;

        // Should never happen
        else
            throw new PlayerPrefsException("Invalid key value");
    }

    // Updates all AudioSources based on the SoundType argument
    private static void updateSounds(SoundType type)
    {
        switch(type)
        {
            case SoundType.music:
                musicSource.volume = getMusicVolume();
                break;

            case SoundType.voice:
                foreach(AudioSource source in voiceSources)
                {
                    source.volume = getVoiceVolume();
                }
                break;

            case SoundType.sfx:
                foreach(AudioSource source in sfxSources)
                {
                    source.volume = getSfxVolume();
                }
                break;

            // Should never happen
            default:
                Debug.Log("Invalid SoundType for Settings");
                break;
        }
    }
}

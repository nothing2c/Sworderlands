using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Super class for all ui elements in the game.
 * Has methods for playing AudioClips when certain ui elements are
 * clicked, hovered over and toggled.*/

public abstract class UIElement : MonoBehaviour {

    // Not always on the UIElement
    protected AudioSource source;

    // SFX that play when the mouse hovers or clicks on the UIElement
    protected AudioClip buttonHover;
    protected AudioClip buttonClick;
    protected AudioClip buttonToggle;

    /* Does not initialize the source as some UIElements will not play AudioClips
     * and those that do need to specify what SoundType they play*/

    protected void Awake()
    {
        buttonHover = Resources.Load<AudioClip>("Audio/SFX/ButtonSounds/MenuButtonHover");
        buttonClick = Resources.Load<AudioClip>("Audio/SFX/ButtonSounds/MenuButtonClick");
        buttonToggle = Resources.Load<AudioClip>("Audio/SFX/ButtonSounds/MenuButtonToggle");
    }

    /// <summary>
    /// Plays the MenuButtonClick AudioClip
    /// </summary>
    public void playButtonClick()
    {
        source.clip = buttonClick;
        source.Play();
    }

    /// <summary>
    /// Plays the MenuButtonToggle AudioClip
    /// </summary>
    public void playButtonToggle()
    {
        source.clip = buttonToggle;
        source.Play();
    }

    /// <summary>
    /// Plays MenuButtonHover AudioClip
    /// </summary>
    public void mouseEnter()
    {
        source.clip = buttonHover;
        source.Play();
    }

    /// <summary>
    /// Empty method to be overwritten by sub-classes if needed
    /// </summary>
    protected void mouseExit()
    {
        throw new System.NotImplementedException();
    }
}

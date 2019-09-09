using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/* This script is to be attatched to all the ui elements on the Options Canvas.
 * It has methods for the functionality for all the elements.
 * An OptionChoice must be selected in the editor, then all the
 * appropriate variables will be initialized (Some may still need to be initialized in the editor).
 * Also comes with a custom editor, OptionsEditor*/

public class Options : UIElement {

    // All types of options that exist in this game
    private enum OptionChoice { musicVol, voiceVol, sfxVol, toggleAim };
    [SerializeField, Tooltip("The type of option on this game object")] private OptionChoice optionChoice;

    // The Slider on this gameObject (if there is one)
    private Slider slider;

    // The Toggle on the gameObject(if there is one)
    private Toggle toggle;

    // The InputField on the gameObject (if there is one)
    private InputField iField;

    [SerializeField, Tooltip("The Text that displays the value of the Slider")] private Text valueText;

    [SerializeField, Tooltip("The AudioClip that plays when an audio Sliders value changes")] private AudioClip sample;

    new void Awake()
    {
        // Initialize AudioClips and Cursors
        base.Awake();

        // The UI component on this gameObject (only one will be initialized, the rest will be null)
        slider = gameObject.GetComponent<Slider>();
        toggle = gameObject.GetComponent<Toggle>();
        iField = gameObject.GetComponent<InputField>();
    }

    void Start () {

        /* Initializes class variables and sets up values four UIElements based
         * on the OptionChoice*/

        switch(optionChoice)
        {
            case OptionChoice.musicVol:
                slider.value = Settings.getMusicVolume();
                break;

            case OptionChoice.voiceVol:
                source = gameObject.GetComponent<AudioSource>();
                source.clip = sample;
                Settings.voiceSources.Add(source);
                slider.value = Settings.getVoiceVolume();
                break;

            case OptionChoice.sfxVol: 
                source = gameObject.GetComponent<AudioSource>();
                source.clip = sample;
                Settings.sfxSources.Add(source);
                slider.value = Settings.getSfxVolume();
                break;

            case OptionChoice.toggleAim:
                source = gameObject.GetComponent<AudioSource>();
                Settings.sfxSources.Add(source);
                toggle.isOn = Settings.getToggleAim();
                break;

            // Should never happen
            default:
                Debug.Log("Invalid OptionChoice for Options");
                break;
        }
    }

    /// <summary>
    /// Update the PlayerPrefs key musicVol to the current value of the slider
    /// and calls updateValueText to make the text match the sliders current value
    /// </summary>
    public void onMusicVolChange()
    {
        Settings.setMusicVolume(slider.value);

        updateValueText();
    }

    /// <summary>
    /// Update the PlayerPrefs key voiceVol to the current value of the slider,
    /// calls updateValueText to make the text match the sliders current value
    /// and plays a sample AudioClip.
    /// </summary>
    public void onVoiceChange()
    {
        Settings.setVoiceVolume(slider.value);

        source.Play();

        updateValueText();
    }

    /// <summary>
    /// Update the PlayerPrefs key sfxVol to the current value of the slider,
    /// calls updateValueText to make the text match the sliders current value
    /// and plays a sample AudioClip.
    /// </summary>
    public void onSfxChange()
    {
        Settings.setSfxVolume(slider.value);

        source.Play();

        updateValueText();
    }

    /// <summary>
    /// Updates the PlayerPrefs key toggleAim to the current value of the toggle
    /// and plays the MenuButtonToggle clip
    /// </summary>
    public void onToggleAimChange()
    {
        Settings.setToggleAim(toggle.isOn);

        playButtonToggle();
    }

    // Updates the valueText text to match the current value of the Slider
    private void updateValueText()
    {
        valueText.text = ((int)(slider.value * 100)).ToString() + "%";
    }


    /* This is the custom editor for the Options Component.
     * It either shows or hides fields based on the OptionChoice selected*/

    [CustomEditor(typeof(Options))]
    public class OptionsEditor : Editor
    {
        // create reference to class
        Options oEdit;

        void OnEnable()
        {
            // Initializes the class reference
            oEdit = (Options)target;
        }

        public override void OnInspectorGUI()
        {
            // Show these in the inspector at all times
            oEdit.optionChoice = (OptionChoice)EditorGUILayout.EnumPopup("Option Type", oEdit.optionChoice);

            // Show these based on the OptionChoice
            switch (oEdit.optionChoice)
            {
                case OptionChoice.musicVol:
                    oEdit.valueText = (Text)EditorGUILayout.ObjectField("Value Text", oEdit.valueText, typeof(Text), true);
                    break;

                case OptionChoice.voiceVol:
                    oEdit.valueText = (Text)EditorGUILayout.ObjectField("Value Text", oEdit.valueText, typeof(Text), true);
                    oEdit.sample = (AudioClip)EditorGUILayout.ObjectField("Voice Sample", oEdit.sample, typeof(AudioClip), true);
                    break;

                case OptionChoice.sfxVol:
                    oEdit.valueText = (Text)EditorGUILayout.ObjectField("Value Text", oEdit.valueText, typeof(Text), true);
                    oEdit.sample = (AudioClip)EditorGUILayout.ObjectField("SFX Sample", oEdit.sample, typeof(AudioClip), true);
                    break;

                case OptionChoice.toggleAim:
                    break;

                // Should never happen
                default:
                    Debug.Log("Invalid OptionChoice for OptionsEditor");
                    break;
            }  
        }
    }
}



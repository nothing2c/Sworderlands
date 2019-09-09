using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This script is to be attatched to all the buttons on the Menu Canvas
 * and the back buttons on the other menus.
 * It has methods that deal with the functionality for all the buttons*/

public class MainMenuButton : UIElement {

    // How much the font fluctuates when the mouse hovers over the button
    private int fontScaleFactor;
    // The text on the button
    private Text text;
    
    new void Awake () {

        // Initialize AudioClips and Cursors
        base.Awake();

        text = gameObject.GetComponentInChildren<Text>();
        fontScaleFactor = 5;

        source = gameObject.GetComponent<AudioSource>();
	}

    void Start()
    {
        // Adds this buttons AudioSource to the List containing all the SFX AuidioSources in Settings
        Settings.sfxSources.Add(source);   
    }

    /// <summary>
    /// Continues the game from the most recent save point when the button is clicked
    /// </summary>
    public void onContinueClick()
    {
        playButtonClick();
    }

    /// <summary>
    /// Changes the current MainMenuState to characterSelectLoad when the button is clicked
    /// </summary>
    public void onLoadGameClick()
    {
        playButtonClick();

        MainMenu.setCurrentState(MainMenu.MainMenuStates.characterSelectLoad);
    }

    /// <summary>
    /// Changes the current MainMenuState to characterSelectNew when the button is clicked
    /// </summary>
    public void onNewGameClick()
    {
        playButtonClick();

        MainMenu.setCurrentState(MainMenu.MainMenuStates.characterSelectNew);
    }

    /// <summary>
    /// Changes the current MainMenuState to customize when the button is clicked
    /// </summary>
    public void onCustomizeClick()
    {
        playButtonClick();

        MainMenu.setCurrentState(MainMenu.MainMenuStates.customize);
    }

    /// <summary>
    /// Changes the current MainMenuState to options when the button is clicked
    /// </summary>
    public void onOptionsClick()
    {
        playButtonClick();

        MainMenu.setCurrentState(MainMenu.MainMenuStates.options);
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void onExitClick()
    {
        Application.Quit();
    }

    /// <summary>
    /// Changes the current MainMenuState to menu when the button is clicked
    /// </summary>
    public void onBackClick()
    {
        playButtonClick();

        MainMenu.setCurrentState(MainMenu.MainMenuStates.menu);
    }

    /// <summary>
    /// Same as mouse enter but enlarges text as well
    /// </summary>
    new public void mouseEnter()
    {
        base.mouseEnter();

        text.fontSize += fontScaleFactor;
    }

    /// <summary>
    /// Same as mouse enter but shrinks text as well
    /// </summary>
    new public void mouseExit()
    {
        text.fontSize -= fontScaleFactor;
    }
}

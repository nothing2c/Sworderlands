using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class inherits from the super class level although doesn't necessarily use many of the
 * attributs and methods of that class as a main menu is not a typical level.
 * It makes use of a day night cycle and has different MainMenuStates that determine
 * what UI elements appear in different CanvasGroups.
 * The default CanvasGroup is the menu CanvasGroup and all other CanvasGroups will be disabled
 * by turning their alha to 0 and setting blocksRaycasts to false*/

public class MainMenu : Level {

    [SerializeField, Tooltip("Initial time of day"), Range(1, DayNightCycle.HOURS_IN_DAY)] private int time = 12;
    DayNightCycle dnc;

    // GameObjects with a CanvasGroup component that are toggled between based on the currentState
    [SerializeField, Tooltip("The main menu Canvas")] private CanvasGroup menu;
    [SerializeField, Tooltip("The character select Canvas")] private CanvasGroup characterSelect;
    [SerializeField, Tooltip("The customize Canvas")] private CanvasGroup customize;
    [SerializeField, Tooltip("The options Canvas")] private CanvasGroup options;

    // The Current enabled menu
    private CanvasGroup currentMenu; 

    // Enum used to determine which UI elements should be visible and where the camera should be looking
    public enum MainMenuStates{ menu, characterSelectLoad, characterSelectNew, customize, options }
    private static MainMenuStates currentState;

    new void Awake ()
    {
        // Used to get the reference to the player
        base.Awake();

        currentMenu = menu;
        currentState = MainMenuStates.menu;

        // Disable all other CanvasGroups
        disableMenu(characterSelect);
        disableMenu(customize);
        disableMenu(options);
    }

    new void Start () {

        // Used to set up the background music
        base.Start();

        dnc = gameObject.AddComponent<DayNightCycle>();
        dnc.setTimeOfDayNow(time);
    }
	
	void Update () {
        
        /* Enables the menu coresponding to the currentState and executes other scripts
         * based on the currentState*/

        switch(currentState)
        {
            case MainMenuStates.menu:
                if(menu.alpha != 1)
                    enableMenu(menu);

                break;

            case MainMenuStates.characterSelectLoad:
                if (characterSelect.alpha != 1)
                    enableMenu(characterSelect);

                break;

            case MainMenuStates.characterSelectNew:
                if (characterSelect.alpha != 1)
                    enableMenu(characterSelect);

                break;

            case MainMenuStates.customize:
                if (customize.alpha != 1)
                    enableMenu(customize);

                break;

            case MainMenuStates.options:
                if (options.alpha != 1)
                    enableMenu(options);

                break;

            // Should never happen
            default:
                Debug.Log("Invalid MainMenuState");
                break;
        }
	}

    /// <summary>
    /// Sets currentState with a MainMenuState
    /// </summary>
    public static void setCurrentState(MainMenuStates state)
    {
        currentState = state;
    }

    /* Disables currentMenu by calling disableMenu with the currentMenu and enables a different
     * CanvasGroup by setting is alpha to 1 and setting blocksRaycasts to true*/
    private void enableMenu(CanvasGroup menu)
    {
        disableMenu(currentMenu);

        menu.alpha = 1;
        menu.blocksRaycasts = true;

        currentMenu = menu;
    }

    /* Disables a CanvasGroup by setting its alpha to 0 and setting blocksRaycasts to false.
     * Always called in enableMenu to ensure only one CanvasGroup is enabled*/
    private void disableMenu(CanvasGroup menu)
    {
        menu.alpha = 0;
        menu.blocksRaycasts = false;
    }

    public override void reloadLevel()
    {
        throw new System.NotImplementedException();
    }
}

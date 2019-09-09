using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* To be used on a skybox with the SkyboxBlend shader (Skybox/Blended).
 * Changes the skybox between night and day textures and also changes
 * the intensity of a light that acts as the sun.
 * Make sure the night-time textures are the first set of textures and the
 * daytime textures are the second set of textures.
 * Also make sure the light that acts as the sun is tagged "Sun"*/

public class DayNightCycle : MonoBehaviour {

    // Light with the tag "Sun"
    private Light sun;
    private Material skybox;

    // Effects how long days are (i.e. timeScale of 1, days last 24 seconds)
    private int timeScale;

    // Used in calculations for framerate independency
    private float actualTimeOfDay;
    // Rounded down variant of actualTimeOfDay that is used in determining the time
    private int timeOfDay;

    // Used to check if either of the changeEnvironment coroutines(IEnumerators) are running
    private bool changeEnvironmentRunning;

    // Constants determing details about an in-game day
    public const int HOURS_IN_DAY = 24;
    public const int DAWN = 6;
    public const int MORNING = 9;
    public const int EVENING = 18;
    public const int NIGHT = 21;

    void Awake () {
        sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        skybox = RenderSettings.skybox;

        setTimeOfDayNow(MORNING);
        setTimeScale(2);
        changeEnvironmentRunning = false;
    }
	
	void Update () {
        setTimeOfDay(actualTimeOfDay += Time.deltaTime / timeScale);

        /* changeEnvironment coroutine times are calculated based on the difference betwwen the next time constant
         * and the current time constat, multiplied by the timeScale. Both changeEnvironment coroutines cannot
         * run at the same time due to the bool changeEnvironmentRunning*/

        switch (timeOfDay)
        {
            case DAWN:
                if(!changeEnvironmentRunning)
                    StartCoroutine(changeEnvironmentToDay((MORNING - DAWN) * timeScale));
                break;

            case MORNING:
                break;

            case EVENING:
                if (!changeEnvironmentRunning)
                    StartCoroutine(changeEnvironmentToNight((NIGHT - EVENING) * timeScale));
                break;

            case NIGHT:
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Used to change time and enviroment instantly
    /// </summary>
    public void setTimeOfDayNow(float time)
    {
        actualTimeOfDay = time;
        timeOfDay = (int)actualTimeOfDay;

        //Sets the skybox and sun intensity to match the current time
        if (timeOfDay >= MORNING && timeOfDay < EVENING - 1)
        {
            skybox.SetFloat("_Blend", 1);
            sun.intensity = 1;
        }
        else
        {
            skybox.SetFloat("_Blend", 0);
            sun.intensity = 0;
        }
    }

    /// <summary>
    /// Returns the current time
    /// </summary>
    public int getTimeOfDay()
    {
        return timeOfDay;
    }

    /// <summary>
    /// Sets the timescale (i.e. timeScale of 1, days last 24 seconds)
    /// </summary>
    public void setTimeScale(int scale)
    {
        timeScale = scale;
    }

    /// <summary>
    /// Returns the timeScale used in this DayNightCycle
    /// </summary>
    public int getTimeScale()
    {
        return timeScale;
    }

    // Only used in this scripts update
    private void setTimeOfDay(float time)
    {
        actualTimeOfDay = time;
        timeOfDay = (int)actualTimeOfDay;

        //Reset when day ends
        if (timeOfDay % HOURS_IN_DAY == 0 && timeOfDay != 0)
        {
            actualTimeOfDay = 0f;
            timeOfDay = 0;
        }
    }

    // Changes the skybox to daytime textures and increases the sun intensity over the period of time "blendTime"
    private IEnumerator changeEnvironmentToDay(float blendTime)
    {
        changeEnvironmentRunning = true;

        bool needToUpdate = true;
        float blendFactor = skybox.GetFloat("_Blend");

        // Make sure this float(NOT blendFactor) is the same as the max value in the blend range
        while (blendFactor <= 1f && needToUpdate)
        {
            blendFactor += Time.deltaTime / blendTime;

            // Used to Make sure it doesn't exceed the max value in the blend range and changes needToUpdate to false to break the loop
            if (blendFactor > 1f)
            {
                blendFactor = 1f;
                needToUpdate = false;
            }

            skybox.SetFloat("_Blend", blendFactor);
            sun.intensity = blendFactor;

            yield return null;
        }

        changeEnvironmentRunning = false;
        yield return null;   
    }

    // Changes the skybox to night-time textures and reduces the sun intensity over the period of time "blendTime"
    private IEnumerator changeEnvironmentToNight(float blendTime)
    {
        changeEnvironmentRunning = true;

        bool needToUpdate = true;
        float blendFactor = skybox.GetFloat("_Blend");

        // Make sure this float(NOT blendFactor) is the same as the min value in the blend range
        while (blendFactor >= 0f && needToUpdate)
        {
            blendFactor -= Time.deltaTime / blendTime;

            // Used to Make sure it doesn't exceed the min value in the blend range and changes needToUpdate to false to break the loop
            if (blendFactor < 0f)
            {
                blendFactor = 0f;
                needToUpdate = false;
            }

            skybox.SetFloat("_Blend", blendFactor);
            sun.intensity = blendFactor;

            yield return null;
        }

        changeEnvironmentRunning = false;
        yield return null;
    }
}

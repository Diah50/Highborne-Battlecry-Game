/* TimeCycle.cs - Highborne Universe
 * 
 * Creation Date: 28/07/2023
 * Authors: C137
 * Original: C137
 * 
 * Changes: 
 *      [28/07/2023] - Initial implementation (C137)
 *      [29/07/2023] - Improved day length calculations (C137)
 *      [02/08/2023] - Removed unnecessary logging + Use of new singleton system (C137)
 */
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCycle : Singleton<TimeCycle>
{
    /// <summary>
    /// Length of a day in minutes
    /// </summary>
    [InspectorName("Day Length(In Minutes)")]
    public float dayLength = 2;

    /// <summary>
    /// Current time of day
    /// </summary>
    public DateTime currentDateTime;

    /// <summary>
    /// Start time 
    /// </summary>
    public DateTime startDateTime = new DateTime(1999, 1, 1);

    /// <summary>
    /// The starting hour of the time
    /// </summary>
    public float startHour;

    /// <summary>
    /// The UI sun showing the time
    /// </summary>
    public Transform sun;

    /// <summary>
    /// The UI clock showing the current time
    /// </summary>
    public TextMeshProUGUI clock;

    /// <summary>
    /// The hour at which the sun rises
    /// </summary>
    public float sunriseHour;

    /// <summary>
    /// The hour at which the sun sets
    /// </summary>
    public float sunsetHour;

    /// <summary>
    /// Hour at which sun rises in timespan
    /// </summary>
    private TimeSpan sunriseTime;

    /// <summary>
    /// Hour at which sun sets in timespan
    /// </summary>
    private TimeSpan sunsetTime;

    void Awake()
    {
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

    }
    void Update()
    {
        float increment = /*Total seconds in a day*/ 86400 / (dayLength * 60) /*day length in seconds*/;

        currentDateTime = currentDateTime.AddSeconds(increment * Time.deltaTime);

        RotateSun();
    }

    void RotateSun()
    {
        float sunRotation;

        if (currentDateTime.TimeOfDay > sunriseTime && currentDateTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);

            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentDateTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentDateTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sun.localRotation = Quaternion.AngleAxis(sunRotation, Vector3.back);

        clock.text = $"{GetFormattedTme(currentDateTime.Hour)}:{GetFormattedTme(currentDateTime.Minute)}";

        //Formats the time by adding 0 in front of it when needed
        static string GetFormattedTme(float time)
        {
            return time < 10 ? $"0{time}" : time.ToString();
        }
    }

    TimeSpan CalculateTimeDifference(TimeSpan from, TimeSpan to)
    {
        TimeSpan difference = to - from;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}

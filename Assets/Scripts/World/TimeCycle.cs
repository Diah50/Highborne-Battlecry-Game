/*
 * TimeCycle.cs - Highborne Universe
 * 
 * Creation Date: 28/07/2023
 * Authors: C137
 * Original : C137
 * 
 * Changes: 
 *      [28/07/2023] - Initial implementation (C137)
 */
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    /// <summary>
    /// Singleton reference
    /// </summary>
    public static TimeCycle instance;

    /// <summary>
    /// Length of a day in hours
    /// In 24 IRL hours, how many hours should pass IG
    /// </summary>
    public float dayLength = 24; // 48h = one IRL day = 2 IG days

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
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;

        currentDateTime = startDateTime + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    void Update()
    {
        float IGSecondsPerRealSeconds = (dayLength * 3600) / (24 * 3600);

        currentDateTime = currentDateTime.AddSeconds(Time.deltaTime * IGSecondsPerRealSeconds);

        Debug.Log(currentDateTime);

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

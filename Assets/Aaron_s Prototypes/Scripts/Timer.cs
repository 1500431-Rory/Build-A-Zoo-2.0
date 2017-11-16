using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Timer : MonoBehaviour {

    public Light dimLight;
    public float cycle = 12;

    public float fadeSpeed = 0.5f;

    public float nightTimeIntensity = 0.2f;
    public float dayTimeIntensity = 1.0f;

    public float dayTime = 0;
    private float nightTime = 0;
    private float targetIntensity;

public class Clock
    {
        private System.DateTime now;
        private System.TimeSpan timeNow;
        private System.TimeSpan gameTime;
        private int minutesPerDay; //Realtime minutes per game-day (1440 would be realtime)

        public Clock(int minPerDay)
        {
            minutesPerDay = minPerDay;
        }
        public System.TimeSpan GetTime()
        {
            now = System.DateTime.Now;
            timeNow = now.TimeOfDay;
            double hours = timeNow.TotalMinutes % minutesPerDay;
            double minutes = (hours % 1) * 60 *60;
            double seconds = (minutes % 1) * 60 *60;
            gameTime = new System.TimeSpan((int)hours, (int)minutes, (int)seconds);

            return gameTime ;
        }
    }

    Clock clock;

    void Start()
    {
        clock = new Clock(24);
    }

    private void Awake()
    {
        dayTime = 6;
        nightTime = dayTime + cycle;

        dimLight.intensity = dayTimeIntensity;
        targetIntensity = nightTimeIntensity;
    }

    void Update()
    {
        if (clock.GetTime().Hours >= dayTime && clock.GetTime().Hours <= nightTime)
        {
            targetIntensity = dayTimeIntensity;
            dimLight.intensity = Mathf.Lerp(dimLight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            dimLight.color = new Vector4(1, 1, 1, 1);
        }
        else
        {
            targetIntensity = nightTimeIntensity;
            dimLight.intensity = Mathf.Lerp(dimLight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            dimLight.color = new Vector4(0.9f, 0.9f, 0.9f, 1);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 20), clock.GetTime().ToString());
    }
}

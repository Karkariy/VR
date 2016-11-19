using UnityEngine;
using System;


public class SunBehavior : MonoBehaviour {

    // levée du soleil à Dehzou ~ 7h
    //0 deg rot X -> 8h

    public Light m_sunLight;
    private DateTime m_clock;
    public int m_sunrise;
    public int m_sunset;


    // Use this for initialization
    void Start () {
        m_clock = DateTime.Now;

        float x = ((m_clock.Hour + m_clock.Minute/60.0f) - m_sunrise)/((m_sunset - m_sunrise))*180.0f;

        m_sunLight.transform.eulerAngles = new Vector3(x, 180.0f, 0.0f);
    }

    // Update is called once per frame
    void Update () {
        //m_clock = m_clock.AddSeconds(Time.deltaTime);
        m_clock = m_clock.AddMinutes(Time.deltaTime*100.0f);

        float x = ((m_clock.Hour + m_clock.Minute / 60.0f) - m_sunrise) / ((m_sunset - m_sunrise)) * 180.0f;
        transform.eulerAngles = new Vector3(x, 180.0f,0.0f);
        updateIntensity(m_clock);

    }

    private void updateIntensity(DateTime time)
    {
        float intensity = -1.0f;
        if (time.Hour <= (m_sunrise + m_sunset) / 2 && time.Hour >= m_sunrise)
        {
            intensity = Mathf.Clamp(time.Hour, m_sunrise, ((m_sunrise + m_sunset) / 2));
            Debug.LogWarning(intensity);
        }
        else if(time.Hour < m_sunrise || time.Hour> m_sunset)
        {
            intensity = 5.0f;
            Debug.Log("Ok");

        }
        else
        {
            float max = 1.0f / ((m_sunrise + m_sunset) / 2.0f);
            float min = 1.0f / m_sunset;

            intensity = Mathf.Clamp(time.Hour, min * 100, max * 100);
            Debug.Log(intensity);
        }

        m_sunLight.intensity = 0.05f * intensity;

    }

}

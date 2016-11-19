using UnityEngine;
using System;


public class SunBehavior : MonoBehaviour {

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
            intensity = 0.05f * Mathf.Clamp(time.Hour, m_sunrise, ((m_sunrise + m_sunset) / 2));
        }
        else if(time.Hour < m_sunrise || time.Hour> m_sunset)
        {
            intensity = 0.05f * 5.0f;
        }
        else
        {
            float max = 1.0f / ((m_sunrise + m_sunset) / 2.0f);
            float min = 1.0f / m_sunset;
            intensity = 0.05f * Mathf.Clamp(time.Hour, min * 100, max * 100);

        }

        m_sunLight.intensity =  intensity;

    }

}

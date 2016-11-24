using UnityEngine;
using System;
using UnityEngine.UI;


public class SunBehavior : MonoBehaviour {
    /// <summary>
    /// 
    /// </summary>
    private DateTime m_clock;
    /// <summary>
    /// 
    /// </summary>
    public Slider m_sliderClock;
    /// <summary>
    /// 
    /// </summary>
    public Text m_clockToString;
    /// <summary>
    /// 
    /// </summary>
    public Light m_sunLight;
    /// <summary>
    /// 
    /// </summary>
    public int m_sunrise;
    /// <summary>
    /// 
    /// </summary>
    public int m_sunset;


    // Use this for initialization
    void Start () {
        m_clock = DateTime.Now;
        float x = ((m_clock.Hour + m_clock.Minute/60.0f) - m_sunrise)/((m_sunset - m_sunrise))*180.0f;
        m_sunLight.transform.eulerAngles = new Vector3(x, 180.0f, 0.0f);
        m_sliderClock.value = (m_clock.Hour + m_clock.Minute / 60.0f + m_clock.Second / 60.0f);
        Debug.Log(m_sliderClock.value);
        m_sliderClock.onValueChanged.AddListener(delegate { updateTime(); });
        updateClockText();
    }


    /// <summary>
    /// update the text that display the clock
    /// </summary>
    private void updateClockText()
    {
        m_clockToString.text = m_clock.Hour + "  :  " + m_clock.Minute + "  :  " + m_clock.Second;
    }

    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void Update () {
        m_clock = m_clock.AddSeconds(Time.deltaTime);

        float x = ((m_clock.Hour + m_clock.Minute / 60.0f) - m_sunrise) / ((m_sunset - m_sunrise)) * 180.0f;
        transform.eulerAngles = new Vector3(x, 180.0f,0.0f);
        updateIntensity(m_clock);
        updateClockText();


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    private void updateIntensity(DateTime time)
    {
        float intensity = -1.0f;
        float lightScale = 0.08f;
        Light [] LightChlidren = GetComponentsInChildren<Light>();

        if (time.Hour <= (m_sunrise + m_sunset) / 2 && time.Hour >= m_sunrise)
        {
            foreach (Light light in LightChlidren)
                light.intensity = 0.2f;
            intensity = lightScale * Mathf.Clamp(time.Hour, m_sunrise, ((m_sunrise + m_sunset) / 2));
        }
        else if(time.Hour < m_sunrise || time.Hour> m_sunset)
        {
            foreach (Light light in LightChlidren)
                light.intensity = 0.1f;
            intensity = lightScale * 5.0f;

        }
        else
        {
            foreach (Light light in LightChlidren)
                light.intensity = 0.2f;
            float max = 1.0f / ((m_sunrise + m_sunset) / 2.0f);
            float min = 1.0f / m_sunset;
            intensity = lightScale * Mathf.Clamp(time.Hour, min * 100, max * 100);

        }

        m_sunLight.intensity =  intensity;

    }


    /// <summary>
    /// 
    /// </summary>
    protected void updateTime()
    {
       
        double minutes = ((m_sliderClock.value - Math.Truncate(m_sliderClock.value))*59);
        double secondes = (minutes - Math.Truncate(minutes)) * 59;
        if (m_sliderClock.value == 24)
            m_sliderClock.value = 23;

        m_clock = new DateTime(m_clock.Year,
                               m_clock.Month,
                               m_clock.Day,
                               (int)m_sliderClock.value,
                               (int)minutes,
                               (int)secondes
                               );

    }

}

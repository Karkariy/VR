using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

    private int m_hour;
    private int m_minutes;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ClockTicTac();

	}

    private void ClockTicTac()
    {
        m_minutes++;
        if (m_minutes >= 60)
        {
            m_minutes = 0;
            m_hour++;
            if (m_hour >= 24)
                m_hour = 0;
        }
     //   Debug.Log("Hours : " + m_hour + "  :  " + m_minutes);

    }



    public int GetHours
    {
        get { return m_hour; }
        set { m_hour = value; }
    }

    public int GetMinutes
    {
        get { return m_minutes; }
        set { m_minutes = value; }
    }
}

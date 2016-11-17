using UnityEngine;
using System.Collections;


public class SunBehavior : MonoBehaviour {

    // levée du soleil à Dehzou ~ 7h
    //0 deg rot X -> 8h

    public Light m_SunLight;
    public Clock m_clock;
    
  

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {

        m_SunLight.transform.Rotate(Vector3.right * Time.deltaTime* transformTimeToRotation(m_clock.GetHours, m_clock.GetMinutes), Space.World); 

    }

    private float transformTimeToRotation(int hour, int minutes)
    {
        Debug.Log("position : " + transform.rotation.eulerAngles);

        if (hour > 6 && hour < 17)
        {
            m_SunLight.intensity = 0.5f;
            return transform.rotation.eulerAngles.x + 0.002f;
        }
        else if (hour == 6)
        {
            m_SunLight.intensity = 0.1f;
            return 0.0f;
        }
        else if (hour == 17)
        {
            m_SunLight.intensity = 0.1f;
            return 0.0f;
        }

        return 0.0f;
    }

}

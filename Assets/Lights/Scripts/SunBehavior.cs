using UnityEngine;
using System.Collections;

public class SunBehavior : MonoBehaviour {

    // levée du soleil à Dehzou ~ 7h
    //0 deg rot X -> 8h

    public Light m_SunLight;
    public int m_hour;
    public int m_minutes;

	// Use this for initialization
	void Start () {
        Vector3 sunRotation = transformTimeToRotation(m_hour, m_minutes);
        m_SunLight.transform.localEulerAngles= sunRotation;
    }

    // Update is called once per frame
    void Update () {
	


	}

    private Vector3 transformTimeToRotation(int m_hour, int m_minutes)
    {
        int x = 0;
        Vector3 sunRotation = new Vector3(x,180,0);
        return sunRotation;
    }

}

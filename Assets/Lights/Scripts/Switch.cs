using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    public ParabolicPointer pointer;
    private Vector3 pointerTarget;
    private bool bMouseDown;
    private bool bLightActive;




    // Use this for initialization
    void Start () {
        bMouseDown = true;
	}
	
	// Update is called once per frame
	void Update () {
        //pointerTarget = pointer.SelectedPoint;
        //if (Input.GetMouseButtonUp(0))
        //    bMouseDown = true;
        //if (Vector3.Distance(pointerTarget, transform.position) < .15)
        //{
        //    HighLightSwitch();
        //    if (Input.GetMouseButton(0) && bMouseDown)
        //        SwitchLight();

        //}
    }

    public void SwitchLight()
    {
        bMouseDown = false;
        bLightActive = !bLightActive;
        Light[] lights = GetComponentsInChildren<Light>();
        
        foreach (Light lightVar in lights)
            if(bLightActive)
                lightVar.GetComponent<Light>().intensity = 0.0f;
            else
                lightVar.GetComponent<Light>().intensity = 1.0f;
    }

    private void HighLightSwitch()
    {

    }


    void OnTriggerEnter(Collider collider)
    {
        if (ReferenceEquals(pointer, collider))
            SwitchLight();
    }

}

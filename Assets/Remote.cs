using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        //if(collider.GetType() == typeof(Switch))
        Switch swi = collider.gameObject.GetComponent<Switch>();
        if (swi)
            swi.SwitchLight();
    }

}

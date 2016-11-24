using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        RaycastHit hit;

        float rayLenght = 100000;
        Debug.DrawLine(ray.origin,ray.direction * rayLenght);

        if(Physics.Raycast(ray,out hit,rayLenght))
        {
            Vector3 hitPoint = hit.point;
            Debug.Log(hitPoint);

            NavMeshHit nhit;
            if(!NavMesh.Raycast(Camera.main.transform.position,hitPoint,out nhit,NavMesh.AllAreas))
            {
                if(Input.GetKey(KeyCode.Space))
                {
                    Debug.Log(nhit);
                    Debug.DrawLine(transform.position, hitPoint, Color.green);

                    transform.position = hitPoint + new Vector3(0, 1.8f, 0);
                }
            }
        }
    }
}

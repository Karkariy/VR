using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = -180.0f;
    private float pitch = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        clampCamera(ref yaw, ref pitch);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void clampCamera(ref float yaw, ref float pitch)
    {
        if (pitch > 90) pitch = 90;
        if (pitch < -90) pitch = -90;
    }
}
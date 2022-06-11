using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float mouseXmove, mouseYmove, mouseXrotate, mouseYrotate;
    void Start()
    {

        //transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1)) {
            mouseXmove = Input.GetAxis("Mouse X") * 1;
            mouseYmove = Input.GetAxis("Mouse Y") * 1;
            transform.position += -transform.right * mouseXmove;
            transform.position += -transform.up * mouseYmove;
        }
        if (Input.GetMouseButton(2)) {
            mouseXrotate += Input.GetAxis("Mouse X") * 3;
            mouseYrotate += Input.GetAxis("Mouse Y") * 3;
            transform.rotation = Quaternion.Euler(-mouseYrotate, mouseXrotate, 0);
        }

        transform.position += transform.forward * Input.mouseScrollDelta.y;
    }
}

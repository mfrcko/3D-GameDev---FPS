using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    //Public
    public float mouseSensitivity = 100f;
    public Transform playerTransform;

    //Private
    float xRotate = 0f;
    
    void Start()
    {
        //Cursor always in the middle of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

   
    void Update()
    {
        //Data about mouse movement inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90, 90f);

        //Apllying rotation
        transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f); 
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}

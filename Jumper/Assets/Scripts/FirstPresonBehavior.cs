using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPresonBehavior : MonoBehaviour
{

    public float speed = 5.0f; 
    public float sensitivity = 2.0f; 
    public float maxYAngle = 80.0f; 
    public float minYAngle = -80.0f; 

    private float rotationX = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;
        transform.Translate(movement);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX * sensitivity, 0);
    }
}











using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DBehavior : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Update()
    {
        // Player movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Jumping (optional)
        // Uncomment the following lines if you want jumping functionality
        /*
        if (Input.GetButtonDown("Jump"))
        {
            // Add jump logic here
        }
        */

        // Running (optional)
        // Uncomment the following lines if you want running functionality
        /*
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10.0f; // Adjust the running speed
        }
        else
        {
            moveSpeed = 5.0f; // Set the regular speed
        }
        */

        // Add any other controls or interactions as needed
    }
}



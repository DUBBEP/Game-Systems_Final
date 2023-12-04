using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DBehavior : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        /*
        if (Input.GetButtonDown("Jump"))
        {
            
        } 
        */
      
    }
}



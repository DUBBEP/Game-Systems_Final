using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform playerPosition;

    private void Update()
    {
        if (playerPosition.position.y > this.transform.position.y)
        {
            this.transform.position = new Vector3(transform.position.x, playerPosition.position.y, transform.position.z);
        }
    }
}

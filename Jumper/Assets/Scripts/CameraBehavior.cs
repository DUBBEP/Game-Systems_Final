using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float offSet;
    public Transform playerPosition;

    private void Update()
    {
        if (!GameManager.instance.playersSpawned)
            return;

        if (playerPosition.position.y > transform.position.y + offSet)
            transform.position = new Vector3(transform.position.x, playerPosition.position.y - offSet, transform.position.z);
    }
}

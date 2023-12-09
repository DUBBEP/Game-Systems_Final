using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float bounceForce;
    public float cullOffSet;

    private void Update()
    {
        float cullingDistance = GameManager.instance.highestPlayerPosition - cullOffSet;
        if (transform.position.y < cullingDistance)
            Destroy(gameObject);
    }
}

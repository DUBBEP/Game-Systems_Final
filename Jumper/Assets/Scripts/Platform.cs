using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float bounceForce;

    private void Update()
    {
        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 8)
            Destroy(gameObject);

    }
}

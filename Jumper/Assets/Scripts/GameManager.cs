using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float platSpawnPos;
    private float topOfScreen;
    private int platSpawnOffSet = 5;
    
    public float platSpawnRate;


    public GameObject platPrefab;
    public static GameManager instance;
    private void Awake()
    {

        topOfScreen = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        platSpawnPos = topOfScreen;
    }

    private void Update()
    {
        topOfScreen = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        // if camera position increases by a certain ammount spawn the platforms
        if (topOfScreen > platSpawnPos + Mathf.Log(platSpawnPos, platSpawnRate))
        {
            SpawnPlatform(topOfScreen + platSpawnOffSet);
            platSpawnPos = topOfScreen;
        }
    }

    void SpawnPlatform(float yPos)
    {
        float leftBoundry = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        float rightBoundry = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

        Vector3 randomOffSet = new Vector3(Random.Range(leftBoundry + 4, rightBoundry - 4), Random.Range(yPos - 2, yPos + 2), 0);

        Instantiate(platPrefab, randomOffSet, Quaternion.identity);
    }
}

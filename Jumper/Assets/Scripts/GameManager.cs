using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    [Header("Info")]
    private int playersInGame;
    public float highestPlayerPosition;

    [Header("Platforms")]
    private int platSpawnOffSet = 5;
    public float platSpawnRate;
    public string platformPrefabLocation;
    public float platSpawnTrigger;


    [Header("Players")]
    public string playerPrefabLocation;
    public PlayerBehavior[] players;
    public int alivePlayers;
    public bool playersSpawned = false;
    
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        platSpawnTrigger = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
    }

    private void Start()
    {
        players = new PlayerBehavior[PhotonNetwork.PlayerList.Length];
        alivePlayers = players.Length;
        playersSpawned = false;

        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void ImInGame()
    {
        playersInGame++;

        if (PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
            photonView.RPC("SpawnPlayer", RpcTarget.All);
    }

    [PunRPC]
    void SpawnPlayer()
    {
        float rightBoundry = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 2;
        Vector3 spawnPosition = new Vector3(Random.Range(2, rightBoundry), Random.Range(-5, 5), 0);
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPosition, Quaternion.identity);
        PhotonNetwork.Instantiate(platformPrefabLocation, spawnPosition - new Vector3(0,3,0), Quaternion.identity);

        // initialize the player for all other players
        playerObj.GetComponent<PlayerBehavior>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);

        Invoke("SetSpawnedTrue", 0.5f);
    }

    void SetSpawnedTrue()
    {
        playersSpawned = true;
    }


    public void SpawnPlatform(float highestY)
    {
        float ySpawn = highestY + platSpawnOffSet;
        
        int groupCount = (1000 - (int)highestY) / 250;
        
        if (highestY > 1000)
            groupCount = 1;


        float leftBoundry = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        float rightBoundry = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

        for (int i = 0; i < groupCount; i++)
        {
            Vector3 randomOffSet = new Vector3(Random.Range(leftBoundry + 4, rightBoundry - 4), Random.Range(ySpawn - 3, ySpawn + 6), 0);
            PhotonNetwork.Instantiate(platformPrefabLocation, randomOffSet, Quaternion.identity);
        }
    }

    [PunRPC]

    public void UpdatePlatformTrigger(float highestY)
    {
        highestPlayerPosition = highestY;
        platSpawnTrigger = highestY - platSpawnRate;
    }

}

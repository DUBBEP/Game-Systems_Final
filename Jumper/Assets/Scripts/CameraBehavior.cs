using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float offSet;
    public PlayerBehavior targetplayer;
    public PlayerBehavior highestPlayer;


    private void Update()
    {

        if (targetplayer == null || !GameManager.instance.playersSpawned)
            return;

        if (targetplayer.dead && highestPlayer != targetplayer)
        {
            targetplayer = GameManager.instance.highestPlayer;
            if (targetplayer == null)
                return;
        }

        if (targetplayer.transform.position.y > transform.position.y + offSet )
            transform.position = new Vector3(transform.position.x, targetplayer.transform.position.y - offSet, transform.position.z);
    }



    public void SetHighestPlayer(PlayerBehavior highest)
    {
        highestPlayer = highest;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerBehavior : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Info")]
    public int id;
    private float xInput;
    private bool inFastFall;
    public bool LocalPlayer;

    [Header("ForPlatformSpawning")]

    [Header("Stats")]
    public int moveSpeed;
    public int MaxSpeed;
    public int fastFallSpeed;
    public int defaultFallSpeed;
    public bool dead;

    [Header("Components")]
    private Rigidbody2D rb;
    public Player photonPlayer;
    public SpriteRenderer playerSprite;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    [PunRPC]
    public void Initialize(Player player)
    {
        id = player.ActorNumber;
        photonPlayer = player;
        LocalPlayer = false;
        dead = false;

        GameManager.instance.players[id - 1] = this;

        // is this not our local player?
        if (!photonView.IsMine)
        {
            rb.isKinematic = true;
            playerSprite.color = Color.grey;
        }
        else
        {
            LocalPlayer = true;
            Camera.main.GetComponent<CameraBehavior>().targetplayer = this;
            // GameUI.instance.Initialize(this);
        }

    }

    private void Update()
    {
        if (dead)
            return;

        Move();
        CheckForCameraBoundry();
        CheckToSpawnPlatforms();
        CheckFastFallExit();
    }

    void Move()
    {
        xInput = Input.GetAxis("Horizontal") * moveSpeed;

        if (rb.velocity.x > MaxSpeed)
            rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
        else if (rb.velocity.x < -MaxSpeed)
            rb.velocity = new Vector2(-MaxSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.S))
            StartFastFall();
    }

    void StartFastFall()
    {
        inFastFall = true;
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
    }

    void CheckFastFallExit()
    {
        if (inFastFall)
        {
            rb.gravityScale = fastFallSpeed;
        }
        else if (!inFastFall)
            rb.gravityScale = defaultFallSpeed;

        if (inFastFall && rb.velocity.y > 0)
            inFastFall = false;
    }

    void CheckForCameraBoundry()
    {
        float leftBoundry = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        float rightBoundry = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        float bottomBoundry = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;

        if (transform.position.x < leftBoundry - 1)
            transform.position = new Vector3(rightBoundry, transform.position.y, 0);
        else if (transform.position.x > rightBoundry + 1)
            transform.position = new Vector3(leftBoundry, transform.position.y, 0);

        if (transform.position.y < bottomBoundry - 10 && LocalPlayer)
            Die();
    }

    void CheckToSpawnPlatforms()
    {

        if (transform.position.y > GameManager.instance.platSpawnTrigger + Mathf.Log(GameManager.instance.platSpawnTrigger, GameManager.instance.platSpawnRate))
        {
            bool isHighestPlayer = true;
            foreach (PlayerBehavior x in GameManager.instance.players)
            {
                if (transform.position.y < x.gameObject.transform.position.y)
                    isHighestPlayer = false;
            }
            float thisTopOfScreen = Camera.main.ScreenToWorldPoint(new Vector2(0,Screen.height)).y;
            Debug.Log("Highest Top of Screen:" + thisTopOfScreen);
            if (isHighestPlayer)
            {
                GameManager.instance.photonView.RPC("UpdatePlatformTrigger", RpcTarget.All, thisTopOfScreen, id);
                GameManager.instance.SpawnPlatform(thisTopOfScreen);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(xInput, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, collision.GetComponent<Platform>().bounceForce), ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        // add end game functionality
        dead = true;
        GetComponent<BoxCollider2D>().enabled = false;

        photonView.RPC("TrackDeath", RpcTarget.MasterClient);
    }

    [PunRPC]
    void TrackDeath()
    {
        GameManager.instance.alivePlayers--;
        GameManager.instance.CheckWinCondition();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GameUI.instance.altitudeText);

        }
        else if (stream.IsReading)
        {
            GameUI.instance.altitudeText.text = (string)stream.ReceiveNext();
        }
    }
}



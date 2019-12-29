using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillScript : MonoBehaviour
{
    //variable declaration!
    public BoxCollider2D feet;
    public Procrastination_Script clockScript;
    public Player_Script playerScript;
    public Rigidbody2D playerRb;
    public bool enemyDying;

    // Start is called before the first frame update
    void Start()
    {   //setting variables in scene
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        feet = GameObject.Find("feetPos").GetComponent<BoxCollider2D>();
        clockScript = this.transform.parent.gameObject.GetComponent<Procrastination_Script>();
        playerScript = GameObject.Find("Player").GetComponent<Player_Script>();
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger == feet) //kill enemy when player jumps on it
        {
            enemyDying = true;
            clockScript.visible = false;
            clockScript.canMove = false;
            clockScript.touchedWall = false;
            this.transform.parent.gameObject.SetActive(false);
            playerRb.velocity = new Vector2(playerRb.velocity.x, 21f);
            playerScript.jumpUp = true;
            enemyDying = false;
            playerScript.enemiesKilled += 1;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !enemyDying) //kill player if they touch enemy
        {
            playerScript.Restart();
            playerScript.livesLeft -= 1;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") && clockScript != null) //change direction if enemy touches wall or other enemy
        {
            clockScript.touchedWall ^= true;
        }
    }

}

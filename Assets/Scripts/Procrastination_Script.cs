using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procrastination_Script : MonoBehaviour
{
    //variable declaration!
    public Player_Script playerScript;
    public Animator procrastinationAnim;
    public float spawnX;
    public float spawnY;
    public bool visible;
    public bool touchedWall;
    public bool canMove;
    public Transform enemies;
    public GameObject clock;
    public GameObject prefab;
    private GameObject clone;
    public Rigidbody2D rb;
    public BoxCollider2D cameraView;

    // Start is called before the first frame update
    void Start()
    {   //setting variables at beginning
        enemies = GameObject.Find("Enemies").GetComponent<Transform>();
        playerScript = GameObject.Find("Player").GetComponent<Player_Script>();
        cameraView = GameObject.Find("Camera View").GetComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        clock = this.transform.GetChild(0).gameObject;
        visible = false;
        canMove = false;
        touchedWall = false;
        rb.freezeRotation = true;
        if(prefab) //instantiating enemies
        {
            for (int i = 0; i < 8; i++)
            {
                if(i == 0)
                {
                    spawnX = 38;
                    spawnY = 9;
                }
                else if(i == 1)
                {
                    spawnX = 69;
                    spawnY = 2;
                }
                else if(i == 2)
                {
                    spawnX = 304;
                    spawnY = 2;
                }
                else if(i == 3)
                {
                    spawnX = 340.5f;
                    spawnY = 9.5f;
                }
                else if(i == 4)
                {
                    spawnX = 350;
                    spawnY = 2;
                }
                else if(i == 5)
                {
                    spawnX = 349;
                    spawnY = 15.5f;
                }
                else if(i == 6)
                {
                    spawnX = 340.5f;
                    spawnY = 24.5f;
                }
                else if(i == 7)
                {
                    spawnX = 386.3f;
                    spawnY = 2;
                }
                clone = Instantiate(prefab, new Vector3(spawnX, spawnY, 1), Quaternion.identity);
                clone.transform.parent = enemies;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        procrastinationAnim.SetBool("In Camera", canMove); //setting animator variable
        //turning around when enemy touches wall and standing still if they shouldn't move
        if (!touchedWall && canMove)
        {
            rb.velocity = new Vector2 (-5,0);
            clock.transform.localScale = new Vector3 (1,1,1);
        }
        else if (touchedWall && canMove)
        {
            rb.velocity = new Vector2 (5,0);
            clock.transform.localScale = new Vector3 (-1,1,1);
        }
        else if (!canMove)
        {
            rb.velocity = Vector2.zero;
        }
        //restart when R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerScript.Restart();
        }
    }

    //setting whether enemy is in camera and if it can move
    void OnTriggerEnter2D (Collider2D trigger)
    {
        if (trigger == cameraView)
        {
            visible = true;
            canMove = true;
        }
    }
    void OnTriggerExit2D (Collider2D trigger)
    {
        if (trigger == cameraView)
        {
            visible = false;
        }
    }
}

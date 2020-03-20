using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procrastination_Script : MonoBehaviour
{
    //variable declaration!
    public Player_Script playerScript;
    public Animator procrastinationAnim;
    public int numOfEnemies;
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
            if (playerScript.futureLevel != null && playerScript.futureLevel.activeSelf)
                FutureLevel();
            else if (playerScript.businessLevel != null && playerScript.businessLevel.activeSelf)
                BusinessLevel();
            else if (playerScript.leaderLevel != null && playerScript.leaderLevel.activeSelf)
                LeaderLevel();
            else if (playerScript.americaLevel != null && playerScript.americaLevel.activeSelf)
                AmericaLevel();
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

    public void FutureLevel()
    {
        Debug.Log("FUTURE LEVEL");
        numOfEnemies = 8;
        this.gameObject.SetActive(true);
        this.transform.localPosition = new Vector2(19, 2);
        for (int i = 1; i <= numOfEnemies; i++)
        {
            if (i == 1)
            {
                spawnX = 40;
                spawnY = 9;
            }
            else if (i == 2)
            {
                spawnX = 69;
                spawnY = 2;
            }
            else if (i == 3)
            {
                spawnX = 304;
                spawnY = 2;
            }
            else if (i == 4)
            {
                spawnX = 340.5f;
                spawnY = 9.5f;
            }
            else if (i == 5)
            {
                spawnX = 350;
                spawnY = 2;
            }
            else if (i == 6)
            {
                spawnX = 349;
                spawnY = 15.5f;
            }
            else if (i == 7)
            {
                spawnX = 340.5f;
                spawnY = 24.5f;
            }
            else if (i == 8)
            {
                spawnX = 386.3f;
                spawnY = 2;
            }
            clone = Instantiate(prefab, new Vector3(spawnX, spawnY, 1), Quaternion.identity);
            clone.transform.parent = enemies;
        }
    }
    public void BusinessLevel()
    {
        Debug.Log("BUSINESS LEVEL");
        numOfEnemies = 7;
        this.gameObject.SetActive(true);
        this.transform.localPosition = new Vector2(27f, 7);
        for (int i = 1; i <= numOfEnemies; i++)
        {
            if (i == 1)
            {
                spawnX = 47;
                spawnY = 6;
            }
            if (i == 2)
            {
                spawnX = 70;
                spawnY = 4;
            }
            if (i == 3)
            {
                spawnX = 92;
                spawnY = 2;
            }
            if (i == 4)
            {
                spawnX = 175;
                spawnY = 2;
            }
            if (i == 5)
            {
                spawnX = 205;
                spawnY = 2;
            }
            if (i == 6)
            {
                spawnX = 235;
                spawnY = 2;
            }
            if (i == 7)
            {
                spawnX = 330;
                spawnY = 15.5f;
            }
            clone = Instantiate(prefab, new Vector3(spawnX, spawnY, 1), Quaternion.identity);
            clone.transform.parent = enemies;
        }
    }
    public void LeaderLevel()
    {
        Debug.Log("LEADER LEVEL");
        numOfEnemies = 1;
        this.transform.localPosition = new Vector2(25, 5);
        for (int i = 1; i <= numOfEnemies; i++)
        {
            if (i == 1)
            {
                spawnX = 20;
                spawnY = 15;
            }
            clone = Instantiate(prefab, new Vector3(spawnX, spawnY, 1), Quaternion.identity);
            clone.transform.parent = enemies;
        }
    }
    public void AmericaLevel()
    {
        Debug.Log("AMERICA LEVEL");
        numOfEnemies = 2;
        this.transform.localPosition = new Vector2(27, 10);
        for (int i = 2; i <= numOfEnemies; i++)
        {
            if (i == 1)
            {
                spawnX = 40;
                spawnY = 9;
            }
            else if (i == 2)
            {
                spawnX = 60;
                spawnY = 15;
            }
            clone = Instantiate(prefab, new Vector3(spawnX, spawnY, 1), Quaternion.identity);
            clone.transform.parent = enemies;
        }
    }
}

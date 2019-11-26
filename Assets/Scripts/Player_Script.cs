using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
	//variable declaration!
	public Procrastination_Script clockScript;
	public Animator playerAnim;
	public Coroutine liftUp;
	public ParticleSystem confettiSystem;
	public int children;
	public float myGravityScale;
	public float speed;
	public float moveDirection;
	public float jumpForce;
	public float checkRadius;
    public float jumpTimeCounter;
    public float totalJumpTime;
	public bool isJumping;
	public bool canJump;
	public bool jumpUp;
	public bool jumpDown;
	public LayerMask ground;
	public LayerMask enemy;
	public Vector2 checkpoint;
	public GameObject player;
	public GameObject clock;
	public GameObject passedCheckpoint;
	public GameObject feetPos;
	public GameObject lift;
	public GameObject gate1;
	public GameObject gate2;
	public GameObject bookPlat;
	public GameObject bookPlat1;
	public GameObject bookPlat2;
	public GameObject bookPlat3;
	public Rigidbody2D rb;
	public Rigidbody2D gate1rb;
	public Rigidbody2D gate2rb;
	public Transform gate1Bottom;
	public Transform gate2Bottom;
	public Transform enemies;

	public GameObject flag;
	

    void Start()
    { 	//setting checkpoint and freezing player rotation
		//checkpoint = player.transform.position;
		checkpoint = new Vector2(415,15);
		rb.freezeRotation = true;
    }

    void FixedUpdate()
	{	//player moving
		moveDirection = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
	}

    public void StopJumpAnim()
	{	//function to stop jump animation
		isJumping = false;
	}

    void Update()
	{	//setting animator values
		playerAnim.SetFloat("Speed", rb.velocity.x);
		playerAnim.SetBool("Jumped", isJumping);
		playerAnim.SetBool("Up", jumpUp);
		playerAnim.SetBool("Down", jumpDown);

		if (rb.velocity.y < 0) //animator value for landing jump
		{
			jumpDown = true;
		}

			//setting up the two gates between level sections
		if (player.transform.position.x > gate1.transform.position.x && gate1.transform.position.y == 30)
		{
			gate1rb.gravityScale = 5.0f;
		}
		if (Physics2D.OverlapCircle(gate1Bottom.position, checkRadius, ground))
		{
			gate1rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		if (player.transform.position.x > gate2.transform.position.x && gate2.transform.position.y == 30)
		{
			gate2rb.gravityScale = 5.0f;
		}
		if (Physics2D.OverlapCircle(gate2Bottom.position, checkRadius, ground))
		{
			gate2rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}

		if (!Physics2D.OverlapCircle(feetPos.transform.position, checkRadius, ground))	//jumping
		{
			isJumping = true;
		}
		else
		{
			isJumping = false;
			jumpDown = false;
			jumpUp = false;
		}

        //short jump
        if (!isJumping && (Input.GetButtonDown("Jump")))
		{
            jumpTimeCounter = totalJumpTime;
			canJump = true;
			rb.velocity = new Vector2(rb.velocity.x, 10f);
			jumpUp = true;
			jumpDown = false;
		}
        //long jump
        if (canJump && (Input.GetButton("Jump")))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
				jumpUp = true;
				jumpDown = false;
            }
            else 
            {
				canJump = false;
				jumpDown = true;
				jumpUp = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            canJump = false;
			jumpUp = false;
			jumpDown = true;
        }

		if (Input.GetButton("Run") && !isJumping) //left shift to run
		{
			speed = 10f;
		}
		else if (Input.GetButtonUp("Run"))
		{
			speed = 5f;
		}

        if (moveDirection < 0) //player sprite changing direction
		{
			transform.localScale = new Vector2 (-.5f, .5f);
		}
        else if (moveDirection > 0)
		{
			transform.localScale = new Vector2(.5f, .5f);
		}

		if (transform.position.x == checkpoint.x) //enemies can't start moving immediately after player respawns
		{
        	clockScript.canMove = false;
        	clockScript.touchedWall = false;
            for (int i = 0; i < 7; i++)
			{
				clockScript.enemies.GetChild(i).GetComponent<Procrastination_Script>().canMove = false;
				clockScript.enemies.GetChild(i).GetComponent<Procrastination_Script>().touchedWall = false;
			}
		}
    }

    public void Restart() //kill player and set them at checkpoint, reset various objects
    {
        Debug.Log("restarting");
        transform.position = checkpoint;
        if (liftUp != null)
        {
            StopAllCoroutines();
            lift.transform.position = new Vector3(150, -1.85f, 0);
        }
		bookPlat.SetActive(true);
		bookPlat.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
		bookPlat1.SetActive(true);
		bookPlat1.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
		bookPlat2.SetActive(true);
		bookPlat2.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
		bookPlat3.SetActive(true);
		bookPlat3.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        clockScript.enemies.GetChild(0).position = new Vector2(17, 2);
		clockScript.enemies.GetChild(1).position = new Vector2(38, 9);
        clockScript.enemies.GetChild(2).position = new Vector2(70, 2);
        clockScript.enemies.GetChild(3).position = new Vector2(304, 2);
        clockScript.enemies.GetChild(4).position = new Vector2(340.5f, 9.5f);
        clockScript.enemies.GetChild(5).position = new Vector2(350, 2);
        clockScript.enemies.GetChild(6).position = new Vector2(349, 15.5f);
        clockScript.enemies.GetChild(7).position = new Vector2(340.5f, 24.5f);
        clockScript.enemies.GetChild(8).position = new Vector2(386.3f, 2);
        children = clockScript.enemies.childCount;
        for (int i = 0; i < children; i++)
        {
            clockScript.enemies.GetChild(i).gameObject.SetActive(true);
        }
        clockScript.canMove = false;
        clockScript.touchedWall = false;
    }

    void OnTriggerEnter2D(Collider2D trigger)
	{
        if (trigger.gameObject.tag == "Checkpoint") //setting checkpoints when passed
		{
			checkpoint = trigger.gameObject.transform.position;
			trigger.gameObject.SetActive(false);
			passedCheckpoint.SetActive(true);
			passedCheckpoint.transform.position = trigger.gameObject.transform.position;
		}
		else if (trigger.gameObject.CompareTag("Finish"))
		{
			EndLevel();
			
		}
	}

	public void EndLevel()
	{
		confettiSystem.Play();
		//end level function (stop score counting down, add score and bonuses, menu)
	}

	void OnTriggerStay2D (Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Slope") //ability to slide
		{
			if(Input.GetAxisRaw("Vertical") < 0)
			{
				rb.gravityScale = 50;
				//slide animation?
			}
			if(Input.GetAxisRaw("Vertical") >= 0)
			{
				rb.gravityScale = 2;
			}
		}
	}

	void OnTriggerExit2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Slope") //finishing slide when not on a slope
		{
			rb.gravityScale = myGravityScale;
		}
	}

	void OnCollisionEnter2D(Collision2D other) //move lift up when the player gets on it
	{
		if(other.gameObject == lift)
		{
			liftUp = StartCoroutine(MoveLift());
		}
	}

	IEnumerator MoveLift() //coroutine to start lift movement
	{
    	float startTime = Time.time;
    	while(Time.time < startTime + 60f)
    	{
        	lift.transform.position = Vector2.Lerp(lift.transform.position, new Vector2(150, 25), (Time.time - startTime)/60f);
        	yield return null;
    	}
    	lift.transform.position = new Vector3(150, 25, 0);
	}
}
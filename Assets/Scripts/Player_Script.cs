using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player_Script : MonoBehaviour
{
	//variable declaration!
	public Procrastination_Script clockScript;
	public Score_Script scoreScript;
	public EnemyKillScript enemyKillScript;
	public Animator playerAnim;
	public Coroutine liftUp;
	public ParticleSystem confettiSystem;
	public ParticleSystem endScoreParts;
	public ParticleSystem totalScoreSystem;
	public int children;
	public float myGravityScale;
	public float speed;
	public float moveDirection;
	public float jumpForce;
	public float checkRadius;
    public float jumpTimeCounter;
    public float totalJumpTime;
	public float livesLeft = 5;
	public float enemiesKilled = 0;
	public float enemiesKilledCheckpoint;
	public bool isJumping;
	public bool canJump;
	public bool jumpUp;
	public bool jumpDown;
	public bool gameOver;
	public LayerMask ground;
	public LayerMask enemy;
	public Vector2 checkpoint;
	public Vector2 startPoint;
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
	public GameObject levelEndPanel;
	public RigidbodyConstraints2D oGGate1;
	public RigidbodyConstraints2D oGGate2;
	public Rigidbody2D rb;
	public Rigidbody2D gate1rb;
	public Rigidbody2D gate2rb;
	public Transform gate1Bottom;
	public Transform gate2Bottom;
	public Transform enemies;
	public TextMeshProUGUI enemiesKilledScore;
	public TextMeshProUGUI enemiesKilledEquation;
	public TextMeshProUGUI livesLeftScore;
	public TextMeshProUGUI livesLeftEquation;
	public TextMeshProUGUI totalScore;


	public GameObject flag;
	

    void Start()
    { 	//setting checkpoint and freezing player rotation
		levelEndPanel.SetActive(false);
		startPoint = player.transform.position;
		//checkpoint = startPoint;
		checkpoint = new Vector2(415,15);
		rb.freezeRotation = true;
		gameOver = false;
		enemiesKilledCheckpoint = enemiesKilled;
		oGGate1 = gate1rb.constraints;
		oGGate2 = gate2rb.constraints;
		
    }

    void FixedUpdate()
	{	//player moving
		if (!gameOver)
		{
			moveDirection = Input.GetAxisRaw("Horizontal");
			rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
		}	
		else
		{
			moveDirection = 0;
			rb.velocity = Vector2.zero;
		}
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

        if (!gameOver)
		{
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
    }

    public void Restart() //kill player and set them at checkpoint, reset various objects
    {
		if (!gameOver)
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
        clockScript.enemies.GetChild(0).position = new Vector2(19, 2);
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
		enemiesKilled = enemiesKilledCheckpoint;
		}
    }

    void OnTriggerEnter2D(Collider2D trigger)
	{
        if (trigger.gameObject.tag == "Checkpoint") //setting checkpoints when passed
		{
			checkpoint = trigger.gameObject.transform.position;
			trigger.gameObject.SetActive(false);
			passedCheckpoint.SetActive(true);
			passedCheckpoint.transform.position = trigger.gameObject.transform.position;
			enemiesKilledCheckpoint = enemiesKilled;
		}
		else if (trigger.gameObject.CompareTag("Finish"))
		{
			StartCoroutine("EndLevel");
		}
	}

	public IEnumerator EndLevel() //show score and end of level screen
	{
		gameOver = true;
		confettiSystem.Play();
		levelEndPanel.SetActive(true);
		enemiesKilledEquation.text = enemiesKilled.ToString() + "     x 100";
		livesLeftEquation.text = livesLeft.ToString() + "     x 100";
		endScoreParts.Play(); //score from time left
		yield return new WaitForSeconds(1);
		scoreScript.timeLeftScore.text = scoreScript.timer.ToString("F0");
		yield return new WaitForSeconds(.75f);
		endScoreParts.transform.localPosition = new Vector2(endScoreParts.transform.localPosition.x, 50); //score from enemies killed
		endScoreParts.Play(); 
		yield return new WaitForSeconds(1);
		enemiesKilledScore.text = (enemiesKilled * 100).ToString();
		yield return new WaitForSeconds(.75f);
		endScoreParts.transform.localPosition = new Vector2(endScoreParts.transform.localPosition.x, -50); //score from lives left
		endScoreParts.Play(); 
		yield return new WaitForSeconds(1);
		livesLeftScore.text = (livesLeft * 100).ToString();
		yield return new WaitForSeconds(1.5f);
		totalScoreSystem.Play();
		yield return new WaitForSeconds(1);
		totalScore.text = (scoreScript.timer + (enemiesKilled * 100) + (livesLeft * 100)).ToString("F0");
		//end level function (add score and bonuses, menu)
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

	public void ReplayLevel() //reset level and replay it
    {
		levelEndPanel.SetActive(false);
		checkpoint = startPoint;
		gameOver = false;
		livesLeft = 5;
		enemiesKilled = 0;
		enemiesKilledCheckpoint = enemiesKilled;
		for (int i = 0; i < enemyKillScript.lives.childCount; i++)
			enemyKillScript.lives.GetChild(i).gameObject.SetActive(true);
		scoreScript.timer = 300.0f;
		gate1.transform.position = new Vector2(gate1.transform.position.x, 30);
		gate1rb.gravityScale = 0;
		gate1rb.constraints = oGGate1;
		gate2.transform.position = new Vector2(gate2.transform.position.x, 30);
		gate2rb.gravityScale = 0;
		gate2rb.constraints = oGGate2;
		confettiSystem.Stop();
		scoreScript.timeLeftScore.text = "";
		enemiesKilledScore.text = "";
		livesLeftScore.text = "";
		totalScore.text = "";
		Restart();
	}

	public void GoToLevels() //go to level select
    {
		SceneManager.LoadScene("Level Select");
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
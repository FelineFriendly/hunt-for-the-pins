using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_Script : MonoBehaviour
{

    public bool touchedByPlayer;
    public bool startWaitTime;
    public float waitTime;
    public GameObject player;
    public Animator bookAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bookAnim = gameObject.GetComponent<Animator>();
        waitTime = 1.0f;
        touchedByPlayer = false;
        startWaitTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        bookAnim.SetBool("Touched By Player", touchedByPlayer);
        if (startWaitTime)
        {
            waitTime -= Time.deltaTime;
        }
    }

    public void AnimEnd()
    {
        /*startWaitTime = true;
        if (waitTime <= 0)
        {*/
            gameObject.SetActive(false);
            touchedByPlayer = false;
        /*    waitTime = 1;
            startWaitTime = false;
        }*/
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            touchedByPlayer = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            touchedByPlayer = false;
        }
    }
}

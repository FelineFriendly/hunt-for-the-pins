using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Script : MonoBehaviour
{
    public float timer = 300.0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timeLeftScore;
    public Player_Script playerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<Player_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.gameOver)
        {
            timer -= Time.deltaTime;
            timerText.text = "Score: " + timer.ToString("F0");
        }   
        else
        {
            timeLeftScore.text = timer.ToString("F0");
        }
    }
}

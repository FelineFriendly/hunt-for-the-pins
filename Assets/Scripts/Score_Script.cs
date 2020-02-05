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
        Destroy(GameObject.Find("Level Controller"));
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.gameOver)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F0");
        }
    }
}

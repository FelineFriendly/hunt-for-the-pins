using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_Script : MonoBehaviour
{
    public float score = 300.0f;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score -= Time.deltaTime;
        scoreText.text = "Score: " + score.ToString("F0");
    }
}

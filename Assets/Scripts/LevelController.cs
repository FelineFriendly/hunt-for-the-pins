using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int level;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        level = 0;
    }
    private void Start()
    {
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            switch (level)
            {
                case 1:
                    GameObject.FindWithTag("Future").SetActive(true);
                    GameObject.FindWithTag("Business").SetActive(false);
                    GameObject.FindWithTag("Leader").SetActive(false);
                    GameObject.FindWithTag("America").SetActive(false);
                    Debug.Log("Future");
                    break;
                case 2:
                    GameObject.FindWithTag("Future").SetActive(false);
                    GameObject.FindWithTag("Business").SetActive(true);
                    GameObject.FindWithTag("Leader").SetActive(false);
                    GameObject.FindWithTag("America").SetActive(false);
                    Debug.Log("Business");
                    break;
                case 3:
                    GameObject.FindWithTag("Future").SetActive(false);
                    GameObject.FindWithTag("Business").SetActive(false);
                    GameObject.FindWithTag("Leader").SetActive(true);
                    GameObject.FindWithTag("America").SetActive(false);
                    Debug.Log("Leader");
                    break;
                case 4:
                    GameObject.FindWithTag("Future").SetActive(false);
                    GameObject.FindWithTag("Business").SetActive(false);
                    GameObject.FindWithTag("Leader").SetActive(false);
                    GameObject.FindWithTag("America").SetActive(true);
                    Debug.Log("America");
                    break;
                default:
                    break;
            }
        }

        if (scene.name == "Level Select")
        {
            level = 0;
            Destroy(this);
        }
    }

    public void PlayLevel(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                level = 1;
                break;
            case 2:
                level = 2;
                break;
            case 3:
                level = 3;
                break;
            case 4:
                level = 4;
                break;
            default:
                break;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Game");
    }
}

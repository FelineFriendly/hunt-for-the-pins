using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mute(bool muted) //mute in-game music
    {
        if (muted)
        {
            music.Stop();
            muted = false;
        }
        else if (!muted)
        {
            music.Play();
            muted = true;
        }
    }

    public void Volume(float value)
    {
        music.volume = value;
    }
}

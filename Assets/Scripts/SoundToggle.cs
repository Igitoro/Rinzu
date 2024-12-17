using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class MusicVolume : MonoBehaviour
{
    public Sprite onMusic;
    public Sprite offMusic;

    public Image MusicButton;
    public bool isOn;
    public AudioSource ad;

    private void Start()
    {
        isOn = false;
    }

    public void Update()
    {
        if(PlayerPrefs.GetInt("music") == 0)
        {
            MusicButton.GetComponent<Image>().sprite = onMusic;
            ad.mute = true;
            isOn = true;
        }
        else if(PlayerPrefs.GetInt("music") == 1)
        {
            MusicButton.GetComponent<Image>().sprite = offMusic;
            ad.mute = false;
            isOn = false;
        }
    }

    public void OffSound()
    {
        if(!isOn)
        {
            PlayerPrefs.SetInt("music", 0);

        } else if (isOn)
        {
            PlayerPrefs.SetInt("music", 1);
        }
    }
}
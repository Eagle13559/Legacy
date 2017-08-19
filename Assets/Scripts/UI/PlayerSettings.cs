using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class PlayerSettings : MonoBehaviour
{
    
    public Slider masterVol;
    public Slider musicVol;
    public Slider sfxVol;

    public AudioMixer masterMixer;

    MixLevels mixLevels = new MixLevels();


    void Start()
    {
        // load saved volumes
        masterVol.value = PlayerPrefs.GetFloat("masterVol");

        musicVol.value = PlayerPrefs.GetFloat("musicVol");

        sfxVol.value = PlayerPrefs.GetFloat("sfxVol");
    }



    // saves volumes. called by slider on change. 
    public void SaveMaster()
    {
        Debug.Log("Save Master");
        PlayerPrefs.SetFloat("masterVol", masterVol.value);

        PlayerPrefs.Save();
    }

    public void SaveMusic() {

        Debug.Log("SaveMusic");

        PlayerPrefs.SetFloat("musicVol", musicVol.value);

        PlayerPrefs.Save();
    }

    public void SaveSFX() {

        Debug.Log("Save SFX");

       PlayerPrefs.SetFloat("sfxVol", sfxVol.value);


        PlayerPrefs.Save();

    }
    
}


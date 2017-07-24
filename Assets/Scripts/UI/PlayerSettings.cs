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

    void Start()
    {
        masterVol.value = PlayerPrefs.GetFloat("masterVol", 1); // load vol and make default 1
        musicVol.value = PlayerPrefs.GetFloat("musicVol", 1);
        sfxVol.value = PlayerPrefs.GetFloat("sfxVol", 1);

    }

    private void Update()
    {
        masterMixer.SetFloat("masterVol", masterVol.value);
    }



    public void SaveSets()
    {

        PlayerPrefs.SetFloat("masterVol", masterVol.value);
       // PlayerPrefs.SetFloat("musicVol", musicVol.value);
       // PlayerPrefs.SetFloat("sfxVol", sfxVol.value);
        Debug.Log("Master Volume set");
    }

}


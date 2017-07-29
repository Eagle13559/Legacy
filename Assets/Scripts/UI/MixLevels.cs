using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixLevels : MonoBehaviour {

    public AudioMixer masterMixer;
    public Slider masterSlider;
    public Toggle masterTog;

    public void SetMasterLvL (float masterLvl)
    {
        masterMixer.SetFloat("masterVol", masterLvl);  
    }

    public void SetSfxLvl (float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);
        masterMixer.SetFloat("menuSfxVol", sfxLvl);
    }

    public void SetMusicLvl (float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
    }

    public void MuteMaster ()
    {
        if (masterTog.isOn == true)
        {
            masterMixer.SetFloat("masterVol", -60f);
            Debug.Log("mute");
        }

        else
        {
            Debug.Log(masterSlider.value);
            masterMixer.SetFloat("masterVol", masterSlider.value);
            Debug.Log("unmute");
        }
        
       }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

    public AudioMixer masterMixer;

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
}

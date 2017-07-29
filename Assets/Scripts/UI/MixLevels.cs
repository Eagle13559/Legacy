using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixLevels : MonoBehaviour {

// audio mixer
    public AudioMixer masterMixer;


// sliders
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

// toggles
    public Toggle masterTog;
    public Toggle musicTog;
    public Toggle sfxTog;
    
// changes master/main audio with slider
    public void SetMasterLvL (float masterLvl)
    {
        masterMixer.SetFloat("masterVol", masterLvl);

//checks if low enough to be mute and turns toggle on/off if so
        if (masterSlider.value == -80 && masterTog.isOn == false)
        {
            masterTog.isOn = true;
        }

        else
        {
            masterTog.isOn = false;
        }
    }

    // same as above but music
    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);

        if (musicSlider.value == -80 && musicTog.isOn == false)
        {
            musicTog.isOn = true;
        }

        else
        {
            musicTog.isOn = false;
        }
    }

    // same as above but for sound effects. menusfx seperate from sfx due to digetic sound being muffled in menu
    public void SetSfxLvl (float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);
        masterMixer.SetFloat("menuSfxVol", sfxLvl);

        if (sfxSlider.value == 0 && sfxTog.isOn == false)
        {
            sfxTog.isOn = true;
        }

        else
        {
            sfxTog.isOn = false;
        }
    }


// individual muting for the three sliders
    public void MuteMaster ()
    {
        if (masterTog.isOn == true)
        {
            masterMixer.SetFloat("masterVol", -80f);
        }

        else
        {
            masterMixer.SetFloat("masterVol", masterSlider.value);

            if (masterSlider.value == -80)
            {
                masterSlider.value = -50;
            } 
        }
        
       }

    public void MuteMusic()
    {
        if (musicTog.isOn == true)
        {
            masterMixer.SetFloat("musicVol", -80f);
        }

        else
        {
            masterMixer.SetFloat("musicVol", musicSlider.value);

            if (musicSlider.value == -80)
            {
                musicSlider.value = -50;
            }
        }

    }


     public void MuteSFX()
        {
            if (sfxTog.isOn == true)
            {
                masterMixer.SetFloat("sfxVol", 0f);
                masterMixer.SetFloat("menuSfxVol", 0f);
            }

            else
            {
                masterMixer.SetFloat("sfxVol", sfxSlider.value);
                masterMixer.SetFloat("menuSfxVol", sfxSlider.value);


                if (sfxSlider.value == -80)
                {
                    sfxSlider.value = -50;
                }
            }


        }


    }




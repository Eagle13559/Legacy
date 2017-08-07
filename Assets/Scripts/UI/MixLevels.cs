using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixLevels : MonoBehaviour {

// gameobjects for inspector

    public AudioMixer masterMixer;


    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;


    public Toggle masterTog;
    public Toggle musicTog;
    public Toggle sfxTog;

 // called by sliders for value changes
    public float getsoundLvl(float soundLvl)
    {
        if (soundLvl == 0)
        {
            return -80;
        }

        else
        {
            if (soundLvl == 1)
            {
                return -70;
            }

            else
            {
                if (soundLvl == 2)
                {
                    return -60;
                }

                else
                {
                    if (soundLvl == 3)
                    {
                        return -50;
                    }
                    
                    else
                    {
                        if (soundLvl == 4)
                        {
                            return -40;
                        }

                        else
                        {
                            if (soundLvl == 5)
                            {
                                return -30;
                            }

                            else
                            {
                                if (soundLvl == 6)
                                {
                                    return -20;
                                }

                                else
                                {
                                    if (soundLvl == 7)
                                    {
                                        return -10;
                                    }

                                    else
                                    {
                                        if (soundLvl == 8)
                                        {
                                            return 0;
                                        }

                                        else
                                        {
                                            if (soundLvl == 9)
                                            {
                                                return 10;
                                            }

                                            else
                                            {
                                                return 20;
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }
                }
            }
        }


    }

    //  slider audio changers for 3 categories. 
    #region setting audio
    public void SetMasterLvL (float masterLvl)
    {
        // calls soundLvl to convert slider #s to correspdoning db #s
        float soundLvl = getsoundLvl(masterLvl);

        masterMixer.SetFloat("masterVol", soundLvl);

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


    public void SetMusicLvl(float musicLvl)
    {
        float soundLvl = getsoundLvl(musicLvl);

        masterMixer.SetFloat("musicVol", soundLvl);

        if (musicSlider.value == -80 && musicTog.isOn == false)
        {
            musicTog.isOn = true;
        }

        else
        {
            musicTog.isOn = false;
        }
    }

    public void SetSfxLvl (float sfxLvl)
    {
        float soundLvl = getsoundLvl(sfxLvl); 
        masterMixer.SetFloat("sfxVol", soundLvl);
        masterMixer.SetFloat("menuSfxVol", soundLvl);

        if (sfxSlider.value == 0 && sfxTog.isOn == false)
        {
            sfxTog.isOn = true;
        }

        else
        {
            sfxTog.isOn = false;
        }
    }
    #endregion



    //   individual muting for the three sliders
    #region muting
    public void MuteMaster()
    {
        if (masterTog.isOn == true)
        {
            masterMixer.SetFloat("masterVol", -80f);
        }

        else
        {
            float soundLvl = getsoundLvl(masterSlider.value);
            masterMixer.SetFloat("masterVol", soundLvl);

            if (masterSlider.value == 0)
            {
                masterSlider.value = 1;
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
            float soundLvl = getsoundLvl(musicSlider.value);
            masterMixer.SetFloat("musicVol", soundLvl);

            if (musicSlider.value == 0)
            {
                musicSlider.value = 1;
            }
        }

    }


     public void MuteSFX()
        {
            if (sfxTog.isOn == true)
            {
                masterMixer.SetFloat("sfxVol", -80f);
                masterMixer.SetFloat("menuSfxVol", -80f);
            }

            else
            {
                float soundLvl = getsoundLvl(sfxSlider.value);
                masterMixer.SetFloat("sfxVol", soundLvl);
                masterMixer.SetFloat("menuSfxVol", soundLvl);


                if (sfxSlider.value == 0)
                {
                    sfxSlider.value = 1;
                }
            }


        }

    #endregion


}




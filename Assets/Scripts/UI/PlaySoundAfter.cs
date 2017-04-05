using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaySoundAfter : MonoBehaviour {

    public AudioClip ClickSource;
    AudioSource audio;

    private Slider _slider;

    void Start()
    {
        _slider = this.GetComponent<Slider>();
        audio = this.GetComponent<AudioSource>();
    } 

    public void PlayVolumeCheck()
    {
        Vector3 temp = new Vector3(9f, 5f);
        Debug.Log("ping");
        audio.PlayOneShot(ClickSource, _slider.value);
    }

}

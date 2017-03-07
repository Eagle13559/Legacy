using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBrain : MonoBehaviour
{
    private GameObject _brainSingleton = null;
    void Start ()
    {
        if (_brainSingleton == null)
            _brainSingleton = this.gameObject;
        else
        {
            if (_brainSingleton != this.gameObject)
                Destroy(this.gameObject);
        }

        TotalTime = Time;

        DontDestroyOnLoad(transform.gameObject);
    }

    /// <summary>
    /// Keeps track of the players money from instance to instance.
    /// </summary>
    public long PlayersMoney;

    /// <summary>
    /// Keeps track of overall time from instance to instance. 
    /// </summary>
    public float Time;

    private float TotalTime;

    /// <summary>
    /// Keeps track of the index that will be loaded on the next load level call
    /// </summary>
    public int nextSceneIndex;

    public void resetTime()
    {
        Time = TotalTime;
    }

    /// <summary>
    /// Type of Items available to the player
    /// </summary>
    public enum ItemTypes { Dash, Bomb, None }
}

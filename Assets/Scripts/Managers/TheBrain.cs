using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBrain : MonoBehaviour
{
    void Start ()
    {
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

    /// <summary>
    /// Keeps track of the index that will be loaded on the next load level call
    /// </summary>
    public int nextSceneIndex;
}

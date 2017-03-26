using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBrain : MonoBehaviour
{
    private GameObject _brainSingleton = null;
    public bool SingletonCreated
    {
        get
        {
            return _brainSingleton == null;
        }
    }

    public TheBrain()
    {
        Time = float.PositiveInfinity;
        PlayersMoney = 100;
        InitalizePlayerItemCount();
        currIncense = IncenseTypes.Base;
    }

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

        InitalizePlayerItemCount();
    }

    public void InitalizePlayerItemCount ()
    {
        foreach (ItemTypes item in Enum.GetValues(typeof(ItemTypes)))
        {
            playerItemCounts[item] = 0;
        }
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

    /// <summary>
    /// 
    /// </summary>
    public enum IncenseTypes { Base, SlowByHalf, SlowByThird, SlowByFourth };

    public IncenseTypes currIncense;

    /// <summary>
    /// Keeps track of players item cache
    /// </summary>
    public Dictionary<ItemTypes, int> playerItemCounts = new Dictionary<ItemTypes, int>();

    /// <summary>
    /// Contains all the incense images to use in UI.
    /// </summary>
    public Sprite[] IncenseSprites = new Sprite[4];
}

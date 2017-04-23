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
            return _brainSingleton != null && _brainSingleton == this.gameObject;
        }
    }

    public TheBrain()
    {
        Time = float.PositiveInfinity;
        PlayersMoney = 100;
        InitalizePlayerItemCount();
        InitalizePlayerIncenseCount();
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
        InitalizePlayerIncenseCount();
    }

    public void InitalizePlayerItemCount ()
    {
        foreach (ItemTypes item in Enum.GetValues(typeof(ItemTypes)))
        {
            playerItemCounts[item] = 0;
        }
    }

    public void InitalizePlayerIncenseCount()
    {
        foreach (IncenseTypes item in Enum.GetValues(typeof(IncenseTypes)))
        {
            playerIncenseCounts[item] = 0;
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

    public float TotalTime { get; private set; }

    /// <summary>
    /// Keeps track of the index that will be loaded on the next load level call
    /// </summary>
    public string nextSceneIndex;

    public void resetTime()
    {
        Time = TotalTime;
    }

    /// <summary>
    /// Type of Items available to the player
    /// </summary>
    public enum ItemTypes { Dash, Bomb, Invincible, None }

    /// <summary>
    /// Represents the incense types the player can have. 
    /// </summary>
    public enum IncenseTypes { Base, Black, Purple, Red, None };

    public IncenseTypes currIncense;

    /// <summary>
    /// Will change the incense type if the count is greater than 0
    /// </summary>
    /// <returns></returns>
    public void SetNextAvailableIncense()
    {
        for (IncenseTypes i = currIncense + 1; i < IncenseTypes.None; i++)
        {
            if (playerIncenseCounts[i] > 0)
            {
                currIncense = i;
                return;
            }
        }

        currIncense = IncenseTypes.Base;
    }

    /// <summary>
    /// Keeps track of players item cache
    /// </summary>
    public Dictionary<ItemTypes, int> playerItemCounts = new Dictionary<ItemTypes, int>();

    /// <summary>
    /// Keeps track of players item cache
    /// </summary>
    public Dictionary<IncenseTypes, int> playerIncenseCounts = new Dictionary<IncenseTypes, int>();

    /// <summary>
    /// Contains all the incense images to use in UI.
    /// </summary>
    public Sprite[] IncenseSprites = new Sprite[4];

}

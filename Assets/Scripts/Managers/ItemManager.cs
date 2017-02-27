using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    /// <summary>
    /// Starting Inventory of this item
    /// </summary>
    [SerializeField]
    private int StartingInventory;

    /// <summary>
    /// What the current inventory of this item is. 
    /// </summary>
    public int CurrInventory
    {
        get; private set;
    }

    /// <summary>
    /// Total amount that this item will cost the player
    /// </summary>
    [SerializeField]
    private long TotalCost;

    /// <summary>
    /// Current cost of this item for the player. Able to go down if player has earned discount. 
    /// </summary>
    public long CurrCost
    {
        get; private set;
    }

    /// <summary>
    /// ItemType as determined by the The Brain's listings. 
    /// </summary>
    public TheBrain.ItemTypes ItemTag;

    // Use this for initialization
    void Start () {
        CurrInventory = StartingInventory;
        CurrCost = TotalCost;
	}

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// If the given the correct amount 
    /// </summary>
    /// <param name="paidAmount"></param>
    /// <returns></returns>
    public bool TryToPurchase (long paidAmount)
    {
        if (paidAmount == CurrCost && CurrInventory > 0)
        {
            CurrInventory--;
            return true;
        }

        return false;
    }
}

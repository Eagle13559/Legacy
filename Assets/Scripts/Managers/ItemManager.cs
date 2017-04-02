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
    /// 
    /// </summary>
    [SerializeField]
    private TextBoxManager infoBox;

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

    public delegate void ItemPurchase(TheBrain.ItemTypes tag);
    public static event ItemPurchase OnClick; 

    // Use this for initialization
    void Start () {
        CurrInventory = StartingInventory;
        CurrCost = TotalCost;
	}

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// Shows the player info for the item they are trying to get. 
    /// </summary>
    public void GetInfo ()
    {
        infoBox.EnableTextBox();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        infoBox.EnableTextBox();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        infoBox.DisableTextBox();
    }

    public void PurchasingItem()
    {
        OnClick(this.ItemTag);
    }
}

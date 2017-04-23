using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : MonoBehaviour {

    public Dictionary<TheBrain.ItemTypes, int> itemCart { get; private set;  }

    public Dictionary<TheBrain.IncenseTypes, int> incenseCart { get; private set; }

    private long size;

    public ShoppingCart ()
    {
        itemCart = new Dictionary<TheBrain.ItemTypes, int>();
        incenseCart = new Dictionary<TheBrain.IncenseTypes, int>();

        foreach (TheBrain.ItemTypes item in Enum.GetValues(typeof(TheBrain.ItemTypes)))
        {
            itemCart[item] = 0;
        }

        foreach (TheBrain.IncenseTypes item in Enum.GetValues(typeof(TheBrain.IncenseTypes)))
        {
            incenseCart[item] = 0;
        }

        size = 0;
    }
    
    /// <summary>
    /// Adds a single instance of the item type given to the shopping cart
    /// </summary>
    /// <param name="type"></param>
    public void addItemToCart (TheBrain.ItemTypes type)
    {
        itemCart[type]++;
        size++;
    }

    /// <summary>
    /// Adds a single instance of the incense typoe given to the shopping cart. 
    /// </summary>
    /// <param name="type"></param>
    public void addIncenseToCart (TheBrain.IncenseTypes type)
    {
        incenseCart[type]++;
        size++;
    }

    /// <summary>
    /// Enumerates over list of items
    /// </summary>
    /// <returns></returns>
    public IEnumerable GetNumOfItems ()
    {
        
        foreach (TheBrain.ItemTypes item in Enum.GetValues(typeof(TheBrain.ItemTypes)))
        {
            yield return new ItemContainer(item, itemCart[item]);
        }
    }


    /// <summary>
    /// Enumerates over list of incense
    /// </summary>
    /// <returns></returns>
    public IEnumerable GetNumOfIncense()
    {

        foreach (TheBrain.IncenseTypes item in Enum.GetValues(typeof(TheBrain.IncenseTypes)))
        {
            yield return new IncenseContainer(item, incenseCart[item]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClearCart ()
    {
        foreach (TheBrain.ItemTypes item in Enum.GetValues(typeof(TheBrain.ItemTypes)))
        {
            itemCart[item] = 0;
        }

        foreach (TheBrain.IncenseTypes item in Enum.GetValues(typeof(TheBrain.IncenseTypes)))
        {
            incenseCart[item] = 0;
        }
    }

    public long GetNumOfSpecificItem (TheBrain.ItemTypes type)
    {
        return itemCart[type];
    }

    public long GetNumOfSpecificIncense(TheBrain.IncenseTypes type)
    {
        return incenseCart[type];
    }

    public long GetTotalSizeOfCart()
    {
        return size;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : MonoBehaviour {

    public Dictionary<TheBrain.ItemTypes, int> cart { get; private set;  }

	public ShoppingCart ()
    {
        cart = new Dictionary<TheBrain.ItemTypes, int>();

        foreach (TheBrain.ItemTypes item in Enum.GetValues(typeof(TheBrain.ItemTypes)))
        {
            cart[item] = 0;
        }
    }
    
    /// <summary>
    /// Adds a single instance of the item type given to the shopping cart
    /// </summary>
    /// <param name="type"></param>
    public void addItemToCart (TheBrain.ItemTypes type)
    {
        cart[type]++;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable GetNumOfItems ()
    {
        
        foreach (TheBrain.ItemTypes item in Enum.GetValues(typeof(TheBrain.ItemTypes)))
        {
            yield return new ItemContainer(item, cart[item]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClearCart ()
    {
        foreach (TheBrain.ItemTypes item in Enum.GetValues(typeof(TheBrain.ItemTypes)))
        {
            cart[item] = 0;
        }
    }

    public long GetNumOfSpecificItem (TheBrain.ItemTypes type)
    {
        return cart[type];
    }
}

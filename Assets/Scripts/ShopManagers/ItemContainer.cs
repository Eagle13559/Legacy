using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemContainer {
    public TheBrain.ItemTypes itemType { get; private set; }
    public int numOfItems { get; private set; }

    public ItemContainer (TheBrain.ItemTypes type, int num)
    {
        itemType = type;
        numOfItems = num;
    }
}

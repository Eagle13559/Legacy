using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IncenseContainer {

    public TheBrain.IncenseTypes itemType { get; private set; }
    public int numOfItems { get; private set; }

    public IncenseContainer(TheBrain.IncenseTypes type, int num)
    {
        itemType = type;
        numOfItems = num;
    }
}

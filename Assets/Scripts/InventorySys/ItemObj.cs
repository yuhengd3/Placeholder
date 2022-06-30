using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
    Food,
    Equipment,
    Default
}

public class ItemObj : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)] 
    public string description;

    public void Do() {
        return; //placeholder
    }
}

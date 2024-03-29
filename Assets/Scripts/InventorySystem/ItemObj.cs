using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
    HealthPortion,
    Default
}

public class ItemObj : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)] 
    public string description;

    public virtual void Do(GameObject player){
        return;
    }

}

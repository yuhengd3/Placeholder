using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObj : ItemObj
{
    public int restoreValue;
    //called when the script instance is being loaded
    public void Awake()
    {
        type = ItemType.Food;
    }
}

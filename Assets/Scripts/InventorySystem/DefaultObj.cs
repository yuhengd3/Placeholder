using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultObj : ItemObj
{
    //called when the script instance is being loaded
    public void Awake() {
        type = ItemType.Default;
    }
}

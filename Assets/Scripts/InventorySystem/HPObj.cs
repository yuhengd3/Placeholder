using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Portion Object", menuName = "Inventory System/Items/Portion")]
public class HPObj : ItemObj
{
    public int restoreValue;
    //called when the script instance is being loaded
    public void Awake()
    {
        type = ItemType.HealthPortion;
    }

    public override void Do(GameObject player){
        player.GetComponent<HealthController>().IncreaseHealth(restoreValue);
    }
}

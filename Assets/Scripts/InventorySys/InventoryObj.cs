using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public bool AddItem(ItemObj _item, int _amount) {

        bool hasItem = false;

        foreach (InventorySlot each in Container) {
            if (each.item.type == _item.type) hasItem = true;
        }

        if (Container.Count > 0 && !hasItem) return false; //disable holding multiple items

        for (int i = 0; i < Container.Count; i++) {
            if (Container[i].item == _item) {
                Container[i].AddAmount(_amount);
                break;
            }
        }

        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }

        return true;
    }

    public void UseObj()
    {
        if (Container.Count == 0) return;
        Container[0].item.Do();
        Container[0].amount -= 1;
    }

    public void DiscardObj() {
        if (Container.Count == 0) return;
        Container[0].amount = 0;
    }

}

[System.Serializable]
public class InventorySlot {
    public ItemObj item;
    public int amount;
    public InventorySlot(ItemObj _item, int _amount) { item = _item; amount = _amount; }
    public void AddAmount(int value) { amount += value; }
}
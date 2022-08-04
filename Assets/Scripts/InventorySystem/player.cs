using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public InventoryObj inv;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other) {
        var item = other.GetComponent<Item>();
        if (item) {
            Debug.Log("Hit");
            inv.AddItem(item.item, 1); //what is item
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit() {
        inv.Container.Clear();
    }

    private void useItem(){
        if (inv.Container.Count == 0){ 
            return;
        }
        
        inv.Container[0].item.Do(this.gameObject);
        inv.Container[0].amount -= 1;

    }

    private void Update(){
        if(Input.GetKeyDown("f")){
            this.useItem();
        }
    }
}

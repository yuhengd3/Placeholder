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
            if (inv.AddItem(item.item, 1)) //what is item
                Destroy(other.transform.parent.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) this.inv.UseObj();
        if (Input.GetKeyDown(KeyCode.Q)) this.inv.DiscardObj();


        if (Input.GetKey(KeyCode.W)) this.transform.position += new Vector3(0.01f, 0, 0);
        if (Input.GetKey(KeyCode.A)) this.transform.position += new Vector3(0, 0, -0.01f);
        if (Input.GetKey(KeyCode.D)) this.transform.position += new Vector3(0, 0, 0.01f);
        if (Input.GetKey(KeyCode.S)) this.transform.position += new Vector3(-0.01f, 0, 0);



    }



    private void OnApplicationQuit() {
        inv.Container.Clear();
    }


}

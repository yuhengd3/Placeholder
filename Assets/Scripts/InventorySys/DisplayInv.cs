using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInv : MonoBehaviour
{
    public InventoryObj inv;

    public int X_gap;
    public int Y_gap;

    public int X_start, Y_start;

    public int col_count;
    public Dictionary<InventorySlot, GameObject> items = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay() {
        Debug.Log("Hello");
        for (int i = 0; i < inv.Container.Count; i++) {
            var obj = Instantiate(inv.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            // Debug.Log(i);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inv.Container[i].amount.ToString("n0");
        }
    }

    public Vector3 GetPosition(int i) {
        return new Vector3(X_start + X_gap * (i % col_count), Y_start + (-Y_gap * (i / col_count)), 0f);
    }

    public void UpdateDisplay() {
        for (int i = 0; i < inv.Container.Count; i++) {
            if (items.ContainsKey(inv.Container[i]))
            {
                items[inv.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inv.Container[i].amount.ToString();

                if (inv.Container[i].amount == 0){
                    Debug.Log("Checkpoint");
                    Debug.Log("Item should be deleted");
                    GameObject child = this.gameObject.transform.GetChild(0).gameObject;
                    Destroy(child);
                    items.Remove(inv.Container[i]);
                    inv.Container.Clear();
                }

            }
            else {
                var obj = Instantiate(inv.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inv.Container[i].amount.ToString("n0");
                items.Add(inv.Container[i], obj);
                Debug.Log("Item added");
            }

        }
        
    }

    public void DestroyObj() {
        
    }

}

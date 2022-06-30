using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemObj item;

    public void Do(){
        item.Do();
    }

    public void Update()
    {
        HighLight();
    }

    private void HighLight() {
        float multipier = Mathf.Cos(Time.fixedTime) / 4f;

        Transform myTransform = this.gameObject.transform;
        myTransform.Rotate(0f, 0.5f * multipier, 0f, Space.Self);
        myTransform.localScale = new Vector3(0.5f,0.5f,0.5f) * (multipier + 2f);
    }

}

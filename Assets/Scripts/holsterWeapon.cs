using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holsterWeapon : MonoBehaviour
{
    public GameObject weapon;
    float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void holster()
    {
        weapon.transform.SetParent(this.transform);
        x = this.transform.position.x+0.7f;
        y = this.transform.position.y+1f;
        z = this.transform.position.z-0.6f;
        weapon.transform.position = new Vector3(x, y, z);

        weapon.transform.localEulerAngles = new Vector3(126.146f, 84.713f, 105.58f);
    }
}

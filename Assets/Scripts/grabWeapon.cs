using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabWeapon : MonoBehaviour
{
    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void draw()
    {
        weapon.transform.SetParent(this.transform);
        weapon.transform.position = this.transform.position;
        weapon.transform.localEulerAngles = new Vector3(-3.469f, 90.039f, -264.706f);
    }

}
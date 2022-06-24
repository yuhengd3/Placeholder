using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    public static AttackController current;

    public UnityEvent enemyHit;
    public UnityEvent weaponHit;
    public UnityEvent drawWeapon;
    public UnityEvent holsterWeapon;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

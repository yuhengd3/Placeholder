using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    public static AttackController current;

    public event UnityAction<int> enemyHit;
    public UnityEvent weaponHit;
    public UnityEvent drawWeapon;
    public UnityEvent holsterWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHit(int id)
    {
        if(enemyHit != null)
        {
            enemyHit(id);
        }
    }

}

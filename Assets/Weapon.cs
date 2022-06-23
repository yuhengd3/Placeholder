using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool hit = false;
    public float coolDownTime = 1.5f;
    public float timer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.tag == "Enemy")
            {
                AttackController.current.weaponHit.Invoke();
                Debug.Log("hiiiiiiiiit");
                //if (timer >= coolDownTime)
                //{
                //    AttackController.current.weaponHit.Invoke();
                //    Debug.Log("hiiiiiiiiit");
                //    timer = 0;
                //}
            }
        }
    }
}

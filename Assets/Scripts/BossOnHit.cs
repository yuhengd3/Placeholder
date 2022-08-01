using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOnHit : MonoBehaviour
{
    public Transform player;

    private Rigidbody rb;
    public float scale = 5f;
    private static int hitHash = Animator.StringToHash("Hit");
    private static int dieHash = Animator.StringToHash("Die");
    private static int dieHash_ = Animator.StringToHash("Die_");
    public bool die = false;

    private float deathTime = 1.5f;
    private float timer = 0;

    private Animator ani;
    private Boss e;
    private BossHealthController con;

    private void Start()
    {
        AttackController.current.enemyHit += hit;
        e = GetComponent<Boss>();
        rb = GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(1000, 0, 1000);
        ani = GetComponent<Animator>();
        con = BossHealthController.instance;
    }


    private void Update()
    {
        if (con.currentBossHealth <= -1000)
        {
            if (!die)
            {
                ani.SetTrigger(dieHash_);
                die = true;
                ani.SetBool(dieHash, die);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            //enabled = false;
            //timer += Time.deltaTime;
            //if(timer >= deathTime)
            //{
            //    ani.enabled = false;
            //}
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("hit");
    //    if (collision.gameObject.tag == "Weapon")
    //    {
    //        --health;
    //        ani.SetTrigger(hitHash);
    //    }
    //}


    public void hit(int num)
    {
        con.DecreaseHealth(num);
        ani.SetTrigger(hitHash);
        Vector3 direction = -(player.position - transform.position).normalized;
        //transform.position += direction * scale;
        Vector3 force = direction * scale;
        //rb.AddForce(force, ForceMode.Impulse);
        //rb.velocity = force;
        transform.position += force;
        e.timer = e.attackPeriod;
        e.state = Boss.State.chase;
    }
}

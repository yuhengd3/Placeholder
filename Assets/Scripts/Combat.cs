using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject weap;
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    private bool hit1 = false;
    private bool hit2 = false;
    private bool hit3 = false;

    private int hit1Hash = Animator.StringToHash("hit1");
    private int hit2Hash = Animator.StringToHash("hit2");
    private int hit3Hash = Animator.StringToHash("hit3");

    //used for attack period detection, change to private later
    public float timer = 0f;
    public float period = 1.5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            hit1 = false;
            //anim.SetBool("hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            hit2 = false;
            //anim.SetBool("hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            hit3 = false;
            AttackController.current.holsterWeapon.Invoke();
            //anim.SetBool("hit3", false);
            noOfClicks = 0;
        }
        anim.SetBool(hit1Hash, hit1);
        anim.SetBool(hit2Hash, hit2);
        anim.SetBool(hit3Hash, hit3);


        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        //cooldown time
        if (Time.time > nextFireTime)
        {
            // Check for mouse input
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();

            }
        }
    }

    void OnClick()
    {
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            hit1 = true;
            AttackController.current.drawWeapon.Invoke();
            //anim.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            hit1 = false;
            hit2 = true;
            AttackController.current.drawWeapon.Invoke();
            //anim.SetBool("hit1", false);
            //anim.SetBool("hit2", true);
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            hit2 = false;
            hit3 = true;
            AttackController.current.drawWeapon.Invoke();
            //anim.SetBool("hit2", false);
            //anim.SetBool("hit3", true);
        }
        anim.SetBool(hit1Hash, hit1);
        anim.SetBool(hit2Hash, hit2);
        anim.SetBool(hit3Hash, hit3);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("hit");
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        if(hit1 || hit2 || hit3)
    //        {
    //            AttackController.current.enemyHit.Invoke();
    //        }
    //    }
    //}

    public void weaponHit()
    {
        if(timer <= 0)
        {
            if(hit1 || hit2 || hit3)
            {
                int id = weap.GetComponent<Weapon>().hitID;
                print("id: " + id);
                AttackController.current.EnemyHit(id);
                timer = period;
            }
        }
    }
}
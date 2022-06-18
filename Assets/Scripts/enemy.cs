using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private enum State
    {
        roam,
        chase,
        attack
    }

    private State state;
    private Animator ani;
    private Vector3 original;
    private static int hitHash = Animator.StringToHash("Hit");
    private static int dieHash = Animator.StringToHash("Die");
    private static int meleeHash = Animator.StringToHash("Melee");
    private static int swordHash = Animator.StringToHash("Sword");
    private static int zombieHash = Animator.StringToHash("Zombie");
    private static int walkHash = Animator.StringToHash("Walking");
    private static int rightHash = Animator.StringToHash("RightTurn");
    private static int leftHash = Animator.StringToHash("LeftTurn");
    private static int[] attacks = { meleeHash, swordHash, zombieHash };
    private bool face_ = false;
    public Transform player;
    public float rotspeed = 1f;
    //public float maxMovespeed = 2f;
    //private float movespeed = 0f;
    //public float accelaration = 0.5f;
    //public float decelaration = 2f;
    public float minD = 5f;
    public float maxD = 20f;
    public float angleDifference = 10f;
    public float error = 1f;

    public float attackPeriod = 5f;
    private float currentTime = 5f;
    private float prevTime = 0f;

    private int health = 4;


    // Start is called before the first frame update
    void Start()
    {
        original = transform.position;
        state = State.roam;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            ani.SetTrigger(dieHash);
            enabled = false;
        }

        switch (state)
        {
            case State.roam:
                roam();
                break;
            case State.chase:
                chase(player.position);
                checkDistance();
                break;
            case State.attack:
                attack();
                checkDistance();
                if (state != State.attack)
                {
                    currentTime = 5f;
                    prevTime = 0f;
                }
                break;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(state == State.roam)
            {
                Debug.Log("changed");
                state = State.chase;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            state = State.roam;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if(collision.gameObject.tag == "Player")
        {
            yBot script = collision.gameObject.GetComponent<yBot>();
            if(script.state == yBot.State.attack)
            {
                --health;
                if(health > 0)
                {
                    ani.SetTrigger(hitHash);
                }
            }
        }
    }

    private void face(Vector3 other)
    {
        Vector3 direction = other - this.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotspeed * Time.deltaTime);
        Vector3 angle = rotation.eulerAngles;
        Vector3 curAngle = transform.rotation.eulerAngles;
        float difference = curAngle.y - angle.y;
        if(difference > angleDifference)
        {
            ani.SetBool(leftHash, true);
            face_ = false;
        }
        else if(difference < -angleDifference)
        {
            ani.SetBool(rightHash, true);
            face_ = false;
        }
        else
        {
            ani.SetBool(leftHash, false);
            ani.SetBool(rightHash, false);
            face_ = true;
        }
        //Debug.Log(difference);
    }

    private void chase(Vector3 other)
    {
        face(other);
        if (face_)
        {
            //Vector3 direction = (player.position - transform.position).normalized;
            //Vector3 position = transform.position;
            //position += direction * Time.deltaTime * movespeed;
            //transform.position = position;
            //increment speed
            //if (movespeed < maxMovespeed)
            //{
            //    movespeed += accelaration * Time.deltaTime;
            //}
            //set animationw
            ani.SetBool(walkHash, true);
        }
    }

    private void roam()
    {
        //Debug.Log("roam");
        float d = Vector3.Distance(original, this.transform.position);
        ani.SetBool(walkHash, false);
        if (d > error)
        {
            Debug.Log("d: " + d);
            //Debug.Log("return");
            chase(original);
        }
    }

    private void checkDistance()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);
        if (distance < minD)
        {
            state = State.attack;
        }
        else if (distance > maxD)
        {
            state = State.roam;
        }
        else
        {
            state = State.chase;
        }
        Debug.Log(distance);
    }

    private void slowDown()
    {
        //movespeed -= Time.deltaTime * decelaration;
        //if(movespeed < 0)
        //{
        //    movespeed = 0;
        //}
        //ani.SetFloat(VelocityHash, movespeed);
        //Vector3 direction = (player.position - transform.position).normalized;
        //Vector3 position = transform.position;
        //position += direction * Time.deltaTime * movespeed;
        //transform.position = position;
    }

    private void attack()
    {
        ani.SetBool(walkHash, false);
        face(player.position);
        if (face_)
        {
            Debug.Log("enterattack");
            if (currentTime - prevTime > attackPeriod)
            {
                Debug.Log("attack");
                int random = Random.Range(0, 3);
                Debug.Log(random);
                ani.SetTrigger(attacks[random]);
                prevTime = currentTime;

            }
        }
        currentTime += Time.deltaTime;
    }
}

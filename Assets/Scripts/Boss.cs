using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public enum State
    {
        chase,
        meleeAttack,
        rangeAttack
    }
    public int id;
    public State state;
    private Animator ani;
    private NavMeshAgent nav;
    private Vector3 original;
    private static int attack1Hash = Animator.StringToHash("Attack1");
    private static int attack2Hash = Animator.StringToHash("Attack2");
    private static int attack3Hash = Animator.StringToHash("Attack3");
    private static int attack4Hash = Animator.StringToHash("Attack4");
    private static int walkHash = Animator.StringToHash("Walk");
    private static int rightHash = Animator.StringToHash("RightTurn");
    private static int leftHash = Animator.StringToHash("LeftTurn");
    private static int[] attacks = { attack1Hash, attack2Hash, attack3Hash, attack4Hash};
    private bool face_ = false;
    public Transform player;
    public float rotspeed = 1f;
    public float minD = 5f;
    public float maxD = 20f;
    public float angleDifference = 10f;
    public float error = 1f;

    //TODO: used for attack period detection, change to private later
    public float attackPeriod = 5f;
    public float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        original = transform.position;
        state = State.chase;
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            state = State.meleeAttack;
        }
        switch (state)
        {
            case State.chase:
                aiChase(player.position);
                checkDistance();
                break;
            case State.meleeAttack:
                meleeAttack();
                checkDistance();
                break;
            case State.rangeAttack:
                rangeAttack();
                checkDistance();
                break;

        }

    }

    private void rangeAttack()
    {
        ani.SetBool(walkHash, true);
        print("rangeAttack");
    }

    private void face(Vector3 other)
    {
        Vector3 direction = other - this.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 angle = rotation.eulerAngles;
        Vector3 curAngle = transform.rotation.eulerAngles;
        float difference = curAngle.y - angle.y;
        if (difference > angleDifference)
        {
            ani.SetBool(leftHash, true);
            face_ = false;
        }
        else if (difference < -angleDifference)
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
        Debug.Log(difference);
    }

    private void checkDistance()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);
        if (distance < minD)
        {
            state = State.meleeAttack;
        }
        else if(distance < maxD)
        {
            state = State.rangeAttack;
        }
        else
        {
            state = State.chase;
        }
        //Debug.Log(distance + "!");
    }


    private void meleeAttack()
    {
        nav.enabled = false;
        ani.SetBool(walkHash, false);
        face(player.position);
        if (face_)
        {
            Debug.Log("enterattack");
            if (timer <= 0f)
            {
                Debug.Log("attack");
                int random = Random.Range(0, attacks.Length);
                Debug.Log(random);
                ani.SetTrigger(attacks[random]);
                timer = attackPeriod;

            }
        }
        timer -= Time.deltaTime;
    }

    private void aiChase(Vector3 other)
    {
        ani.SetBool(rightHash, false);
        ani.SetBool(leftHash, false);
        nav.enabled = true;
        float distance = Vector3.Distance(this.transform.position, player.position);
        nav.SetDestination(other);
        ani.SetBool(walkHash, true);
    }

}


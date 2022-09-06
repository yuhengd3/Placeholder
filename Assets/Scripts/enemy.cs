using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public enum State
    {
        roam,
        chase,
        attack
    }
    public int id;
    public State state;
    private Animator ani;
    private NavMeshAgent nav;
    private Vector3 original;
    private FieldOfView fov;
    private static int meleeHash = Animator.StringToHash("Melee");
    private static int swordHash = Animator.StringToHash("Sword");
    private static int walkHash = Animator.StringToHash("Walking");
    private static int rightHash = Animator.StringToHash("RightTurn");
    private static int leftHash = Animator.StringToHash("LeftTurn");
    private static int[] attacks = { meleeHash, swordHash};
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
        state = State.roam;
        ani = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            state = State.attack;
        }
        switch (state)
        {
            case State.roam:
                roam();
                if (fov.canSeePlayer)
                {
                    state = State.chase;
                }
                break;
            case State.chase:
                //chase(player.position);
                aiChase(player.position);
                checkDistance();
                break;
            case State.attack:
                attack();
                checkDistance();
                if (state == State.roam)
                {
                    //timer = 0f;
                }
                break;

        }

    }


    private void face(Vector3 other)
    {
        Vector3 direction = other - this.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
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
            ani.SetBool(walkHash, true);
        }
    }

    private void roam()
    {
        //Debug.Log("roam");
        float d = Vector3.Distance(original, this.transform.position);
        if(d <= error)
        {
            ani.SetBool(walkHash, false);
            nav.enabled = false;
        }
        if (d > error)
        {
            Debug.Log("d: " + d);
            //Debug.Log("return");
            aiChase(original);
        }
    }

    private void checkDistance()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);
        bool inView = fov.canSeePlayer;
        if(distance < minD)
        {
            state = State.attack;
        }
        else if(distance > maxD)
        {
            state = State.roam;
        }
        else
        {
            state = State.chase;
        }
        //Debug.Log(distance + "!");
    }


    private void attack()
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
                int random = Random.Range(0, 3);
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

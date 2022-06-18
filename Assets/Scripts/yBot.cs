using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yBot : MonoBehaviour
{
    public enum State
    {
        walk,
        attack
    }
    public State state = State.walk;
    private Animator ani;
    private float forward = 0f;
    private float side = 0f;
    public float acceleration = 0.2f;
    private int forwardHash;
    private int sideHash;
    private float weight = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        forwardHash = Animator.StringToHash("Forward");
        sideHash = Animator.StringToHash("Side");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPress = Input.GetKey(KeyCode.W);
        bool runPress = Input.GetKey(KeyCode.LeftShift);
        bool leftPress = Input.GetKey(KeyCode.A);
        bool rightPress = Input.GetKey(KeyCode.D);
        bool rightMousePress = Input.GetMouseButton(0);

        float maxiV = runPress ? 2f : 0.5f;

        if (forward < maxiV && forwardPress)
        {
            forward += Time.deltaTime * acceleration;
        }

        if(forward > 0f && !forwardPress){
            forward -= Time.deltaTime * acceleration;
        }

        if(side < maxiV && rightPress)
        {
            side += Time.deltaTime * acceleration;
        }

        if(side > -maxiV && leftPress)
        {
            side -= Time.deltaTime * acceleration;
        }

        if(side > 0f && !rightPress)
        {
            side -= Time.deltaTime * acceleration;
        }

        if(side < 0f && !leftPress)
        {
            side += Time.deltaTime * acceleration;
        }

        if(forward > maxiV)
        {
            forward -= Time.deltaTime * acceleration;
        }

        if(side < -maxiV)
        {
            side += Time.deltaTime * acceleration;
        }

        if(side > maxiV)
        {
            side -= Time.deltaTime * acceleration;
        }


        if (rightMousePress && weight < 1f)
        {
            weight += acceleration * Time.deltaTime;
            Debug.Log("add");
        }

        if(!rightMousePress && weight > 0f)
        {
            weight -= acceleration * Time.deltaTime;
        }

        if(weight > 0)
        {
            state = State.attack;
        }
        else
        {
            state = State.walk;
        }
        ani.SetLayerWeight(1, weight);


        ani.SetFloat(forwardHash, forward);
        ani.SetFloat(sideHash, side);

        //move
        Vector3 position = transform.position;
        position.z += forward * Time.deltaTime * 3;
        position.x += side * Time.deltaTime * 3;
        transform.position = position;
    }
}

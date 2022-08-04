using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemObj item;
    public string groundName;
    
    private Transform model; 
    private Rigidbody thisBody;

    private void Start(){
        thisBody = gameObject.GetComponent<Rigidbody>();
        model = gameObject.GetComponentInChildren<Transform>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == groundName){
            thisBody.GetComponent<BoxCollider>().isTrigger = true;
            thisBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        } 
    }

    private void Update() {
        float multipier = Mathf.Cos(Time.fixedTime) / 4f;
        Transform myTransform = model;
        myTransform.Rotate(0f, 0.5f * multipier, 0f, Space.Self);
        myTransform.localScale = new Vector3(0.5f,0.5f,0.5f) * (multipier + 2f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public bool active;

    void Start()
    {
        active = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            active = !active;
            Debug.Log(active);
            panel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            active = !active;
            Debug.Log(active);
            panel.SetActive(false);
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class SetupLocalPlayer : NetworkBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
            gameObject.GetComponent<PlayerController>().enabled = true;
        else
            gameObject.GetComponent<PlayerController>().enabled = false;


    }

    // Update is called once per frame
    void Update()
    {

    }
}

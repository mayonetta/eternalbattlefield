using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public GameObject VerorongMachineGunReticle;

    void Update()
    {
        //this.transform.forward = Camera.main.transform.forward;

        VerorongMachineGunReticle.transform.position = Input.mousePosition;
    }
}

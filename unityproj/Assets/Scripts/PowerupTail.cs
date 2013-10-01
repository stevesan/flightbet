
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupTail : MonoBehaviour
{
    public Vector3 chaseOffset = new Vector3(0, -2, 0);

    // we can only have one at a time
    GameObject powerup = null;

    Vector3 smoothVelocity = Vector3.zero;

    public void Add(GameObject pup)
    {
        if( powerup != null )
            Destroy(powerup);

        powerup = pup;
        smoothVelocity = Vector3.zero;
    }

    void Update()
    {
        if( Input.GetButtonDown("Fire2") )
        {
            if( powerup != null )
            {
                powerup.SendMessage("OnUse");
                // Do NOT destroy here - the powerup may need some time to take effect
                powerup = null;
            }
            else
            {
                // play error sound?
            }
        }

        if( powerup != null )
        {
            powerup.transform.position = Vector3.SmoothDamp( 
                    powerup.transform.position,
                    transform.position + chaseOffset,
                    ref smoothVelocity,
                    0.05f );
        }
    }
}

using UnityEngine;
using System.Collections;
using SteveSharp;

public class Wind : MonoBehaviour
{
    public Vector3 force;

    void OnTriggerStay( Collider other )
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if( rb != null )
        {
            Debug.Log("wind forcing "+other.gameObject.name);
            rb.AddForce( force );
        }
    }
}

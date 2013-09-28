using UnityEngine;
using System.Collections;
using SteveSharp;

public class Wind : MonoBehaviour
{
    public Vector3 force;
    public float maxTorque = 1f;

    void OnTriggerStay( Collider other )
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if( rb != null )
        {
            rb.AddForce( force );

            // random turbulence force
            rb.AddTorque( Vector3.forward *
                    Mathf.Lerp( -maxTorque, maxTorque, Random.value ) );

            Debug.Log("blowing "+other.gameObject.name);
        }
    }
}

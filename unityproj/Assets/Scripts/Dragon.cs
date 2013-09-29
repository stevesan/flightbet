using UnityEngine;
using System.Collections;
using SteveSharp;

public class Dragon : MonoBehaviour
{
    public Vector3 initOffset = Vector3.zero;
    public Vector3 dropOffset = Vector3.zero;

    bool isDropping = false;
    Vector3 dropVelocity = Vector3.zero;
    Vector3 dropTarget = Vector3.zero;

	// Use this for initialization
	void Start()
    {
        dropTarget = transform.position + dropOffset;
        transform.position += initOffset;
	}

    void Update()
    {
        if( isDropping )
        {
            transform.position = Vector3.SmoothDamp(
                    transform.position, dropTarget, ref dropVelocity, 0.1f );
        }
    }

    void OnTriggerEnter( Collider other )
    {
        PlaneMover plane = Utility.FindAncestor<PlaneMover>(other.gameObject);

        if( plane != null )
        {
            Debug.Log("entere");
            plane.rigidbody.AddForce( -10000 * Vector3.up, ForceMode.Acceleration );
            isDropping = true;
        }
    }
}

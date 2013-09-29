using UnityEngine;
using System.Collections;
using SteveSharp;

public class Mine : MonoBehaviour
{
    public GameObject explodeFx;
    public float bobHeight = 1f;
    public Vector3 bobForce = Vector3.up;
    public float explodeForceMag = 1f;

    Vector3 startPos;
    float bobOffset = 0f;

	// Use this for initialization
	void Start()
    {
        startPos = transform.position;
        bobOffset = Random.value * bobHeight;
	}
	
	// Update is called once per frame
	void LateUpdate()
    {
        float bobScale = Mathf.Sin( 2*Mathf.PI/bobHeight * (bobOffset + transform.position.y) );
        rigidbody.AddForce( bobScale * bobForce );
	}

    void OnCollisionEnter( Collision col )
    {
        if( col.gameObject.GetComponent<Piloted>() != null )
        {
            Vector3 toTarget = (col.gameObject.transform.position - transform.position).normalized;
            col.rigidbody.AddForce( toTarget * explodeForceMag );

            Utility.Instantiate( explodeFx, transform.position );
            Destroy(gameObject);
        }
    }
}

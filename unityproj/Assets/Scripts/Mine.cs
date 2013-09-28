using UnityEngine;
using System.Collections;
using SteveSharp;

public class Mine : MonoBehaviour
{
    public GameObject explodeFx;
    public AudioClip explodeClip;
    public float bobHeight = 1f;
    public Vector3 bobForce = Vector3.up;

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
        Utility.Instantiate( explodeFx, transform.position );
        AudioSource.PlayClipAtPoint( explodeClip, transform.position );
        Destroy(gameObject);
    }
}

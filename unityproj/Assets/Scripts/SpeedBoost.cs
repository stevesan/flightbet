using UnityEngine;
using System.Collections;
using SteveSharp;

public class SpeedBoost : MonoBehaviour
{
    public GameObject getFx;
    public GameObject boostFx;
    public float forceMag = 200f;

    GameObject owner;
    bool useQueued = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if( useQueued )
        {
            Vector3 f = owner.transform.right * forceMag;
            owner.GetComponent<Rigidbody>().AddForce( f );
            Destroy(gameObject);
        }
	}

    void OnGot(Piloted who)
    {
        Destroy( GetComponent<Rigidbody>() );
        Destroy( GetComponent<Collider>() );
        who.GetComponent<PowerupTail>().Add(gameObject);
        owner = who.gameObject;
    }

    void OnTriggerEnter( Collider col )
    {
        Piloted piloted = col.gameObject.GetComponent<Piloted>();

        if( piloted != null )
        {
            OnGot(piloted);
            Utility.Instantiate( getFx, transform.position );
        }
    }

    void OnUse()
    {
        useQueued = true;
        // make the effect follow the owner (ie. TRAILL)
        Utility.Instantiate( boostFx, owner.transform.position, owner.transform );
        boostFx.transform.localPosition -= new Vector3(-10, 0, 0);
    }
}

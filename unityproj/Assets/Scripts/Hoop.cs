using UnityEngine;
using System.Collections;
using SteveSharp;

public class Hoop : MonoBehaviour
{
    public GameObject explodeFx;
    public AudioClip explodeClip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider col )
    {
    	if (col.gameObject.name == "Plane")
    	{
        	Utility.Instantiate( explodeFx, transform.position );
        	AudioSource.PlayClipAtPoint( explodeClip, transform.position );
        	// Award pilot $.
        }
    }
}

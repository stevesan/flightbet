using UnityEngine;
using System.Collections;
using SteveSharp;

public class Mine : MonoBehaviour
{
    public GameObject explodeFx;
    public AudioClip explodeClip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter( Collision col )
    {
        Utility.Instantiate( explodeFx, transform.position );
        AudioSource.PlayClipAtPoint( explodeClip, transform.position );
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;
using SteveSharp;

public class Mine : MonoBehaviour
{
    public GameObject explodeFx;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter( Collision col )
    {
        Utility.Instantiate( explodeFx, transform.position );
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class FloatingCash : MonoBehaviour
{
    public float initLifetime = 3f;
    public Vector3 velocity = Vector3.zero;

    float lifetime = 0f;


	// Use this for initialization
	void Start()
    {
        lifetime = initLifetime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifetime -= Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if( lifetime < 0 )
            Destroy(gameObject);
	}
}

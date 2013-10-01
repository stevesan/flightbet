using UnityEngine;
using System.Collections;

public class FloatingCash : MonoBehaviour
{
    public float initLifetime = 3f;
    public Vector3 velocity = Vector3.zero;
    public tk2dSpriteAnimator anim;
    public Vector3 accel;

    float lifetime = 0f;


	// Use this for initialization
	void Start()
    {
        lifetime = initLifetime;
        anim.PlayFromFrame( Random.Range(0, 10) );
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifetime -= Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        velocity += accel * Time.deltaTime;

        if( lifetime < 0 )
            Destroy(gameObject);
	}
}

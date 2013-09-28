using UnityEngine;
using System.Collections;
using SteveSharp;

public class Mine : MonoBehaviour
{
    public GameObject explodeFx;
    public AudioClip explodeClip;
    public float waveFreq = 1f;
    public Vector3 waveOffset = Vector3.up;

    Vector3 startPos;
    float wavePhase = 0f;

	// Use this for initialization
	void Start()
    {
        startPos = transform.position;
        wavePhase = Random.value;
	}
	
	// Update is called once per frame
	void Update()
    {
        transform.position = startPos + Mathf.Sin( wavePhase*2*Mathf.PI + 2*Mathf.PI*waveFreq * Time.time ) * waveOffset;
	
	}

    void OnCollisionEnter( Collision col )
    {
        Utility.Instantiate( explodeFx, transform.position );
        AudioSource.PlayClipAtPoint( explodeClip, transform.position );
        Destroy(gameObject);
    }
}

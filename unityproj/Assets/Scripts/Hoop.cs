using UnityEngine;
using System.Collections;
using SteveSharp;

public class Hoop : MonoBehaviour
{
    public AudioClip explodeClip;

    public GameObject cashPrefab;
    public int numCashes = 10;
    public float cashSpawnRadius = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider col )
    {
    	if (col.gameObject.GetComponent<PlaneMover>() != null)
    	{
        	AudioSource.PlayClipAtPoint( explodeClip, transform.position );
            Destroy(gameObject);
        	// Award pilot $.

            SpawnCash();
        }
    }

    void SpawnCash()
    {
        for( int i = 0; i < numCashes; i++ )
        {
            Vector3 p = Utility.SampleCircleXY( transform.position, cashSpawnRadius );
            GameObject inst = Utility.Instantiate( cashPrefab, p );
            FloatingCash cash = inst.GetComponent<FloatingCash>();
            //cash.velocity = (p-transform.position).normalized * 1f;
        }
    }
}

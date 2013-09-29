using UnityEngine;
using System.Collections;

public class JitterPosition : MonoBehaviour {

    public Vector3 maxOffset = Vector3.zero;

	// Use this for initialization
	void Start()
    {
        transform.position += Vector3.Scale(
                new Vector3( Random.value, Random.value, Random.value ) * 2f - new Vector3(1,1,1),
                maxOffset );
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

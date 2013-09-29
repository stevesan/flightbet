using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetScale = new Vector3(0.5f, 0.5f, 0.5f);

    Vector3 origPos;
    Vector3 origTargetPos;

	// Use this for initialization
	void Start()
    {
        origPos = transform.position;
        origTargetPos = target.position;
	}

    void LateUpdate()
    {
        Vector3 targetOffset = target.position - origTargetPos;
        transform.position = origPos + Vector3.Scale( offsetScale, targetOffset );
    }
}

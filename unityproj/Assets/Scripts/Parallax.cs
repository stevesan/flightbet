using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{
    public Transform target;
    public float offsetScale = 0f;  // If 0, this will not move at all WRT to the target

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
        transform.position = origPos + offsetScale * targetOffset;
    }
}

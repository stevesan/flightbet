using UnityEngine;
using System.Collections;
using SteveSharp;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Transform limitsRef;
    public float minY = 0f;
    public float maxY = 100f;
    public float smoothTime = 0.5f;
    public Vector3 maxShakeOffset;

    public GameEvent lateUpdateDone = new GameEvent();

    Vector3 followVelocity = Vector3.zero;
    Vector3 offset;

    public float shakeDuration = 1f;
    float shakeRemainTime = 0f;

    Vector3 unshakenPosition;

	// Use this for initialization
	void Start()
    {
        offset = transform.position - target.position;
        unshakenPosition = transform.position;
	}

    void Update()
    {
        shakeRemainTime -= Time.deltaTime;
    }

    public void TriggerShake()
    {
        shakeRemainTime = shakeDuration;
    }
	
	// Update is called once per frame
	void LateUpdate()
    {
        unshakenPosition = Vector3.SmoothDamp( unshakenPosition, target.position+offset, ref followVelocity, smoothTime );
        unshakenPosition.y = Mathf.Clamp( unshakenPosition.y, limitsRef.position.y + minY, limitsRef.position.y + maxY );

        Vector3 shakeOffset = Mathf.Max( 0f, Utility.Unlerp(0, shakeDuration, shakeRemainTime) )
            * Vector3.Scale( new Vector3( Random.value, Random.value, Random.value ), maxShakeOffset );
        transform.position = unshakenPosition + shakeOffset;
        lateUpdateDone.Trigger(gameObject);
	}
}

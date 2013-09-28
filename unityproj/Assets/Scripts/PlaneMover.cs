using UnityEngine;
using System.Collections;

public class PlaneMover : MonoBehaviour
{
    public float fullSpeed = 100f;
    public float activeMaxAccel = 300f;
    public float passiveMaxAccel = 100f;
    public float fullVerticalControlSpeed = 100f;
    public float angularSpeed = 5f;
    public float liftSpeed = 0.5f;
    public float outputVal = 1f;

    // Use this for initialization
	void Start()
    {
	}

    private void CheckOnGround()
    {
        //var fwd = transform.TransformDirection (Vector3.up);
        //if (Physics.Raycast (transform.position, fwd, 10)) {
    }

    void FixedUpdate()
    {
        float angleChange = Input.GetAxisRaw("Vertical");
        rigidbody.AddTorque(new Vector3(0f, 0f, angleChange * angularSpeed));

        Vector3 move = transform.right * Mathf.Max(0, Input.GetAxisRaw("Horizontal"));
        rigidbody.AddForce(move, ForceMode.Force);

        outputVal = Vector3.Dot(rigidbody.velocity, transform.right);
        Vector3 liftForce = new Vector3(0, Vector3.Dot(rigidbody.velocity, transform.right), 0);
        rigidbody.AddForce(liftForce * liftSpeed, ForceMode.Force);

        CheckOnGround();
    }
}

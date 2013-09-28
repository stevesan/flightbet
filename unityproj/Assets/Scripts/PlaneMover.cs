using UnityEngine;
using System.Collections;

public class PlaneMover : MonoBehaviour
{
    public float throttle = 15f;
    public float maxSpeed = 40f;
    public float angularSpeed = 2f;
    public float liftSpeed = 14f;
    public AudioSource thrustAudio;
    public ParticleSystem thrustFx;

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
        //float stallFactor = Mathf.Clamp(Vector3.Dot(rigidbody.velocity, transform.right), 0, 1);
        rigidbody.AddTorque(new Vector3(0f, 0f, angleChange * angularSpeed));

        Vector3 move = transform.right * Mathf.Max(0, Input.GetAxisRaw("Horizontal") * throttle);
        rigidbody.AddForce(move, ForceMode.Force);

        Vector3 liftForce = new Vector3(0, Mathf.Clamp(Vector3.Dot(rigidbody.velocity, transform.right), 0, 1), 0);
        rigidbody.AddForce(liftForce * liftSpeed, ForceMode.Force);

        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }

        CheckOnGround();

        if( move.magnitude > 1e-2 && !thrustAudio.isPlaying )
        {
            thrustAudio.Play();
            thrustFx.Play();
        }
        else if( move.magnitude <= 1e-2 && thrustAudio.isPlaying )
        {
            thrustAudio.Stop();
            thrustFx.Stop();
        }
    }
}

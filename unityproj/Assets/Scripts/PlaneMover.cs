using UnityEngine;
using System.Collections;
using SteveSharp;

public class PlaneMover : MonoBehaviour
{
    public float throttle = 15f;
    public float maxSpeed = 40f;
    public float angularSpeed = 4f;
    public float liftSpeed = 14f;
    public AudioSource thrustAudio;
    public ParticleSystem thrustFx;
    public GameObject backWing;
    public tk2dSpriteAnimator propAnim;
    public AudioClip backWingMoveClip;

    private int prevBackWingSign = 0;

    // Use this for initialization
	void Start()
    {
	}

    void FixedUpdate()
    {
        float speedPercent = rigidbody.velocity.magnitude / maxSpeed;
        float velocityRightDot = Vector3.Dot(rigidbody.velocity, transform.right);
        float normalizedVelocityRightDot = Vector3.Dot(rigidbody.velocity.normalized, transform.right);

        float angleChange = -Input.GetAxisRaw("Vertical");
        rigidbody.AddTorque(new Vector3(0f, 0f, angleChange * angularSpeed * Mathf.Max(0, normalizedVelocityRightDot) * speedPercent));


        float negativeVelocityRightDot = Mathf.Clamp(velocityRightDot, -1, 0);
        Vector3 move = transform.right * Mathf.Max(0, Input.GetAxisRaw("Fire1") * (throttle - (throttle * negativeVelocityRightDot)));
        rigidbody.AddForce(move, ForceMode.Force);

        float liftForce = normalizedVelocityRightDot;
        rigidbody.AddForce(transform.up * liftForce * liftSpeed * speedPercent, ForceMode.Force);

        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }

        //----------------------------------------
        //  Thrust fx
        //----------------------------------------
        if( move.magnitude > 1e-2 && !thrustAudio.isPlaying )
        {
            thrustAudio.Play();
            thrustFx.Play();
            propAnim.Play();
        }
        else if( move.magnitude <= 1e-2 && thrustAudio.isPlaying )
        {
            thrustAudio.Stop();
            thrustFx.Stop();
            propAnim.Stop();
        }

        //----------------------------------------
        //  Backwing fx
        //----------------------------------------
        int backWingSign = Utility.SignOrZero(Input.GetAxisRaw("Vertical"));
        backWing.transform.localEulerAngles = new Vector3( 0, 0, backWingSign * 45 );
        if( backWingSign != prevBackWingSign )
            AudioSource.PlayClipAtPoint( backWingMoveClip, transform.position );
        prevBackWingSign = backWingSign;
    }
}

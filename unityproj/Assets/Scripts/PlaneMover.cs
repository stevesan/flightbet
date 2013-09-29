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
    public GameObject damageFx;
    public AudioClip backWingMoveClip;
    public float moveScale = 1f;
    public float gracePeriod = 1f;

    public GameEvent gameOverEvent = new GameEvent();

    int prevBackWingSign = 0;
    float graceTimer = 0;

    // Use this for initialization
	void Start()
    {
	}

    void FixedUpdate()
    {
        float speedFrac = rigidbody.velocity.magnitude / maxSpeed;
        float velocityRightDot = Vector3.Dot(rigidbody.velocity, transform.right);
        float normalizedVelocityRightDot = Vector3.Dot(rigidbody.velocity.normalized, transform.right);

        float angleChange = -Input.GetAxisRaw("Vertical");
        rigidbody.AddTorque(new Vector3(0f, 0f, angleChange * angularSpeed * Mathf.Max(0, normalizedVelocityRightDot) * speedFrac));


        float negativeVelocityRightDot = Mathf.Clamp(velocityRightDot, -1, 0);
        Vector3 move = moveScale * transform.right * Mathf.Max(0, Input.GetAxisRaw("Fire1") * (throttle - (throttle * negativeVelocityRightDot)));
        rigidbody.AddForce(move, ForceMode.Force);

        float liftForce = normalizedVelocityRightDot;
        rigidbody.AddForce(transform.up * liftForce * liftSpeed * speedFrac, ForceMode.Force);

        /*
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }
        */

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

        graceTimer -= Time.deltaTime;
        if( graceTimer > 0 )
        {
            propAnim.gameObject.SetActive( Mathf.FloorToInt((graceTimer/gracePeriod)/0.1f) % 2 == 0);
        }
        else
        {
            propAnim.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter( Collision col )
    {
        if( col.gameObject.GetComponent<Ground>() != null )
        {
            // game over!
            Utility.Instantiate( damageFx, transform.position );
            gameOverEvent.Trigger(this);
        }
        else if( graceTimer < 0 )
        {
            Utility.Instantiate( damageFx, transform.position );
            graceTimer = gracePeriod;

        }
    }
}

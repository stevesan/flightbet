using UnityEngine;
using System.Collections;
using SteveSharp;

public class LightningCloud : MonoBehaviour
{
    public float minCooldown = 3f;
    public float maxCooldown = 8f;
    public tk2dSpriteAnimator anim;
    public int lightningFrame = 10;
    public GameObject lightningPrefab;

    PlaneMover plane;

    float cooldown = 0f;
    bool isStriking = false;

	// Use this for initialization
	void Start()
    {
        cooldown = Random.Range( minCooldown, maxCooldown );
        isStriking = false;
	}
	
	// Update is called once per frame
	void Update()
    {
        if( !isStriking )
        {
            cooldown -= Time.deltaTime;

            if( cooldown < 0 )
            {
                anim.Play();
                isStriking = true;
            }
        }
        else
        {
            if( anim.CurrentFrame >= lightningFrame )
            {
                Utility.Instantiate( lightningPrefab, transform.position, transform );
                isStriking = false;
                cooldown = Random.Range( minCooldown, maxCooldown );

                // Damage player, if they're in our trigger
                if( plane != null )
                {
                    plane.OnLightningDamage(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter( Collider other )
    {
        PlaneMover thisPlane = Utility.FindAncestor<PlaneMover>(other.gameObject);

        if( thisPlane != null )
        {
            plane = thisPlane;
        }
    }

    void OnTriggerExit( Collider other )
    {
        PlaneMover thisPlane = Utility.FindAncestor<PlaneMover>(other.gameObject);

        if( plane == thisPlane )
        {
            plane = null;
        }
    }
}


using UnityEngine;
using System.Collections;
using SteveSharp;

public class AutoDestroyEffects : MonoBehaviour
{
    tk2dSpriteAnimator animator;
    AudioSource audioSource;

    void Awake()
    {
        animator = GetComponent<tk2dSpriteAnimator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool waitAnim = (animator != null && animator.IsPlaying( animator.DefaultClip ));
        bool waitAudio = (audioSource != null && audioSource.isPlaying);

        if( !waitAnim && !waitAudio )
        {
            Destroy(gameObject);
        }
    }
}

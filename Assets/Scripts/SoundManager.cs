using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Space]
    [Header("SFX")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip jumpSound;
    public enum Sound { pickup, jump };
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(Sound s)
    {
        if (s == Sound.pickup)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        else if (s == Sound.jump)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _swingSound;
    [SerializeField] private AudioClip _grassSound;
    [SerializeField] private AudioClip _wallSound;
    [SerializeField] private AudioClip _scoreSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _fallSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _fortifySound;


    private AudioSource _audioSource;

    public static SoundManager soundManager;

    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySwing()
    {
        _audioSource.PlayOneShot(_swingSound);
    }

    public void PlayHitGrass()
    {
        _audioSource.PlayOneShot(_grassSound);
    }

    public void PlayHitWall()
    {
        _audioSource.PlayOneShot(_wallSound);
    }

    public void PlayScore()
    {
        _audioSource.PlayOneShot(_scoreSound);
    }

    public void PlayDeath()
    {
        _audioSource.PlayOneShot(_deathSound);
    }

    public void PlayFall()
    {
        _audioSource.PlayOneShot(_fallSound);
    }

    public void PlayExplosion()
    {
        _audioSource.PlayOneShot(_explosionSound);
    }

    public void PlayFortify()
    {
        _audioSource.PlayOneShot(_fortifySound);
    }
}

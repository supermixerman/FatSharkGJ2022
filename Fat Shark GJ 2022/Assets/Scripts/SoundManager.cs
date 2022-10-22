using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _swingSound;

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

    public void PlaySwingSound()
    {
        _audioSource.PlayOneShot(_swingSound);
    }
}

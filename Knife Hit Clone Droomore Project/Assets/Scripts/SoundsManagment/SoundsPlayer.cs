using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundsPlayer : MonoBehaviour
{
    public static SoundsPlayer Instance;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip PlayAndRestartButtonSound;
    [SerializeField] private AudioClip HomeButtonSound;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAndRestartButtonSoundPlay()
    {
        _audioSource.PlayOneShot(PlayAndRestartButtonSound, 0.1f);
    }

    public void HomeButtonSoundPlay()
    {
        _audioSource.PlayOneShot(HomeButtonSound, 0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KnifeCounter), typeof(AudioSource))]
public class KnifeThrower : MonoBehaviour
{
    private KnifeCounter _knifeCounter;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _knifeCounter = GetComponent<KnifeCounter>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.childCount > 0)
        {
            KnifeMover knife = transform.GetChild(0).GetComponent<KnifeMover>();

            if (knife.IsActive)
            {
                knife.Throw();
                _audioSource.Play();
                knife.IsActive = false;
                _knifeCounter.DecrementDisplayedKnifeCount();
                _knifeCounter.KnivesAmountOnLevel--;
            }
        }
    }
}

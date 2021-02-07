using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Apple : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void DestroyApple()
    {
        transform.SetParent(null);
        _boxCollider2D.enabled = false;
        _spriteRenderer.enabled = false;

        PlayerStatistics.Instance.IncreaseAppleScore();

        _audioSource.Play();
        _particleSystem.Play();

        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Knife knife) && !PlayerStatistics.Instance.GameIsOver)
        { 
            DestroyApple();
        }
    }
}

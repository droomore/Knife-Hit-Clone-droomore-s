using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class KnifeMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _splashEffect;

    [SerializeField] private ParticleSystem _particleSystem;

    public bool IsActive = true;

    private Vector3 _lastPos;

    private bool _isAbleToThrow;

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _wheelHit;
    [SerializeField] private AudioClip _knifeHit;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        Vibration.Init();
    }

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _isAbleToThrow = false;
    }

    private void Update()
    {
        _lastPos = transform.position;

        if (_isAbleToThrow)
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;

            LayerMask layerMask = 1 << 8;
            layerMask = ~layerMask;

            RaycastHit2D hit = Physics2D.Linecast(_lastPos, transform.position, layerMask);

            if (hit.collider != null)
            {
                _isAbleToThrow = false;

                if (hit.collider.gameObject.TryGetComponent(out Wheel wheel))
                {
                    hit.collider.GetComponent<Animator>().SetTrigger("Hit");
                    transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                    _particleSystem.Play();
                    GetComponentInParent<KnifeSpawner>().OnSuccessfulKnifeHit();
                    transform.parent = hit.transform;
                    PlayerStatistics.Instance.IncreaseKnifeScore();
                    _audioSource.PlayOneShot(_wheelHit);
                    Vibration.VibratePop();
                    _boxCollider2D.enabled = true;
                    hit.collider.GetComponent<WheelPartsDestroyer>().Damage(1);
                }
                else if (hit.collider.gameObject.TryGetComponent(out Knife knife))
                {
                    Instantiate(_splashEffect, new Vector3(hit.point.x, hit.point.y, -1), Quaternion.identity);
                    _audioSource.PlayOneShot(_knifeHit);
                    Vibration.VibratePop();
                    transform.SetParent(null);
                    _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    _rigidbody2D.AddForce(new Vector2(_rigidbody2D.velocity.x, -11), ForceMode2D.Impulse);
                    _rigidbody2D.AddTorque(30, ForceMode2D.Impulse);
                    StopGame();
                }
            }
        }
    }

    public void Throw()
    {
        _isAbleToThrow = true;
    }

    private void StopGame()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        PlayerStatistics.Instance.GameIsOver = true;
        yield return new WaitForSecondsRealtime(0.5f);
        PlayerStatistics.Instance.GameEndMenu.SetActive(true);
        PlayerStatistics.Instance.Canvas.sortingOrder = 1;

    }
}
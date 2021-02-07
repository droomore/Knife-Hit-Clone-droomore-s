using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(CircleCollider2D))]
public class WheelPartsDestroyer : MonoBehaviour
{
    [SerializeField] private KnifeCounter _knifeCounter;
    [SerializeField] private int _health;

    private AudioSource _audioSource;

    private CircleCollider2D _circleCollider2D;

    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _health = _knifeCounter.KnivesAmountOnLevel;
    }

    public void Damage(int damage)
    {
        _health -= damage;

        if (_health == 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        _circleCollider2D.enabled = false;

        _audioSource.Play();

        MoveTrunkApart(new Vector3(400, 800, 0), new Vector3(100, 100, 100));
        MoveTrunkApart(new Vector3(-400, 800, 0), new Vector3(-100, 100, 100));
        MoveTrunkApart(new Vector3(0, 800, 0), new Vector3(-200, 100, -100));

        while (transform.childCount > 0)
        {
           if (transform.GetChild(0).TryGetComponent(out Apple apple))
           {
                Destroy(apple);
                transform.GetChild(0).GetComponent<Rigidbody2D>().isKinematic = false;
                transform.GetChild(0).parent = null;
           }
           else
           {
                Transform child = transform.GetChild(0);
                Rigidbody2D childRigidbody2D = child.GetComponent<Rigidbody2D>();
                childRigidbody2D.isKinematic = false;
                childRigidbody2D.AddForce(new Vector2(Random.Range(-400f, 400f), Random.Range(400f, 800f)));
                childRigidbody2D.AddTorque(Random.Range(-400, 400));
                child.parent = null;
           }
        }

        NextLevelLoader.Instance.Load();
    }

    private void MoveTrunkApart(Vector3 force, Vector3 torque)
    {
        Transform child = transform.GetChild(0);
        Rigidbody childRigidbody = child.GetComponent<Rigidbody>();
        childRigidbody.isKinematic = false;
        childRigidbody.AddForce(force);
        childRigidbody.AddTorque(torque);
        child.parent = null;
    }
}

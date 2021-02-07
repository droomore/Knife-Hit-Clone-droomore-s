using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KnifeCounter))]
public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _knifeSpawnPosition;
    [SerializeField] private GameObject _knifeObject;

    private KnifeCounter _knifeCounter;

    private void Awake()
    {
        _knifeCounter = GetComponent<KnifeCounter>();
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(_knifeObject, _knifeSpawnPosition, Quaternion.identity, transform);
    }

    public void OnSuccessfulKnifeHit()
    {
        if (_knifeCounter.KnivesAmountOnLevel > 0)
        {
            Spawn();
        }
    }

}

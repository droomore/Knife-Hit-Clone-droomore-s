using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class WheelPropsSpawner : MonoBehaviour, ISceneLoadHandler<LevelsConfigurator>
{
    [Header("Apple Settings")]
    [SerializeField] private float _appleChance;
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] private List<float> _appleAngles;

    [Header("Stucked Knives Settings")]
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private List<float> _knivesAngles;

    private void Start()
    {
        if (Random.value <= _appleChance)
        {
            SpawnApple();
        }

        SpawnKnives();
    }

    public void OnSceneLoaded(LevelsConfigurator argument)
    {
        _appleChance = argument.AppleChance;
    }

    private void SpawnApple()
    {
        if (_appleAngles.Count == 0) return;

        float angleToSpawn = _appleAngles[Random.Range(0, _appleAngles.Count)];

        if (_knivesAngles.Contains(angleToSpawn))
        {
            _knivesAngles.Remove(angleToSpawn);
        }

        GameObject apple = Instantiate(_applePrefab);
        apple.transform.SetParent(transform);

        Vector2 offset = new Vector2(Mathf.Sin(angleToSpawn * Mathf.Deg2Rad), Mathf.Cos(angleToSpawn * Mathf.Deg2Rad)) * 26f;

        apple.transform.localPosition = (Vector2)transform.localPosition + offset;
        apple.transform.localRotation = Quaternion.Euler(0, 0, -angleToSpawn);
    }

    private void SpawnKnives()
    {
        int amountOfKnives = Random.Range(1, 4);

        if (amountOfKnives > _knivesAngles.Count) return;

        List<float> knivesAnglesCopy = _knivesAngles;

        for (int i = 0; i < amountOfKnives; i++)
        {
            int angleIndex = Random.Range(0, knivesAnglesCopy.Count);
            float angleToSpawn = knivesAnglesCopy[angleIndex];
            knivesAnglesCopy.RemoveAt(angleIndex);

            GameObject knife = Instantiate(_knifePrefab);
            knife.transform.SetParent(transform);

            Vector2 offset = new Vector2(Mathf.Sin(angleToSpawn * Mathf.Deg2Rad), Mathf.Cos(angleToSpawn * Mathf.Deg2Rad)) * 21.9f;

            knife.transform.localPosition = (Vector2)transform.localPosition + offset;
            knife.transform.localRotation = Quaternion.Euler(0, 0, -angleToSpawn + 180);

        }
    }
}

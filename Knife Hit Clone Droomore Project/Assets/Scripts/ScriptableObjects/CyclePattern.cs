using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cycle Pattern", menuName = "Cycle Pattern")]
public class CyclePattern : ScriptableObject
{
    [SerializeField] private float _cycleVelocity;
    [SerializeField] private float _cycleDuration;

    public float CycleVelocity => _cycleVelocity;
    public float CycleDuration => _cycleDuration;
}

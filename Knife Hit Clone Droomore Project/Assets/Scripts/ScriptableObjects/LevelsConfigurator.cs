using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Levels Configurator", menuName = "Levels Configurator")]
public class LevelsConfigurator : ScriptableObject
{
    [Header("Min And Max Amount Of Knives")]
    [SerializeField] private Vector2Int _knivesAmountOnLevel;

    [SerializeField] private List<CyclePattern> _cyclePatterns;
    [Range(0,1)] [SerializeField] private float _appleChance;
    
    public List<CyclePattern> CyclePatterns => _cyclePatterns;
    public float AppleChance => _appleChance;
    public Vector2Int KnivesAmountOnLevel => _knivesAmountOnLevel;
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

[RequireComponent(typeof(Rigidbody2D))]
public class WheelRotator : MonoBehaviour, ISceneLoadHandler<LevelsConfigurator>
{
    [SerializeField] private float _velocity;
    [SerializeField] private bool _varyVelocity;
    [SerializeField] private float _durationOfOneCycle;
    private float _initialVelocity;
    private bool _accelerate;
    private float _lerpVariable;

    private Rigidbody2D _rigidbody2D;

    private List<CyclePattern> _cyclePatterns;

    public void OnSceneLoaded(LevelsConfigurator argument)
    {
        _cyclePatterns = new List<CyclePattern>();

        int cyclesCount = DifficultyCalculation();

        if (cyclesCount > argument.CyclePatterns.Count) cyclesCount = argument.CyclePatterns.Count;

        for (int i = 0; i < cyclesCount; i++)
        {
            _cyclePatterns.Add(argument.CyclePatterns[i]);
        }

        CyclePattern cyclePattern = _cyclePatterns[Random.Range(0, _cyclePatterns.Count)];
        _velocity = cyclePattern.CycleVelocity;
        _durationOfOneCycle = cyclePattern.CycleDuration;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _accelerate = true;
        _initialVelocity = _velocity;
        _lerpVariable = 0;
    }

    private void Update()
    {
        if (!PlayerStatistics.Instance.GameIsOver)
        {
            if (_varyVelocity)
            {
                VaryVelocity();
            }
            Rotate();
        }
        else
        {
            _rigidbody2D.angularVelocity = 0;
        }
    }

    private void Rotate()
    {
        _rigidbody2D.angularVelocity = _velocity * 10;
    }

    private void VaryVelocity()
    {
        if (_accelerate)
        {
            if (_velocity != 0)
            {
                _velocity = Mathf.Lerp(_initialVelocity, 0, _lerpVariable);
                _lerpVariable += Time.deltaTime / _durationOfOneCycle;
            }
            else
            {
                _lerpVariable = 0;
                _accelerate = !_accelerate;
                CyclePattern cyclePattern = _cyclePatterns[Random.Range(0, _cyclePatterns.Count)];
                _initialVelocity = cyclePattern.CycleVelocity;
                _durationOfOneCycle = cyclePattern.CycleDuration;
            }
        }
        else
        {
            if (_velocity != _initialVelocity)
            {
                _velocity = Mathf.Lerp(0, _initialVelocity, _lerpVariable);
                _lerpVariable += Time.deltaTime / _durationOfOneCycle;
            }
            else
            {
                _lerpVariable = 0;
                _accelerate = !_accelerate;
            }
        }
    }

    private int DifficultyCalculation()
    {
        int stage = PlayerPrefs.GetInt("StageScoreInGame", 1);

        if (stage == 1) stage = 2;

        int amountOfCyclesOnLevel = (int)Mathf.Log(stage, 2);

        return amountOfCyclesOnLevel;
    }
}

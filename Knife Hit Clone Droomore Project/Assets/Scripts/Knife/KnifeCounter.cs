using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class KnifeCounter : MonoBehaviour, ISceneLoadHandler<LevelsConfigurator>
{
    public int KnivesAmountOnLevel { get { return _knivesAmountOnLevel; } set { _knivesAmountOnLevel = value; } }

    [Header("Knife Count Display")]
    [SerializeField] private int _knivesAmountOnLevel;
    [SerializeField] private GameObject _knivesPanel;
    [SerializeField] private GameObject _knifeIcon;
    [SerializeField] private Color _usedKnifeIconColor;

    private int _childCount;

    private int _knifeIconToChange = 0;

    public void OnSceneLoaded(LevelsConfigurator argument)
    {
        _knivesAmountOnLevel = Random.Range(argument.KnivesAmountOnLevel.x, argument.KnivesAmountOnLevel.y);
    }

    private void Start()
    {
        SetInitialDisplayedKnifeCount(_knivesAmountOnLevel);
        _childCount = _knivesPanel.transform.childCount;
    }

    private void SetInitialDisplayedKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(_knifeIcon, _knivesPanel.transform);
    }

    public void DecrementDisplayedKnifeCount()
    {
        if (_knifeIconToChange < _childCount)
        {
            _knivesPanel.transform.GetChild(_knifeIconToChange++)
            .GetComponent<Image>().color = _usedKnifeIconColor;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class GameEndMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _knifeScore;
    [SerializeField] private TextMeshProUGUI _stageScore;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();

        _knifeScore.text = PlayerPrefs.GetInt("KnifeScoreInGame", 0).ToString();
        _stageScore.text = "STAGE " + PlayerPrefs.GetInt("StageScoreInGame", 1).ToString();
    }
}

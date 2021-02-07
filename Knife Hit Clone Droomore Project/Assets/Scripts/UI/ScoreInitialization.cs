using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreInitialization : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _appleCount;
    [SerializeField] private TextMeshProUGUI _stageScore;
    [SerializeField] private TextMeshProUGUI _knifeScore;

    private void Awake()
    {
        AppleCountInit();
        StageScoreInit();
        KnifeScoreInit();
    }

    private void KnifeScoreInit()
    {
        _knifeScore.text = "SCORE " + PlayerPrefs.GetInt("KnifeScoreBest", 0);
        PlayerPrefs.DeleteKey("KnifeScoreInGame");
    }

    private void StageScoreInit()
    {
        _stageScore.text = "STAGE " + PlayerPrefs.GetInt("StageScoreBest", 0);
        PlayerPrefs.DeleteKey("StageScoreInGame");
    }

    private void AppleCountInit()
    {
        _appleCount.text = PlayerPrefs.GetInt("AppleScore", 0).ToString();
    }
}

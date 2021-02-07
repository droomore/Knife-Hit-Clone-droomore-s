using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatistics : MonoBehaviour
{
    public static PlayerStatistics Instance;

    public bool GameIsOver = false;
    public GameObject GameEndMenu;
    public Canvas Canvas;

    [SerializeField] private TextMeshProUGUI _knifeScoreText;
    private int _knifeScoreInGame;

    [SerializeField] private TextMeshProUGUI _appleScoreText;
    private int _appleScore;

    [SerializeField] private TextMeshProUGUI _stageScoreText;
    private int _stageScoreInGame;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        ScoresInit();
    }

    private void ScoresInit()
    {
        _appleScoreText.text = PlayerPrefs.GetInt("AppleScore", 0).ToString();
        _stageScoreText.text = "STAGE " + PlayerPrefs.GetInt("StageScoreInGame", 1).ToString();
        _knifeScoreText.text = PlayerPrefs.GetInt("KnifeScoreInGame", 0).ToString();
    }

    public void IncreaseKnifeScore()
    {
        _knifeScoreInGame = PlayerPrefs.GetInt("KnifeScoreInGame", 0);
        _knifeScoreInGame++;

        _knifeScoreText.text = _knifeScoreInGame.ToString();

        PlayerPrefs.SetInt("KnifeScoreInGame", _knifeScoreInGame);

        if (_knifeScoreInGame > PlayerPrefs.GetInt("KnifeScoreBest", 0))
        {
            PlayerPrefs.SetInt("KnifeScoreBest", _knifeScoreInGame);
        }
    }

    public void IncreaseAppleScore()
    {
        _appleScore = PlayerPrefs.GetInt("AppleScore", 0);

        _appleScore++;

        _appleScoreText.text = _appleScore.ToString();

        PlayerPrefs.SetInt("AppleScore", _appleScore);
    }

   public void IncreaseStageScore()
   {
        _stageScoreInGame = PlayerPrefs.GetInt("StageScoreInGame", 1);
        _stageScoreInGame++;

        PlayerPrefs.SetInt("StageScoreInGame", _stageScoreInGame);

        if (_stageScoreInGame > PlayerPrefs.GetInt("StageScoreBest", 1))
        {
            PlayerPrefs.SetInt("StageScoreBest", _stageScoreInGame);
        }

   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class NextLevelLoader : MonoBehaviour
{
    public static NextLevelLoader Instance;

    [SerializeField] private LevelsConfigurator _levelsConfigurator;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Load()
    {
        StartCoroutine(LoadNextLevel());
    }

    public void LoadFirstly()
    {
        Game.Load(_levelsConfigurator);
    }

    public void Restart()
    {
        PlayerPrefs.DeleteKey("KnifeScoreInGame");
        PlayerPrefs.DeleteKey("StageScoreInGame");

        Game.Load(_levelsConfigurator);
    }

    private IEnumerator LoadNextLevel()
    {
        PlayerStatistics.Instance.IncreaseStageScore();

        yield return new WaitForSecondsRealtime(1f);

        Game.Load(_levelsConfigurator);
    }
}

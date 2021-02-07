using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void Pressed()
    {
        SoundsPlayer.Instance.PlayAndRestartButtonSoundPlay();
        NextLevelLoader.Instance.LoadFirstly();
    }
}

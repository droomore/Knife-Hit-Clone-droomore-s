﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class HomeButton : MonoBehaviour
{
    public void Pressed()
    {
        SoundsPlayer.Instance.HomeButtonSoundPlay();
        MainMenu.Load();
    }
}

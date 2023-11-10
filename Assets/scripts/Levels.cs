using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels
{
    public static int currentLevel = 0;

    public static string[] levels = {
        "MainGame",
        "Level1"
    };

    public static float[] timeInLevels = {
        60f*3f, // 3min
        60f*0.1f // 1.5min
    };
}

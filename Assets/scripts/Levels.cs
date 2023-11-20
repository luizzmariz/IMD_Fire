using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels
{
    public static int currentLevel = 0;

    public static string[] levels = {
        "MainGame",
        "Level1",
        "Level2",
        "TheEnd"
    };

    public static float[] timeInLevels = {
        60f*3f, // 3min
        60f*1.5f, // 1.5min
        60f*2f, // 2min
        60f
    };
}

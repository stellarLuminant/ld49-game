using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static string TitleScreenScene = "TitleScreen";
    public static string StoryStartScene = "StoryStart";
    public static string StoryEndScene = "StoryEnd";
    public static string[] LevelScenes = { "01.Tutorial", "02.Tree", "03.Passages" };

    public static int CurrentLevel = 0;
}

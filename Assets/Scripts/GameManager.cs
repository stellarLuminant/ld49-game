using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static string TitleScreenScene = "TitleScreen";
    public static string StoryStartScene = "StoryStart";
    public static string StoryEndScene = "StoryEnd";
    public static string[] LevelScenes = { 
        "01.Tutorial", "02.Tree", "03.Passages", "04.Tunnel", "05.Stairs", "06.Blockade" };

    public static int CurrentLevel = 0;
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelNameText : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    private static Dictionary<Int32, String> LevelNumberToName = new Dictionary<Int32, String>() {
        { 0, "Clover" },
        { 1, "Tree" },
        { 2, "Passages" },
        { 3, "Tunnel" },
        { 4, "Stairs" }
    };

    private static String GetLevelName(Int32 levelNumber)
    {
        if (!LevelNumberToName.TryGetValue(levelNumber, out String levelName))
            levelName = "Unknown";
        
        Debug.Log("Got Level Name: " + levelName);

        return levelName;
    }

    public void ShowText()
    {
        var levelContext = FindObjectOfType<LevelContext>();
        if (levelContext == null)
        {
            gameObject.SetActive(false);
            return;
        }

        // TODO: Fix this.
        // When this code is loaded, it's still in the previous level
        // so in order for it to update to the next level's text, it needs to pass in
        // levelNumber + 1. which is not exactly super safe.
        textComponent.SetText(GetLevelName(levelContext.LevelNumber + 1));
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

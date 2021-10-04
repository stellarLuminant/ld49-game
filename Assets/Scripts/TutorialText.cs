using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour
{
    public void Logic()
    {
        var levelContext = FindObjectOfType<LevelContext>();
        bool shouldShow = levelContext ? levelContext.LevelNumber == 0 : false;
        gameObject.SetActive(shouldShow);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Logic();
    }

    // Start is called before the first frame update
    void Start()
    {
        Logic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

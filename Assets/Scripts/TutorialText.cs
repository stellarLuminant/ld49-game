using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour
{
    public void ShowText(bool forceHide = false)
    {
        var levelContext = FindObjectOfType<LevelContext>();
        bool shouldShow = levelContext ? levelContext.LevelNumber == 0 : false;

        if (forceHide) shouldShow = false;
        gameObject.SetActive(shouldShow);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

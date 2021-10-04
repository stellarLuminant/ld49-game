using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContext : MonoBehaviour
{
    public int LevelNumber = 1;
    public float FadeInTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.CurrentLevel = LevelNumber;
    }
}

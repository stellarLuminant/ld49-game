using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnStart : MonoBehaviour
{
    public MusicManager.MusicState State;

    private void Start()
    {
        var musicManager = MusicManager.Instance;
        if (musicManager.State != State)
            musicManager.State = State;
    }
}

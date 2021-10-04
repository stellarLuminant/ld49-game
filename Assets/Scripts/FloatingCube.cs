using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCube : MonoBehaviour
{
    public string AnimName = "cube 1-6";

    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.Play(AnimName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

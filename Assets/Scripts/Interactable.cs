using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual bool Interact(Vector3 direction) 
    {
        return false;
    }
}

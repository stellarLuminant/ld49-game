using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Comes from:
/// https://gamedev.stackexchange.com/questions/119734/unity-order-in-z-layer-for-objects
/// </summary>
public class SpritePositionSetter : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        SetPosition();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        SetPosition();
    }

    void SetPosition()
    {
        // If you want to change the transform, use this
        Vector3 newPosition = transform.position;
        newPosition.z = transform.position.y;
        transform.position = newPosition;

        // Or if you want to change the SpriteRenderer's sorting order, use this
        //if (!spriteRenderer) Start();
        //spriteRenderer.sortingOrder = (int)transform.position.y;
    }
}
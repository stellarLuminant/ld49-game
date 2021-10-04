using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool On;

    //public SpriteRenderer GlowSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        On = false;
    }

    private void Update()
    {
        //if (On)
        //{
        //    GlowSpriteRenderer.enabled = true;
        //} else
        //{
        //    GlowSpriteRenderer.enabled = false;
        //}
    }

    private void FixedUpdate()
    {
        On = Utils.CastForObjectOnTile(Utils.ToGridPosition(transform.position)) != null;
    }
}

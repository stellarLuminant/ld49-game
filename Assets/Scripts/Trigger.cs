using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool On;

    // Start is called before the first frame update
    void Start()
    {
        On = false;
    }

    private void FixedUpdate()
    {
        On = Utils.CastForObjectOnTile(Utils.ToGridPosition(transform.position)) != null;
    }
}

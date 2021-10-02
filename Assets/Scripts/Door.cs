using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Triggerable
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Code that runs on the frame when all triggers turn on.
    public override void OnTriggerActivate() 
    {
        // TODO: Open the door, change sprite.
    }

    // Code that runs on teh frame when any trigger turns off.
    public override void OnTriggerDeactivate()
    {
        // TODO: Close the door, change sprite.
    }

    // Code that runs for every frame that all triggers are on.
    public override void OnTriggerOn()
    {
        // TODO: Check for player and if player is on top, win level.
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour
{
    private const Int32 ACTIVATE_FRAME_COOLDOWN = 8;
    
    public Int32 ActivateFrames = 0;
    public Int32 DeactivateFrames = 0;
    public bool Activated;
    public bool TriggersOn;
    public Trigger[] Triggers;

    // Start is called before the first frame update
    private void Start()
    {
        TriggersOn = false;
    }

    private void TickFrameCooldown()
    {
        if (!Activated && ActivateFrames > 0)
            ActivateFrames -= 1;
        if (Activated && DeactivateFrames > 0)
            DeactivateFrames -= 1;
    }

    private void CheckTriggers()
    {
        TriggersOn = true;

        foreach (Trigger t in Triggers)
        {
            if (!t.On)
            {
                TriggersOn = false;
                break;
            }
        }
    }

    private void RunTriggerable()
    {
        if (TriggersOn) {
            if (Activated) {
                OnTriggerOn();
                return;
            }

            if (ActivateFrames > 0)
                return;

            Activated = true;
            ActivateFrames = ACTIVATE_FRAME_COOLDOWN;
            OnTriggerActivate();
        }
        else {
            if (!Activated) {
                OnTriggerOff();
                return;
            }

            if (DeactivateFrames > 0)
                return;

            Activated = false;
            DeactivateFrames = ACTIVATE_FRAME_COOLDOWN;
            OnTriggerDeactivate();
        }
    }

    private void FixedUpdate()
    {
        TickFrameCooldown();
        CheckTriggers();
        RunTriggerable();
    }

    // Code that runs on the frame when all triggers turn on.
    public virtual void OnTriggerActivate() {}

    // Code that runs on teh frame when any trigger turns off.
    public virtual void OnTriggerDeactivate() {}

    // Code that runs for every frame that all triggers are on.
    public virtual void OnTriggerOn() {}

    // Code that runs for every frame that any trigger is off.
    public virtual void OnTriggerOff() {}
}

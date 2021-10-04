using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour
{
    public bool On;
    public bool OnLastFrame;
    public Trigger[] Triggers;

    // Start is called before the first frame update
    private void Start()
    {
        On = false;
        OnLastFrame = false;
    }

    private void CheckTriggers()
    {
        OnLastFrame = On;
        On = true;

        foreach (Trigger t in Triggers)
        {
            if (!t.On)
            {
                //Debug.Log("One of the triggers was off");
                On = false;
                break;
            }
        }
    }

    private void RunTriggerable()
    {
        if (On)
        {
            if (OnLastFrame)
                OnTriggerOn();
            else
                OnTriggerActivate();

            return;
        }

        if (OnLastFrame)
        {
            OnTriggerDeactivate();
            return;
        }

        OnTriggerOff();
    }

    private void FixedUpdate()
    {
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

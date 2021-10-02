using System;
using UnityEngine;

public class Box : Interactable
{
    // Time it takes for the box to move to its destination.
    public Single PushTime = 0.5f;

    // Number of tiles the box gets pushed. Please keep to integer values.
    public Int32 PushDistance = 1;

    // FixedTime when box reaches its destination.
    public Single PushEndTime;
    
    public Vector3 PushDestination;

    private Single PushVelocityX;
    private Single PushVelocityY;

    public Rigidbody2D Rigidbody;

    public override bool Interact(Vector3 direction)
    {
        // Still currently being pushed?
        if (Time.fixedTime < PushEndTime)
            return false;

        Vector3 destination = Utils.ToGridPosition(transform.position) + direction;

        // The destination is not an empty tile...
        if (!Utils.IsTileEmpty(destination))
            return false;
        
        PushEndTime = Time.fixedTime + PushTime;
        PushDestination = destination;
 
        return true;
    }

    private void Start()
    {
        PushEndTime = 0;
        PushDestination = transform.position;
        PushVelocityX = 0;
        PushVelocityY = 0;
    }

    private void UpdateMovement(Single time)
    {
        var diff = PushDestination - transform.position;

        if (diff == Vector3.zero)
            return;
        
        var x = Mathf.SmoothDamp(transform.position.x, PushDestination.x, ref PushVelocityX, PushTime, Mathf.Infinity, time);
        var y = Mathf.SmoothDamp(transform.position.y, PushDestination.y, ref PushVelocityY, PushTime, Mathf.Infinity, time);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    private void FixedUpdate()
    {
        UpdateMovement(Time.fixedDeltaTime);
    }
}

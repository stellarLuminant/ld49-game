using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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

    private Rigidbody2D Rigidbody;

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

        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void UpdateMovement(Single time)
    {
        var pushDest = new Vector2(PushDestination.x, PushDestination.y);
        var diff = pushDest - Rigidbody.position;

        if (diff == Vector2.zero)
            return;
        
        var x = Mathf.SmoothDamp(Rigidbody.position.x, pushDest.x, ref PushVelocityX, PushTime, Mathf.Infinity, time);
        var y = Mathf.SmoothDamp(Rigidbody.position.y, pushDest.y, ref PushVelocityY, PushTime, Mathf.Infinity, time);

        Rigidbody.position = new Vector2(x, y);
    }

    private void FixedUpdate()
    {
        UpdateMovement(Time.fixedDeltaTime);
    }
}

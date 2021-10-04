using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : Triggerable
{
    public Sprite LockedSprite;
    public Sprite UnlockedSprite;

    public Color LockedColor = new Color(0, 0, 0, .5f);
    public Color UnlockedColor = new Color(1, 1, 1, .8f);

    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = LockedSprite;
    }

    // Code that runs on the frame when all triggers turn on.
    public override void OnTriggerActivate() 
    {
        //_renderer.sprite = UnlockedSprite;
        _renderer.color = UnlockedColor;
        // TODO: Open the door, change sprite.
    }

    // Code that runs on teh frame when any trigger turns off.
    public override void OnTriggerDeactivate()
    {
        //_renderer.sprite = LockedSprite;
        _renderer.color = LockedColor;
        // TODO: Close the door, change sprite.
    }

    // Code that runs for every frame that all triggers are on.
    public override void OnTriggerOn()
    {
        // TODO: Check for player and if player is on top, win level.
    }
}

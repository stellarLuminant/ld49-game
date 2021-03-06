using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region State

    // Tiles per second.
    public Single MoveSpeed = 1;

    // Seconds after a successful interact before it can be used again.
    public Single SuccessfulInteractCooldown = 1;

    // FixedTime when the player is able to interact again.
    public Single TimeToNextInteract;

    // The current direction the character is looking at.
    public Vector3 LookDirection;

    // Multiple inputs we can allow.
    public KeyCode[] Input_MoveUp;
    public KeyCode[] Input_MoveDown;
    public KeyCode[] Input_MoveLeft;
    public KeyCode[] Input_MoveRight;
    public KeyCode[] Input_Interact;
    public KeyCode[] Input_Restart = { KeyCode.R };
    public KeyCode[] Input_I_Give_Up = { KeyCode.End };

    // The transform of the interaction cursor.
    public Transform InteractCursor;

    private Rigidbody2D Rigidbody;

    private Animator Animator;

    // The position the player was last frame.
    private Vector2 OldPosition;

    public bool IsInTutorial;

    public bool CanMove = true;

    #endregion State

    #region Helpers

    public Vector3 GridPos => Utils.ToGridPosition(transform.localPosition);

    private bool IsMovingUp => Utils.CheckInputsHeld(Input_MoveUp);

    private bool IsMovingDown => Utils.CheckInputsHeld(Input_MoveDown);

    private bool IsMovingLeft => Utils.CheckInputsHeld(Input_MoveLeft);

    private bool IsMovingRight => Utils.CheckInputsHeld(Input_MoveRight);

    private bool IsInteracting => Utils.CheckInputsPressed(Input_Interact);

    private bool IsRestarting => Utils.CheckInputsPressed(Input_Restart);

    private Vector3 GetVerticalMoveDirection()
    {
        bool up = IsMovingUp;
        bool down = IsMovingDown;

        if (up)
        {
            return down
                ? Vector3.zero
                : new Vector3(0, 1, 0);
        }

        if (down)
            return new Vector3(0, -1, 0);

        return Vector3.zero;
    }

    private Vector3 GetHorizontalMoveDirection()
    {
        bool left = IsMovingLeft;
        bool right = IsMovingRight;

        if (left)
        {
            return right
                ? Vector3.zero
                : new Vector3(-1, 0, 0);
        }

        if (right)
            return new Vector3(1, 0, 0);
        
        return Vector3.zero;
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 direction = GetVerticalMoveDirection() + GetHorizontalMoveDirection();

        if (!CanMove) return Vector3.zero;

        return direction == Vector3.zero
            ? Vector3.zero
            : Vector3.Normalize(direction);
    }

    private Vector3 GetInteractCursorPosition()
    {
        return GridPos + Utils.ToGridPosition(LookDirection);
    }

    // Sets the LookDirection to MoveDirection if it is non-zero and non-diagonal
    private void SetLookDirection(Vector3 moveDir)
    {
        if (moveDir == Vector3.zero)
            return;

        // Animator should know the direction the player is going, including diagonals
        Animator.SetFloat("Horizontal", moveDir.x);
        Animator.SetFloat("Vertical", moveDir.y);

        // ignore if movement is diagonal
        if (moveDir.x != 0 && moveDir.y != 0)
            return;

        LookDirection = moveDir;
        InteractCursor.position = GetInteractCursorPosition();
    }

    private Interactable CheckInteractable(Vector3 pos)
    {
        return Utils.CastForObjectOnTile(pos)?.GetComponent<Interactable>();
    }

    private bool IsApproximately(Vector2 a, Vector2 b, float range = 0.001f)
    {
        return IsApproximately(a.x, b.x) && IsApproximately(a.y, b.y);
    }

    private bool IsApproximately(float a, float b, float range = 0.001f)
    {
        return Math.Abs(b - a) < range;
    }

    #endregion Helpers

    #region Unity Behaviour

    // Start is called before the first frame update
    private void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        // Player looks down on init.
        SetLookDirection(new Vector3(0, -1, 0));
    }

    private void UpdateMovement()
    {
        OldPosition = Rigidbody.position;
        Vector3 moveDir = GetMoveDirection();
        Rigidbody.velocity = moveDir * MoveSpeed;
        SetLookDirection(moveDir);
    }

    private void UpdateAnimation()
    {
        Vector3 moveDir = GetMoveDirection();
        if (moveDir == Vector3.zero)
        {
            Animator.Play("Idle");
            return;
        }

        if (IsApproximately(OldPosition, Rigidbody.position))
        {
            Animator.Play("Push");
            return;
        }

        Animator.Play("Movement");

        if (IsInTutorial)
        {
            Tutorial.CompletedMovement = true;
        }
    }

    private void UpdateInteraction(Single time)
    {
        if (!CanMove)
            return;

        if (time < TimeToNextInteract)
            return;
        
        if (!IsInteracting)
            return;
        
        if (CheckInteractable(GetInteractCursorPosition())?.Interact(LookDirection) == true)
            TimeToNextInteract = time + SuccessfulInteractCooldown;
    }

    private void UpdateOtherKeys()
    {
        if (!CanMove)
            return;

        if (IsRestarting)
        {
            var uIManager = UIManager.Instance;
            var fadeManager = uIManager.FadeManager;
            fadeManager.FadeIn(uIManager.StartFadeTime, Color.black);
            SceneManager.LoadScene(GameManager.LevelScenes[GameManager.CurrentLevel]);
        }

        if (Utils.CheckInputsPressed(Input_I_Give_Up))
        {
            FindObjectOfType<Door>().LoadNewLevel(this);
        }
    }

    // FixedUpdate is called once per physics update
    private void FixedUpdate()
    {
        UpdateMovement();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateAnimation();
        UpdateInteraction(Time.time);
        UpdateOtherKeys();
    }

    #endregion Unity Behaviour
}

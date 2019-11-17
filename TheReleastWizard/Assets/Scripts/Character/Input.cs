using UnityEngine;

public class Input : MonoBehaviour
{
    PlayerInput pc;

    public bool WPressed
    {
        protected set;
        get;
    }

    public bool SPressed
    {
        protected set;
        get;
    }

    public bool APressed
    {
        protected set;
        get;
    }

    public bool DPressed
    {
        protected set;
        get;
    }
    public bool SprintToggle
    {
        protected set;
        get;
    }

    public bool SpacePressed
    {
        protected set;
        get;
    }

    public bool primaryPressed
    {
        protected set;
        get;
    }

    public bool secondaryPressed
    {
        protected set;
        get;
    }

    public Vector2 mouseDelta
    {
        protected set;
        get;
    }

    private void OnEnable()
    {
        pc.Player.Enable();
    }

    private void OnDisable()
    {
        pc.Player.Disable();
    }

    void Awake()
    {
        pc = new PlayerInput();

        pc.Player.Up.performed += ctx => UpPressed(true);
        pc.Player.Up.canceled += ctx => UpPressed(false);

        pc.Player.Down.performed += ctx => DownPressed(true);
        pc.Player.Down.canceled += ctx => DownPressed(false);

        pc.Player.Right.performed += ctx => RightPressed(true);
        pc.Player.Right.canceled += ctx => RightPressed(false);

        pc.Player.Left.performed += ctx => LeftPressed(true);
        pc.Player.Left.canceled += ctx => LeftPressed(false);

        pc.Player.Sprint.performed += ctx => ToggleSprint();

        pc.Player.Jump.performed += ctx => JumpPressed(true);
        pc.Player.Jump.canceled += ctx => JumpPressed(false);

        pc.Player.PrimaryFire.performed += ctx => PrimaryFirePressed(true);
        pc.Player.PrimaryFire.canceled += ctx => PrimaryFirePressed(false);

        pc.Player.SecondaryFire.performed += ctx => SecondaryFirePressed(true);
        pc.Player.SecondaryFire.canceled += ctx => SecondaryFirePressed(false);

        pc.Player.MouseMovement.performed += ctx => mouseDelta = ctx.ReadValue<Vector2>();
        pc.Player.MouseMovement.canceled += ctx => mouseDelta = Vector2.zero;
    }
    
    void UpPressed(bool val)
    {
        WPressed = val;
    }

    void DownPressed(bool val)
    {
        SPressed = val;
    }

    void RightPressed(bool val)
    {
        DPressed = val;
    }

    void LeftPressed(bool val)
    {
        APressed = val;
    }

    void ToggleSprint()
    {
        SprintToggle = !SprintToggle;
    }

    void JumpPressed(bool val)
    {
        SpacePressed = val;
    }

    void PrimaryFirePressed(bool pressed)
    {
        primaryPressed = pressed;
    }

    void SecondaryFirePressed(bool pressed)
    {
        secondaryPressed = pressed;
    }


}

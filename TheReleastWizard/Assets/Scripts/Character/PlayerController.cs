using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // == public == //
    public int walkSpeed = 20;

    public int runModifier = 2;

    public Camera camera;

    // == private == //
    Rigidbody rb;

    PlayerInput pc;

    CapsuleCollider col;

    private bool WPressed = false;
    private bool SPressed = false;
    private bool APressed = false;
    private bool DPressed = false;
    private bool SprintToggle = false;
    private bool SpacePressed = false;

    // For directional inputs
    Vector3 camForward;
    Vector3 camRight;

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
        rb = GetComponent<Rigidbody>();

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

        pc.Player.Jump.performed += ctx => JumpPressed();
    }

    void FixedUpdate()
    {
        camForward = camera.transform.forward;
        camForward.y = 0;
        camForward = camForward.normalized;
        camRight = camera.transform.right;
        camRight.y = 0;
        camRight = camRight.normalized;

        Vector3 movement = new Vector3();
        movement.y = rb.velocity.y;

        if (WPressed)
            movement.z += walkSpeed * 10 * Time.fixedDeltaTime * (SprintToggle ? runModifier : 1);
        if (SPressed)
            movement.z -= walkSpeed * 10 * Time.fixedDeltaTime * (SprintToggle ? runModifier : 1);

        if (APressed)
            movement.x -= walkSpeed * 10 * Time.fixedDeltaTime * (SprintToggle ? runModifier : 1);
        if (DPressed)
            movement.x += walkSpeed * 10 * Time.fixedDeltaTime * (SprintToggle ? runModifier : 1);

        if(JumpPressed && IsGrounded())
            movement.y += 

        movement = movement.z * camForward + camRight * movement.x;
        movement = movement.normalized;

        rb.velocity = movement;
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

    void JumpPressed()
    {
        SpacePressed = true;
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), 0.18f, groundLayers);
    }

    // == DEBUG == //
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, (transform.forward + new Vector3(0,1,1)), Color.red);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // == public == //
    public int walkSpeed = 20;

    // == private == //
    Rigidbody rb;

    PlayerInput pc;

    private bool WPressed = false;
    private bool SPressed = false;
    private bool APressed = false;
    private bool DPressed = false;
    private bool SprintToggle = false;

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
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3();
        movement.y = rb.velocity.y;

        if (WPressed)
            movement.z += walkSpeed * 10 * Time.fixedDeltaTime;
        if (SPressed)
            movement.z -= walkSpeed * 10 * Time.fixedDeltaTime;

        if (APressed)
            movement.x -= walkSpeed * 10 * Time.fixedDeltaTime;
        if (DPressed)
            movement.x += walkSpeed * 10 * Time.fixedDeltaTime;

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

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, (transform.forward + new Vector3(0,1,1)), Color.red);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // == public == //
    public int walkSpeed = 20;

    public int runModifier = 2;

    public int jumpHeight = 10;

    public Camera camera;

    public GameObject primaryAttack;
    public GameObject secondaryAttack;

    public float primaryMaxCool = 0.25f, secondMaxCool = 5.0f;

    [Tooltip("Layers to check for jumping.")]
    [SerializeField] LayerMask groundLayers;

    // == private == //
    Rigidbody rb;

    PlayerInput pc;

    Collider col;

    private bool WPressed = false;
    private bool SPressed = false;
    private bool APressed = false;
    private bool DPressed = false;
    private bool SprintToggle = false;
    private bool SpacePressed = false;

    private bool primaryPressed = false;
    private bool secondaryPressed = false;

    // For directional inputs
    Vector3 camForward;
    Vector3 camRight;

    float primaryCoolDown, secondCoolDown;

    private void OnEnable()
    {
        pc.Player.Enable();
    }

    private void OnDisable()
    {
        pc.Player.Disable();
    }

    Vector2 mouseDelta;

    void Awake()
    {
        col = GetComponent<Collider>();
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

        pc.Player.PrimaryFire.performed += ctx => PrimaryFirePressed(true);
        pc.Player.PrimaryFire.canceled += ctx => PrimaryFirePressed(false);

        pc.Player.SecondaryFire.performed += ctx => SecondaryFirePressed(true);
        pc.Player.SecondaryFire.canceled += ctx => SecondaryFirePressed(false);

        pc.Player.MouseMovement.performed += ctx => mouseDelta = ctx.ReadValue<Vector2>();
        pc.Player.MouseMovement.canceled += ctx => mouseDelta = Vector2.zero;
    }

    private void Update()
    {
        if (primaryCoolDown != 0)
            primaryCoolDown += Time.deltaTime;
        if (secondCoolDown != 0)
            secondCoolDown += Time.deltaTime;

        if (primaryCoolDown >= primaryMaxCool)
            primaryCoolDown = 0;
        if (secondCoolDown >= secondMaxCool)
            secondCoolDown = 0;

        if (primaryCoolDown == 0 && primaryPressed)
        {
            GameObject g = Instantiate(primaryAttack, transform.position + transform.forward * 1.5f, Quaternion.identity);
            g.GetComponent<Missile>().direction = camera.transform.forward;
            primaryCoolDown += 0.0001f;
        }

        if (secondCoolDown == 0 && secondaryPressed)
        {
            GameObject g = Instantiate(secondaryAttack, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
            secondCoolDown += 0.0001f;
        }
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

        if (WPressed)
            movement.z += walkSpeed * 10 * Time.fixedDeltaTime * (SprintToggle ? runModifier : 1);
        if (SPressed)
            movement.z -= walkSpeed * 10 * Time.fixedDeltaTime * (SprintToggle ? runModifier : 1);

        if (APressed)
            movement.x -= walkSpeed * 10 * Time.fixedDeltaTime * (SprintToggle ? runModifier : 1);
        if (DPressed)
            movement.x += walkSpeed * 10 * Time.fixedDeltaTime * (SprintToggle ? runModifier : 1);

        movement = movement.z * camForward + camRight * movement.x;

        transform.forward = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);

        movement = movement.normalized;

        if (SpacePressed && IsGrounded())
        {
            SpacePressed = false;
            rb.AddForce(transform.up * jumpHeight, ForceMode.Acceleration);
        }

        movement.y = rb.velocity.y;

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

    void PrimaryFirePressed(bool pressed)
    {
        primaryPressed = pressed;
    }

    void SecondaryFirePressed(bool pressed)
    {
        secondaryPressed = pressed;
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), 0.18f, groundLayers);
    }

}

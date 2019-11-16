using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // == public == //
    public int walkSpeed = 20;

    public int runModifier = 2;

    public int jumpHeight = 10;

    public Camera _camera;

    public GameObject primaryAttack;
    public GameObject secondaryAttack;

    public float primaryMaxCool = 0.25f, secondMaxCool = 5.0f;

    [Tooltip("Layers to check for jumping.")]
    [SerializeField] LayerMask groundLayers;

    // == private == //
    Collider col;

    CharacterController controller;

    [SerializeField] Input input;

    // For directional inputs
    Vector3 camForward;
    Vector3 camRight;

    float primaryCoolDown, secondCoolDown;

    void Awake()
    {
        col = GetComponent<Collider>();

        controller = GetComponent<CharacterController>();
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

        if (primaryCoolDown == 0 && input.primaryPressed)
        {
            GameObject g = Instantiate(primaryAttack, transform.position + transform.forward * 1.5f, Quaternion.identity);
            g.GetComponent<Missile>().direction = _camera.transform.forward;
            primaryCoolDown += 0.0001f;
        }

        if (secondCoolDown == 0 && input.secondaryPressed)
        {
            GameObject g = Instantiate(secondaryAttack, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
            secondCoolDown += 0.0001f;
        }
    }

    void FixedUpdate()
    {
        camForward = _camera.transform.forward;
        camForward.y = 0;
        camForward = camForward.normalized;
        camRight = _camera.transform.right;
        camRight.y = 0;
        camRight = camRight.normalized;

        Vector3 movement = new Vector3();

        if (input.WPressed)
            movement.z += walkSpeed * Time.fixedDeltaTime * (input.SprintToggle ? runModifier : 1);
        if (input.SPressed)
            movement.z -= walkSpeed * Time.fixedDeltaTime * (input.SprintToggle ? runModifier : 1);

        if (input.APressed)
            movement.x -= walkSpeed * Time.fixedDeltaTime * (input.SprintToggle ? runModifier : 1);
        if (input.DPressed)
            movement.x += walkSpeed * Time.fixedDeltaTime * (input.SprintToggle ? runModifier : 1);

        movement = movement.z * camForward + camRight * movement.x;

        if (input.SpacePressed && IsGrounded())
        {
            movement.y = jumpHeight;
        }

        transform.forward = new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z);

        controller.Move(movement);


    }
    
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), 0.18f, groundLayers);
    }

}

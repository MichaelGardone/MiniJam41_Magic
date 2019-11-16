using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensitivityX = 0.005f;
    public float sensitivityY = 0.005f;

    public float distY = 4;
    public float distZ = 12.0f;

    [SerializeField] Transform target;

    PlayerInput pc;

    Vector2 mouseDelta;

    float velX = 0;
    float velY = 0;

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
        Cursor.lockState = CursorLockMode.Locked;
        pc = new PlayerInput();

        pc.Player.MouseMovement.performed += ctx => mouseDelta = ctx.ReadValue<Vector2>();
        pc.Player.MouseMovement.canceled += ctx => mouseDelta = Vector2.zero;

        velX = transform.eulerAngles.y;
        velY = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Horz. rotate
        velX += 20 * mouseDelta.x * distZ * sensitivityX;

        // Vert. rotate
        velY -= 20 * mouseDelta.y * sensitivityY;

        velY = ClampAngle(velY, -20, 80);
        Quaternion rot = Quaternion.Euler(velY, velX, 0);

        distZ = 12.0f;

        RaycastHit hit;
        if(Physics.Linecast(target.position, transform.position, out hit))
        {
            distZ -= hit.distance;
        }

        Vector3 negDistance = new Vector3(0, distY, -distZ);
        Vector3 pos = rot * negDistance + target.position;

        transform.rotation = rot;
        transform.position = pos;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f || angle > 360f)
            angle = 0;

        return Mathf.Clamp(angle, min, max);
    }
}

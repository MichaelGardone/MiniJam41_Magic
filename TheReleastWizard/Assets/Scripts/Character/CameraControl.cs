using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensitivityX = 0.005f;
    public float sensitivityY = 0.005f;

    public float distY = 4;
    public float distZ = 12.0f;

    [SerializeField] CentralInput input;

    [SerializeField] Transform target;

    float velX = 0;
    float velY = 0;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        velX = transform.eulerAngles.y;
        velY = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Horz. rotate
        velX += 20 * input.mouseDelta.x * distZ * sensitivityX;

        // Vert. rotate
        velY -= 20 * input.mouseDelta.y * sensitivityY;

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

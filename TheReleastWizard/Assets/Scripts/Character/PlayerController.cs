using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Entity
{
    // == public == //
    public int runModifier = 2;

    public int jumpHeight = 3;

    public float gravity = -9.81f;

    public Camera _camera;

    public GameObject primaryAttack;
    public GameObject secondaryAttack;

    public float primaryMaxCool = 0.25f, secondMaxCool = 5.0f;

    public Inventory inventory;

    [Tooltip("Layers to check for jumping.")]
    [SerializeField] LayerMask groundLayers;
    [SerializeField] CentralInput input;

    // == private == //
    Collider col;

    CharacterController controller;

    Vector3 velocity;

    // For directional inputs
    Vector3 camForward;
    Vector3 camRight;

    float primaryCoolDown, secondCoolDown;

    bool inAir;

    public int xp;

    int maxXp = 100;

    int level = 1;

    PlayerLevelUp levelUpEvent;

    void Awake()
    {
        levelUpEvent = new PlayerLevelUp();

        health = maxHealth;

        col = GetComponent<Collider>();

        controller = GetComponent<CharacterController>();

        inventory = FindObjectOfType<Inventory>();
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
            g.GetComponent<Magic>().SetOwner(this);
            primaryCoolDown += 0.0001f;
        }

        if (secondCoolDown == 0 && input.secondaryPressed)
        {
            GameObject g = Instantiate(secondaryAttack, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
            g.GetComponent<Magic>().SetOwner(this);
            secondCoolDown += 0.0001f;
        }

        if(xp >= maxXp)
        {
            xp -= maxXp;
            level++;
            levelUpEvent.Invoke(level);
            maxXp = 100 * level;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(inventory.GetInventory().Count);
            foreach (IBaubleItem item in inventory.GetInventory().Keys)
                Debug.Log(item.Name);
        }

    }

    void FixedUpdate()
    {
        inAir = !IsGrounded();

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


        transform.forward = new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z);

        controller.Move(movement);

        if (!inAir && velocity.y < 0)
            velocity.y = -2f;

        if (input.SpacePressed && !inAir)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.fixedDeltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        IBaubleItem item = other.GetComponent<IBaubleItem>();
        if (item != null)
        {
            inventory.AddBauble(item);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), 0.18f, groundLayers);
    }

    public float GetXpAsPercent()
    {
        return ((float)xp) / maxXp;
    }

    public int LevelUp()
    {
        return level;
    }

    public void AddListenerForLevelUp(UnityAction<int> listener)
    {
        levelUpEvent.AddListener(listener);
    }

}

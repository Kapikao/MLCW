using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour, IDataPersistence
{
    [Header("Ruch")]
    public float moveSpeed = 5f;        // podstawowa prędkość
    public float sprintMultiplier = 2f; // mnożnik sprintu

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private InputAction moveAction;
    private InputAction sprintAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            sprintAction = playerInput.actions["Sprint"];
        }
    }

    // Implementacja interfejsu IDataPersistence
    public void LoadData(GameData data)
    {
        transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = transform.position;
    }

    void OnEnable()
    {
        moveAction?.Enable();
        sprintAction?.Enable();
    }

    void OnDisable()
    {
        moveAction?.Disable();
        sprintAction?.Disable();
    }

    void Update()
    {
        if (moveAction != null)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            moveInput = moveValue.normalized;
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = moveSpeed;
        if (sprintAction != null && sprintAction.IsPressed())
        {
            currentSpeed *= sprintMultiplier;
        }

        rb.linearVelocity = moveInput * currentSpeed;
    }
}

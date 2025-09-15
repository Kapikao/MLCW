using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    [Header("Movement")]
    public float moveSpeed = 5f;   // bazowa szybko��
    public float sprintMultiplier = 2f; // ile razy szybciej podczas sprintu

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private InputAction moveAction;
    private InputAction sprintAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // Pobieramy PlayerInput z obiektu gracza
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            sprintAction = playerInput.actions["Sprint"]; // <-- nowa akcja Sprint
        }
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
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

        // Je�li Sprint jest wci�ni�ty ? zwi�ksz pr�dko��
        if (sprintAction != null && sprintAction.IsPressed())
        {
            currentSpeed *= sprintMultiplier;
        }

        rb.linearVelocity = moveInput * currentSpeed;
    }
}

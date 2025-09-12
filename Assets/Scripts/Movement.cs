using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f; // szybko�� poruszania

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // wy��czamy grawitacj� w 2D
    }

    void Update()
    {
        // Pobieranie wej�cia (WSAD / strza�ki)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalizacja -> dzi�ki temu poruszanie po skosie nie jest szybsze
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Fizyczne poruszanie
        rb.linearVelocity = moveInput * moveSpeed;
    }
}

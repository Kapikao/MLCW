using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel; // przypnij tutaj swój UI ekwipunku
    private InputSystem_Actions inputActions;
    private bool isOpen = false;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Inventory.performed += OnInventory;
    }

    private void OnDisable()
    {
        inputActions.Player.Inventory.performed -= OnInventory;
        inputActions.Disable();
    }

    private void OnInventory(InputAction.CallbackContext context)
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
            Time.timeScale = 0f;   // pauza
        else
            Time.timeScale = 1f;   // wznowienie gry
    }
}

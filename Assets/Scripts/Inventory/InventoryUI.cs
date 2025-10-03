using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public int width = 10;
    public int height = 6;

    private GridLayoutGroup gridLayout;
    private RectTransform rectTransform;
    private InventorySlot[,] slots; // tablica slotów

    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = width;

        slots = new InventorySlot[width, height];
    }

    private void Start()
    {
        AdjustPanelSize();
        GenerateGrid();
    }

    private void AdjustPanelSize()
    {
        Vector2 cellSize = gridLayout.cellSize;
        Vector2 spacing = gridLayout.spacing;

        float totalWidth = (cellSize.x + spacing.x) * width - spacing.x;
        float totalHeight = (cellSize.y + spacing.y) * height - spacing.y;

        rectTransform.sizeDelta = new Vector2(totalWidth, totalHeight);
    }

    private void GenerateGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject slot = Instantiate(slotPrefab, transform);
                slot.name = $"Slot_{x}_{y}";

                InventorySlot slotScript = slot.GetComponent<InventorySlot>();
                if (slotScript != null)
                {
                    slotScript.Init(this, x, y);
                    slots[x, y] = slotScript;
                }
            }
        }
    }

    public InventorySlot GetSlot(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return null;
        return slots[x, y];
    }
}

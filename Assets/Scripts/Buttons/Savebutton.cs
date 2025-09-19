using UnityEngine;
using UnityEngine.SceneManagement;

public class Savebutton : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite normalSprite;    // Domyœlny sprite
    public Sprite hoverSprite;     // Sprite po najechaniu myszk¹

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ustaw domyœlny sprite na start
        if (normalSprite != null)
            spriteRenderer.sprite = normalSprite;
    }

    void OnMouseEnter()
    {
        if (spriteRenderer != null && hoverSprite != null)
            spriteRenderer.sprite = hoverSprite;
    }

    void OnMouseExit()
    {
        if (spriteRenderer != null && normalSprite != null)
            spriteRenderer.sprite = normalSprite;
    }

}

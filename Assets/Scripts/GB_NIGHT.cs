using UnityEngine;

public class GB_NIGHT : MonoBehaviour
{
    private Renderer blockRenderer;
    private Collider2D blockCollider;
    public ShiftController shiftController;

    private readonly string targetBack = "back_night";

    void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        blockCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (shiftController == null) return;

        string currentBack = shiftController.GetCurrentBackName();
        bool isActive = (currentBack == targetBack);

        if (blockRenderer != null)
        {
            Color color = blockRenderer.material.color;
            color.a = isActive ? 1f : 0.3f;
            blockRenderer.material.color = color;
        }

        if (blockCollider != null)
            blockCollider.enabled = isActive;
    }
}

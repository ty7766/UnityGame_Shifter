using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    private Renderer blockRenderer;
    private Collider2D blockCollider;
    public ShiftController shiftController;
    void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        blockCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shiftController == null) return;
        string currentBack = shiftController.GetCurrentBackName();
        bool isActive = (currentBack == "back_sunset");

        if (blockRenderer != null)
        {
            Color color = blockRenderer.material.color;
            color.a = isActive ? 1f : 0.3f;
            blockRenderer.material.color = color;
        }
    }
}

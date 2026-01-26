using UnityEngine;

public class NPCInteractColor : Interactable
{
    private SpriteRenderer sr;
    private Color originalColor;
    private bool isGreen = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public override void Interact()
    {
        isGreen = !isGreen;
        sr.color = isGreen ? Color.green : originalColor;
    }
}

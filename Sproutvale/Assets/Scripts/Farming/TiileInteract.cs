using UnityEngine;
using UnityEngine.UI;

public class FarmTile : MonoBehaviour
{
    public RectTransform player;
    public float interactDistance = 80f;

    public float growTime = 5f; // waktu tumbuh (detik)

    RectTransform tileRect;
    Image tileImage;

    enum TileState { Empty, Planted, Grown }
    TileState state = TileState.Empty;

    float growTimer;

    Color emptyColor = new Color(0.6f, 0.4f, 0.2f); // coklat tanah
    Color plantedColor = Color.green;              // setelah ditanam
    Color grownColor = Color.blue;                 // setelah tumbuh

    void Awake()
    {
        tileRect = GetComponent<RectTransform>();
        tileImage = GetComponent<Image>();
        tileImage.color = emptyColor;
    }

    void Update()
    {
        if (player == null) return;

        HandleInteract();
        HandleGrowth();
    }

    // ================= INTERACT =================
    void HandleInteract()
    {
        float dist = Vector2.Distance(
            player.anchoredPosition,
            tileRect.anchoredPosition
        );

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            if (state == TileState.Empty)
            {
                Plant();
            }
        }
    }

    void Plant()
    {
        state = TileState.Planted;
        growTimer = 0f;
        tileImage.color = plantedColor;
        Debug.Log($"{gameObject.name} ditanami");
    }

    // ================= GROW =================
    void HandleGrowth()
    {
        if (state != TileState.Planted) return;

        growTimer += Time.deltaTime;

        if (growTimer >= growTime)
        {
            Grow();
        }
    }

    void Grow()
    {
        state = TileState.Grown;
        tileImage.color = grownColor;
        Debug.Log($"{gameObject.name} siap (biru)");
    }
}

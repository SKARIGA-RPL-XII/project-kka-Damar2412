using UnityEngine;
using UnityEngine.UI;

public class InteractTile : MonoBehaviour
{
    public static InteractTile closestTile;

    [Header("Reference")]
    public RectTransform player;
    public float interactDistance = 45f;

    [Header("Growth")]
    public float growTime = 5f;

    [Header("Harvest")]
    public int minHarvest = 3;
    public int maxHarvest = 5;

    [Header("Tile Images")]
    public Sprite grassTile;
    public Sprite soilTile;
    public Sprite plantedTile;
    public Sprite grownTile;

    RectTransform tileRect;
    Image tileImage;

    enum TileState { Grass, Soil, Planted, Grown }
    TileState state = TileState.Grass;

    float growTimer;

    void Awake()
    {
        tileRect = GetComponent<RectTransform>();
        tileImage = GetComponent<Image>();
        tileImage.sprite = grassTile;
    }

    void Update()
    {
        if (player == null) return;

        UpdateClosestTile();

        if (closestTile != this) return; // ⛔ bukan tile terdekat

        HandleHoe();
        HandlePlant();
        HandleGrowth();
        HandleHarvest();
    }

    void UpdateClosestTile()
    {
        float dist = Vector2.Distance(
            player.anchoredPosition,
            tileRect.anchoredPosition
        );

        if (dist > interactDistance) return;

        if (closestTile == null)
        {
            closestTile = this;
            return;
        }

        float closestDist = Vector2.Distance(
            player.anchoredPosition,
            closestTile.tileRect.anchoredPosition
        );

        if (dist < closestDist)
        {
            closestTile = this;
        }
    }

    void HandleHoe()
    {
        if (state == TileState.Grass && Input.GetMouseButtonDown(0))
        {
            state = TileState.Soil;
            tileImage.sprite = soilTile;
        }
    }

    void HandlePlant()
    {
        if (state == TileState.Soil && Input.GetKeyDown(KeyCode.E))
        {
            state = TileState.Planted;
            growTimer = 0f;
            tileImage.sprite = plantedTile;
        }
    }

    void HandleGrowth()
    {
        if (state != TileState.Planted) return;

        growTimer += Time.deltaTime;
        if (growTimer >= growTime)
        {
            state = TileState.Grown;
            tileImage.sprite = grownTile;
        }
    }

    void HandleHarvest()
    {
        if (state == TileState.Grown && Input.GetKeyDown(KeyCode.F))
        {
            Harvest();
        }
    }

    void Harvest()
    {
        int amount = Random.Range(minHarvest, maxHarvest + 1);
        Debug.Log($"Panen {amount} item!");

        state = TileState.Soil;
        tileImage.sprite = soilTile;
    }
}

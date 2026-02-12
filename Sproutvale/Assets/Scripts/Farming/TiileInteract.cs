using UnityEngine;

public class FarmTile : MonoBehaviour
{
    [Header("Reference")]
    public Transform player;
    public float interactDistance = 1.2f;

    [Header("Growth")]
    public float growTime = 5f;

    [Header("Harvest")]
    public int minHarvest = 3;
    public int maxHarvest = 5;

    [Header("Sprites")]
    public Sprite grassTile;
    public Sprite soilTile;
    public Sprite plantedTile;
    public Sprite grownTile;

    SpriteRenderer sr;

    enum TileState { Grass, Soil, Planted, Grown }
    TileState state = TileState.Grass;

    float growTimer;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = grassTile;
    }

    void Update()
    {
        if (!PlayerNear()) return; // 🔥 KUNCI UTAMA

        HandleHoe();
        HandlePlant();
        HandleGrowth();
        HandleHarvest();
    }

    bool PlayerNear()
    {
        return Vector2.Distance(transform.position, player.position) <= interactDistance;
    }

    // 🖱 CANGKUL
    void HandleHoe()
    {
        if (Input.GetMouseButtonDown(0) && state == TileState.Grass)
        {
            state = TileState.Soil;
            sr.sprite = soilTile;
        }
    }

    // 🌱 TANAM
    void HandlePlant()
    {
        if (Input.GetKeyDown(KeyCode.E) && state == TileState.Soil)
        {
            state = TileState.Planted;
            growTimer = 0f;
            sr.sprite = plantedTile;
        }
    }

    // ⏳ TUMBUH
    void HandleGrowth()
    {
        if (state != TileState.Planted) return;

        growTimer += Time.deltaTime;
        if (growTimer >= growTime)
        {
            state = TileState.Grown;
            sr.sprite = grownTile;
        }
    }

    // 🌾 PANEN
    void HandleHarvest()
    {
        if (Input.GetKeyDown(KeyCode.F) && state == TileState.Grown)
        {
            int amount = Random.Range(minHarvest, maxHarvest + 1);
            Debug.Log("Panen: " + amount);

            state = TileState.Soil;
            sr.sprite = soilTile;
        }
    }
}

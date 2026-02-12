using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public static bool isShopOpen = false; // ⬅️ INI WAJIB ADA

    public GameObject shopUI;
    bool playerNear = false;

    void Start()
    {
        shopUI.SetActive(false);
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            shopUI.SetActive(!shopUI.activeSelf);
            isShopOpen = shopUI.activeSelf;
        }

        if (isShopOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            shopUI.SetActive(false);
            isShopOpen = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            shopUI.SetActive(false);
            isShopOpen = false;
        }
    }
}

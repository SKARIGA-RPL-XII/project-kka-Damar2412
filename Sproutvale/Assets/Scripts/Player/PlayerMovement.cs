using UnityEngine;

public class PlayerUIMove : MonoBehaviour
{
    public float speed = 400f;
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // ⛔ STOP gerak kalau inventory terbuka
        if (InventoryUI.isOpen)
            return;

        float x = 0;
        float y = 0;

        if (Input.GetKey(KeyCode.A)) x = -1;
        if (Input.GetKey(KeyCode.D)) x = 1;
        if (Input.GetKey(KeyCode.W)) y = 1;
        if (Input.GetKey(KeyCode.S)) y = -1;

        Vector2 dir = new Vector2(x, y).normalized;
        rect.anchoredPosition += dir * speed * Time.deltaTime;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class NpcColorInteract : MonoBehaviour
{
    public RectTransform player;   // drag PLAYER IMAGE ke sini
    public float interactDistance = 80f;

    public float shakeSpeed = 25f;
    public float shakeStrength = 15f;

    RectTransform npcRect;
    Image npcImage;

    Color defaultColor;
    bool isGreen = false;
    bool shakePlayer = false;
    float timer = 0f;
    Vector2 originalPlayerPos;

    void Awake()
    {
        npcRect = GetComponent<RectTransform>();
        npcImage = GetComponent<Image>();
        defaultColor = npcImage.color;
    }

    void Update()
    {
        if (player == null) return;

        // 🔥 GERAK PAKSA PLAYER
        if (shakePlayer)
        {
            timer += Time.deltaTime * shakeSpeed;

            float x = Mathf.Sin(timer) * shakeStrength;
            player.anchoredPosition = originalPlayerPos + new Vector2(x, 0);
        }

        float dist = Vector2.Distance(
            player.anchoredPosition,
            npcRect.anchoredPosition
        );

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            Toggle();
        }
    }

    void Toggle()
    {
        isGreen = !isGreen;
        npcImage.color = isGreen ? Color.green : defaultColor;

        shakePlayer = !shakePlayer;

        if (shakePlayer)
        {
            originalPlayerPos = player.anchoredPosition;
            timer = 0f;
        }
        else
        {
            // balikin posisi normal
            player.anchoredPosition = originalPlayerPos;
        }
    }
}

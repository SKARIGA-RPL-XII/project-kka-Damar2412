using UnityEngine;
using TMPro;

public class GameTimeManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timeText;

    [Header("Day Night UI")]
    public GameObject backgroundDay;
    public GameObject backgroundNight;

    [Header("Time Settings")]
    [Tooltip("Berapa detik real untuk 1 jam game")]
    public float secondsPerHour = 3.5f;

    int hour = 6;
    int minute = 0;

    float timeAccumulator;
    bool isNight;

    void Start()
    {
        UpdateUI();
        UpdateDayNight();
    }

    void Update()
    {
        // Tambah waktu real
        timeAccumulator += Time.deltaTime;

        // Konversi ke menit game
        float secondsPerMinute = secondsPerHour / 60f;

        if (timeAccumulator >= secondsPerMinute)
        {
            timeAccumulator -= secondsPerMinute;
            AddMinute();
        }
    }

    void AddMinute()
    {
        minute++;

        if (minute >= 60)
        {
            minute = 0;
            hour++;

            if (hour >= 24)
                hour = 0;

            UpdateDayNight();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (timeText != null)
            timeText.text = $"{hour:00}:{minute:00}";
    }

    void UpdateDayNight()
    {
        bool nowNight = (hour >= 18 || hour < 6);

        if (nowNight == isNight) return;

        isNight = nowNight;

        if (backgroundDay != null)
            backgroundDay.SetActive(!isNight);

        if (backgroundNight != null)
            backgroundNight.SetActive(isNight);
    }

    // Optional helper
    public int GetHour() => hour;
    public int GetMinute() => minute;
    public bool IsNight() => isNight;
}

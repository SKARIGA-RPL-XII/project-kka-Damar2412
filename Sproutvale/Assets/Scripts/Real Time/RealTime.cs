using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayNightManager : MonoBehaviour
{
    public Image backgroundDay;
    public Image backgroundNight;
    public Image fadePanel;

    public float dayDuration = 5f;
    public float fadeDuration = 1.5f;

    float timer = 0f;
    bool isDay = true;
    bool isTransitioning = false;

    void Start()
    {
        backgroundDay.gameObject.SetActive(true);
        backgroundNight.gameObject.SetActive(false);
        fadePanel.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (isTransitioning) return;

        timer += Time.deltaTime;

        if (timer >= dayDuration)
        {
            timer = 0f;
            StartCoroutine(SwitchTime());
        }
    }

    IEnumerator SwitchTime()
    {
        isTransitioning = true;

        // fade out
        yield return StartCoroutine(Fade(0f, 1f));

        isDay = !isDay;
        backgroundDay.gameObject.SetActive(isDay);
        backgroundNight.gameObject.SetActive(!isDay);

        // fade in
        yield return StartCoroutine(Fade(1f, 0f));

        isTransitioning = false;
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, a);
            yield return null;
        }

        fadePanel.color = new Color(0, 0, 0, to);
    }
}

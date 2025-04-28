using System.Collections;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public float fadeDuration = 0.5f; // Duration of the fade effect
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing!");
        }
    }

    // Public method that can be invoked to start the fade-in effect
    public void StartFadeIn()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            StartCoroutine(FadeInCoroutine());
        }
        else
        {
            Debug.LogError("SpriteRenderer component is not assigned.");
        }
    }

    IEnumerator FadeInCoroutine()
    {
        this.enabled = true;
        float elapsedTime = 0.0f;
        Color color = spriteRenderer.color;
        color.a = 0.0f; // Start with fully transparent
        spriteRenderer.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }
    }
}

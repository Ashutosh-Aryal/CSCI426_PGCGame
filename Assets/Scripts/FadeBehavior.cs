using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBehavior : MonoBehaviour
{
    [SerializeField] private SpriteRenderer myRenderer;

    IEnumerator FadeOut()
    {
        for (float alpha = 1.0f; alpha >= 0.0f; alpha -= 0.05f)
        {
            Color currentColor = myRenderer.material.color;
            currentColor.a = alpha;
            myRenderer.material.color = currentColor;
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(gameObject);
    }

    IEnumerator FadeIn()
    {
        for (float alpha = 0.0f; alpha <= 1.0f; alpha += 0.05f)
        {
            Color currentColor = myRenderer.material.color;
            currentColor.a = alpha;
            myRenderer.material.color = currentColor;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine("FadeOut");
    }

    public void StartFadeIn()
    {
        StartCoroutine("FadeIn");
    }
}

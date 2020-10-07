using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBehavior : MonoBehaviour
{
    private SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

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

    public void StartFading()
    {
        StartCoroutine("FadeOut");
    }
}

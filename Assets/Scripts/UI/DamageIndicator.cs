using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public static DamageIndicator Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image image;
    [SerializeField] private float flashSpeed;

    private Coroutine fadeAway;

    public void Flash()
    {
        if (fadeAway != null)
        {
            StopCoroutine(fadeAway);
        }

        image.enabled = true;
        image.color = Color.white;
        fadeAway = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float a = 1f;

        while (a > 0f)
        {
            a -= (1.0f / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 1f, 1f, a);
            yield return null;
        }

        image.enabled = false;
    }
}

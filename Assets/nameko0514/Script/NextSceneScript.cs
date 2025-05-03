using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneScript : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    [SerializeField] private string nextSceneName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeIn(0, 1, 2, null));
        }
    }

    private IEnumerator FadeIn(float startAlpha, float endAlpha, float duration, Action onComplete)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;

        SceneManager.LoadScene(nextSceneName);

        onComplete?.Invoke();
    }
}

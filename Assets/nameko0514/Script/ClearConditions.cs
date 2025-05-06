using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fujiiYuma
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ClearConditions : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        private BoxCollider2D boxCollider;

        [SerializeField] private GameObject enemyBoss;
        private bool isBossTrigger = true;

        [SerializeField] private UnityEngine.UI.Image fadeImage;

        private bool isTrigger = true;

        private void Awake()
        {
            isBossTrigger = true;

            isTrigger = true;

            boxCollider = GetComponent<BoxCollider2D>();

            if(enemyBoss == null)
            {
                isBossTrigger = false;
            }
        }

        private void Update()
        {
            if (isBossTrigger)
            {
                if(enemyBoss == null)
                {
                    StartCoroutine(FadeIn(0, 1, 2, null));
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (isTrigger)
                {
                    isTrigger = false;

                    StartCoroutine(FadeIn(0, 1, 2, null));
                }
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

            SceneManager.LoadScene(sceneName);

            onComplete?.Invoke();
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace matumoto
{
    public class TitleManager : MonoBehaviour
    {
        [Header("点滅させたいテキスト")]
        [SerializeField] Text brinkText;

        [Header("点滅間隔")]
        [SerializeField] float brinkingInterval = 0;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
             //if (Input.GetKey(KeyCode.Space){ SceneManager.LoadScene(""); }
            StartCoroutine(BrinkText());
        }

        private void Update()
        {
           // if (Input.GetKey(KeyCode.Space){ SceneManager.LoadScene(""); }
        }

        private IEnumerator BrinkText()
        {
            while (true)
            {
                yield return new WaitForSeconds(brinkingInterval);
               brinkText.enabled = !brinkText.enabled;
            }
        }
    }
}
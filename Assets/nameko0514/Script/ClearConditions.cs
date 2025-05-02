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

        private void Awake()
        {
            isBossTrigger = true;

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
                    SceneManager.LoadScene(sceneName);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}

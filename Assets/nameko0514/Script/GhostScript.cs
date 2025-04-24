using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fujiiYuma
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class GhostScript : MonoBehaviour
    {
        [Header("----追尾の速度----")]
        [SerializeField] float moveSpeed = 3f;

        [Header("----playerとの最小距離----")]
        [SerializeField] private float minDistance = 0.1f;

        //playerのtransform
        private Transform player;

        [Header("----Ghostの有/無の切り替え----")]
        [SerializeField] private float waitTime = 10f;

        [Header("----Ghostの消える/登場するspeed----")]
        [SerializeField] private float switchSpeed = 1f;

        private float time = 0f;

        private bool isChangeFlag = false;

        private CircleCollider2D circleCollider;

        GameObject playerSwitchPlayer;


        private void Awake()
        {
            circleCollider = GetComponent<CircleCollider2D>();

            playerSwitchPlayer = GameObject.FindGameObjectWithTag("SwitchPlayer")?.gameObject;
        }

        private void Update()
        {
            if (playerSwitchPlayer != null)
            {
                if (playerSwitchPlayer.CompareTag("SwitchPlayer"))
                {
                    if (playerSwitchPlayer.TryGetComponent<SwitchPlayer>(out var switchPlayer))
                    {
                        if (switchPlayer.isTrigger)
                        {
                            Debug.Log("aaaaaa");
                        }
                    }
                }
            }

            time += Time.deltaTime;

            if(time > waitTime)
            {
                time = 0f;
                if (!isChangeFlag)
                {
                    isChangeFlag = true;
                    Debug.Log("Change.a_0");
                    StartCoroutine(SwitchColor(1f, 0f, switchSpeed, null));
                }
                else
                {
                    isChangeFlag = false;
                    Debug.Log("Change.a_255");
                    StartCoroutine(SwitchColor(0f, 1f, switchSpeed, null));
                }
            }

            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            //Debug.Log(player);

            //playerが設定されていないときはなにもしない
            if (player == null) return;

            //playerとの距離の計算
            float distanceToplayer = Vector2.Distance(transform.position, player.position);

            //playerとの距離が最小距離以上の場合のみ移動
            if(distanceToplayer > minDistance)
            {
                //現在の位置からPlayerの位置に向かってLerpで移動
                transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }

        private IEnumerator SwitchColor(float startAlpha, float endAlpha, float duration, Action onComplete)
        {
            float elapsedTime = 0f;
            Color color = gameObject.GetComponent<Renderer>().material.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                gameObject.GetComponent<Renderer>().material.color = color;
                yield return null;
            }

            color.a = endAlpha;
            gameObject.GetComponent<Renderer>().material.color = color; 
            
            if(endAlpha == 0)
            {
                circleCollider.enabled = false;
            }
            else
            {
                circleCollider.enabled = true;
            }

            onComplete?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.TryGetComponent<PlayerController>(out var player))
                {
                    const int LethalDamage = 1; 
                    player.TakeDamage(LethalDamage);
                }
            }
        }
    }
}

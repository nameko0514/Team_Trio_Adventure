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
        [Header("----�ǔ��̑��x----")]
        [SerializeField] float moveSpeed = 3f;

        [Header("----player�Ƃ̍ŏ�����----")]
        [SerializeField] private float minDistance = 0.1f;

        //player��transform
        private Transform player;

        [Header("----Ghost�̗L/���̐؂�ւ�----")]
        [SerializeField] private float waitTime = 10f;

        [Header("----Ghost�̏�����/�o�ꂷ��speed----")]
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

            //player���ݒ肳��Ă��Ȃ��Ƃ��͂Ȃɂ����Ȃ�
            if (player == null) return;

            //player�Ƃ̋����̌v�Z
            float distanceToplayer = Vector2.Distance(transform.position, player.position);

            //player�Ƃ̋������ŏ������ȏ�̏ꍇ�݈̂ړ�
            if(distanceToplayer > minDistance)
            {
                //���݂̈ʒu����Player�̈ʒu�Ɍ�������Lerp�ňړ�
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

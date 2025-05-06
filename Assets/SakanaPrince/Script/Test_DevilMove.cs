using fujiiYuma;
using System.Collections;
using Takato;
using UnityEngine;

namespace matumoto
{

[RequireComponent(typeof(Rigidbody2D))]
    public class Test_DevilMove : MonoBehaviour
    {
        [Header("ステータス")]
        [SerializeField] private int maxHealth = 3;          // HP
        [SerializeField] private float moveSpeed = 2f;       // スピード
        [SerializeField] private int damage = 1;  //ダメージ
        [SerializeField] private float debuffTime = 3f; //デバフ時間

        private int currentHealth;                           // 現在のHP

        private bool stoperOfDobuleDead = false;

        [SerializeField] bool testInstaZeroLife = false;

        [SerializeField] private Camera mainCamera;

        private Transform player;

        private Rigidbody2D rb;

        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();

            rb = GetComponent<Rigidbody2D>();

            if (mainCamera == null) mainCamera = Camera.main;

        }

        private void Start()
        {
            currentHealth = maxHealth;

        }

        private void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            
        }

        private void FixedUpdate()
        {
            if (!IsObjectInCameraView())
            {
                return;
            }

            if (player == null)
            {
                return;
            }

            // プレイヤーを追尾
            if (player != null)
            {
                if(stoperOfDobuleDead == false)
                {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

                }
                else
                {
                    moveSpeed = 0;
                }
            }

            //アニメーション遷移
            SetAnime();

            //左右の見た目反転
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (testInstaZeroLife)
            {
                Die();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //layerControllerを継承したプレイヤーに当たったか
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null  && currentHealth > 0)
            {
                Debug.Log("Devil: プレイヤーにヒット！");

                player.TakeDamage(damage); // P体力を減らす

                // プレイヤーが操作反転を持っているか確認して付与
                if (player is IReverseControl1 reverseControl)　　　//IReverseControl1をScriptsフォルダーに追加してほしいです
                {
                    reverseControl.ApplyReverseControl(debuffTime);  // 反転デバフを与える
                }
            }

            //if (other.CompareTag("PlayerAttack"))
            //{
            //    if (other.TryGetComponent<BUkketTest>(out var bullet))
            //    {
            //        if (bullet != null)
            //        {
            //            currentHealth -= (int)bullet.GetDamage();
            //        }
            //    }

            //}

            Takato.BulletController bulletController = other.GetComponent<Takato.BulletController>();
            if (bulletController != null)
            {
              
                currentHealth = Mathf.Max(currentHealth - (int)bulletController.GetDamage(), 0);
                //Debug.Log("SKEED");

                if (currentHealth == 0)
                {
                    Die(); //死亡処理
                }
            }

            //BUkketTest bUkketTest = other.GetComponent<BUkketTest>();
            //if(bUkketTest != null)
            //{
            //    currentHealth = Mathf.Max(currentHealth - bUkketTest.GetDamage(), 0); 
            //    Debug.Log("SKEED");

            //    if (currentHealth == 0)
            //    {
            //        Die(); //死亡処理
            //    }
            //}

        }

  

        private void Die()
        {
            Debug.Log("Devil: 倒された！");

            if (stoperOfDobuleDead == false)
            {
            SoundManager.Instance.PlaySE(SESoundData.SE.Damage);
                stoperOfDobuleDead = true;
            StartCoroutine(KnockAnime());
            }
         
        }

        IEnumerator KnockAnime()
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.EnemyKnockOut);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
            animator.SetBool("hori", false);
            animator.SetBool("knock", true);
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        private bool IsObjectInCameraView()
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

            return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
        }

        private void SetAnime()
        {
            if (player.transform.position.x - transform.position.x >
                player.transform.position.y - transform.position.y)
            {
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                animator.SetBool("hori", true);
            }
            else
            {

                if (player.transform.position.y > transform.position.y)
                {
                    animator.SetBool("up", true);
                    animator.SetBool("down", false);
                    animator.SetBool("hori", false);
                }
                else
                {
                    animator.SetBool("up", false);
                    animator.SetBool("down", true);
                    animator.SetBool("hori", false);
                }
            }
        }
    }

    //[SerializeField] private int damage = 1;  //ダメージ
    //    [SerializeField] private float debuffTime = 3f; //デバフ時間


    //    Transform player;

    //    Animator animator;

    //    float moveSpeed = 2;



      
        //private void FixedUpdate()
        //{
        //    player = GameObject.FindGameObjectWithTag("Player")?.transform;
        //    //Debug.Log(player);

        //    //playerが設定されていないときはなにもしない
        //    if (player == null) return;

        //    //playerとの距離の計算
        //    float distanceToplayer = Vector2.Distance(transform.position, player.position);

        //    //playerとの距離が最小距離以上の場合のみ移動
        //    if (distanceToplayer > 0.1f)
        //    {
        //        //現在の位置からPlayerの位置に向かってLerpで移動
        //        transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
        //    }

    }

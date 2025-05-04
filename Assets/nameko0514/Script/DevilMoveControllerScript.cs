using UnityEngine;
using fujiiYuma;

[RequireComponent(typeof(Rigidbody2D))]
public class DevilMoveControllerScript : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] private int maxHealth = 3;          // HP
    [SerializeField] private float moveSpeed = 2f;       // スピード
    [SerializeField] private int damage = 1;  //ダメージ
    [SerializeField] private float debuffTime = 3f; //デバフ時間

    private int currentHealth;                           // 現在のHP

    private Transform player;

    private Rigidbody2D rb;

    private Animator animator;

    private Camera mainCamera;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        mainCamera = Camera.main;

        currentHealth = maxHealth;

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

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // プレイヤーを追尾
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //layerControllerを継承したプレイヤーに当たったか
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Devil: プレイヤーにヒット！");

            player.TakeDamage(damage); // P体力を減らす

            // プレイヤーが操作反転を持っているか確認して付与
            if (player is IReverseControl1 reverseControl)   //IReverseControl1をScriptsフォルダーに追加してほしいです
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

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Devil: ダメージを受けた！残りHP: " + currentHealth);


    }

    private void Die()
    {
        Debug.Log("Devil: 倒された！");
        Destroy(gameObject);
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

    private bool IsObjectInCameraView()
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }
}


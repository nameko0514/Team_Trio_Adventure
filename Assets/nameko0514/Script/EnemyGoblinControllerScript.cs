using UnityEngine;

public class EnemyGoblinControllerScript : MonoBehaviour
{
    [Header("----追尾の速度----")]
    [SerializeField] float moveSpeed = 1f;

    //[Header("----playerとの最小距離----")]
    //[SerializeField] private float minDistance = 0.1f;  //今のところ使わないけど一応

    [Header("----攻撃関連----")]
    [SerializeField] private float attackRange = 1.5f; //攻撃距離
    [SerializeField] private float attackCooldown = 1f; //クールタイム
    [SerializeField] private int damage = 1; //攻撃力


    [Header("----ゴブリンの体力----")]
    [SerializeField] private int maxHealth = 2;

    private int currentHealth;            // 現在の体力
    private float lastAttackTime = 0f;    // 最後に攻撃した時間
    private Transform player;             // プレイヤーの位置を格納

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // プレイヤーをタグで
        currentHealth = maxHealth; // 初期体力
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null) return;   //プレイヤー見つからないなら

        if (!IsObjectInCameraView())
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            //プレイヤーへ移動
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }

        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            //クールタイムが終わると攻撃再開
            Attack();
            lastAttackTime = Time.time; //攻撃時間記録
        }
    }

    private void Attack()
    {
        if (player != null && player.gameObject.activeSelf)
        {
            fujiiYuma.PlayerController playerController = player.GetComponent<fujiiYuma.PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage); //プレイヤーにダメージ
                Debug.Log("プレイヤーに攻撃！ ダメージ: " + damage);
            }
        }
    }

    public void TakeDamage(int amout)
    {
        currentHealth -= amout;
        Debug.Log("ゴブリン　残りのHP" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); // 死亡
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //攻撃が当たった時
        if (collision.CompareTag("PlayerAttack"))
        {
            TakeDamage(1); //受けたダメージ
        }

    }

    private bool IsObjectInCameraView()
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }
}


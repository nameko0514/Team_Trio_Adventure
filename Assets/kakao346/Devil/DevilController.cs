using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class DevilController : MonoBehaviour
    {
        [Header("ステータス")]
        [SerializeField] private int maxHealth = 3;          // HP
        [SerializeField] private float moveSpeed = 2f;       // スピード
        [SerializeField] private int damage = 1;  //ダメージ
        [SerializeField] private float debuffTime = 3f; //デバフ時間

        private int currentHealth;                           // 現在のHP

        private Transform player;                 

        private Rigidbody2D rb;

        private void Start()
        {
            currentHealth = maxHealth;

            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            // プレイヤーを追尾
            if (player != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
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
                if (player is IReverseControl1 reverseControl)　　　//IReverseControl1をScriptsフォルダーに追加してほしいです
                {
                    reverseControl.ApplyReverseControl(debuffTime);  // 反転デバフを与える
                }
            }
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            Debug.Log("Devil: ダメージを受けた！残りHP: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die(); //死亡処理
            }
        }

        private void Die()
        {
            Debug.Log("Devil: 倒された！");
            Destroy(gameObject);
        }

    }

}


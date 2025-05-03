using UnityEngine;
using fujiiYuma;
using System.Collections;
using Takato;

namespace Gishi
{
    public class BossController : MonoBehaviour
    {
        [Header("ボス設定")]
        [SerializeField] private float moveSpeed = 3f; // ボスの移動速度
        [SerializeField] private float attackRange = 5f; // 近距離攻撃範囲
        [SerializeField] private float shootRange = 10f; // 遠距離攻撃範囲
        [SerializeField] private float guardDuration = 2f; // ガードの持続時間
        [SerializeField] private float attackCooldown = 1f; // 攻撃のクールダウン
        [SerializeField] private float health = 20f; // ボスの体力

        [Header("攻撃設定")]
        [SerializeField] private GameObject bulletPrefab; // 弾
        [SerializeField] private Transform bulletSpawnPoint; // 弾の発射位置
        [SerializeField] private float bulletSpeed = 5f; // 弾の移動速度

        private Transform player; // プレイヤーのTransform
        private float nextAttackTime; // 次の攻撃可能時間
        private bool isGuarding = false; // ガード判断

        private Rigidbody2D rb;

        private Animator animator;  //アニメーションつけるなら

        private float lastShootTime;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            // プレイヤーが存在しない場合、何もしない
            if (player == null) return;

            // プレイヤーとの距離を計算
            float distance = Vector2.Distance(transform.position, player.position);

            // 追尾モード
            ChasePlayer();

            // プレイヤーが近距離範囲内なら近接攻撃
            if (distance <= attackRange)
            {
                MeleeAttack();
            }
            // プレイヤーが遠距離範囲内なら遠距離攻撃
            else if (distance <= shootRange && Time.time >= nextAttackTime)
            {
                ShootProjectile();
                nextAttackTime = Time.time + attackCooldown; // 次の攻撃可能時間
            }
        }

        private void ChasePlayer()
        {
            if (isGuarding) return; // ガード中は追尾しない

            // プレイヤーの位置を見て移動する
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        private void MeleeAttack()
        {
            // プレイヤーに近い場合、近接攻撃（例: プレイヤーに衝突ダメージを与える）
            Debug.Log("近接攻撃");

            // ここで近接攻撃の処理を追加（例えばアニメーションの再生など）
        }

        private void ShootProjectile()
        {
            // 弾をプレイヤー方向に発射
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed; // 弾をプレイヤー方向に発射
            }

            Debug.Log("遠距離攻撃");
        }

        public void TakeDamage(float damage)
        {
            if (isGuarding) return; // ガード中はダメージを受けない

            health -= damage;
            Debug.Log("ボスの体力: " + health);

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // 死亡時の処理
            Debug.Log("ボスが死亡した！");
            Destroy(gameObject);
        }

        public void StartGuard()
        {
            if (isGuarding) return;

            isGuarding = true;
            Debug.Log("ガード開始");

            // 一定時間後にガードを終了
            Invoke("EndGuard", guardDuration);
        }

        private void EndGuard()
        {
            isGuarding = false;
            Debug.Log("ガード終了");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーの弾に当たったか確認
            Takato.BulletController bullet = collision.GetComponent<Takato.BulletController>();
            if (bullet != null)
            {
                // 弾のダメージを取得して、自分にダメージを与える
                float damage = bullet.GetDamage();

                TakeDamage(damage);

                // 弾を削除（ボス側で破壊）
                Destroy(bullet.gameObject);

                Debug.Log("ボスが弾に当たった！ ダメージ: " + damage);
            }
        }
    }

}

using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class WaspController : MonoBehaviour
    {
        [Header("蜂の設定")]

        [SerializeField] private GameObject bulletPrefab; // 弾

        [SerializeField] private float attackCooldown = 2f; // 攻撃のクールダウン

        [SerializeField] private float bulletSpeed = 5f; // 弾の移動速度

        [SerializeField] private float attackRange = 7f; // 攻撃範囲

        private Transform player; // プレイヤーのTransform

        private float nextAttackTime; // 次の攻撃時間

        private void Start()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        private void Update()
        {
            // プレイヤーがいない場合は何もしない
            if (player == null) return;

            // プレイヤーとの距離
            float distance = Vector2.Distance(transform.position, player.position);

            // プレイヤーが攻撃範囲内かつ攻撃可能時間になったら攻撃
            if (distance <= attackRange && Time.time >= nextAttackTime)
            {
                Shoot(); // 発射

                nextAttackTime = Time.time + attackCooldown; // 次に攻撃できる時間を更新
            }
        }

        private void Shoot()
        {
            // 弾をプレイヤー方向に発射
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // プレイヤー方向のベクトルを計算
            Vector2 direction = (player.position - transform.position).normalized;

            // 弾のRigidbody2Dコンポーネントを取得して移動させる
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed; // 弾を指定した速度で発射
            }
        }
    }

}




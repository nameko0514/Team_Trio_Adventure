using matumoto;
using UnityEngine;

namespace Takato
{
    public class Player_AutoAttack : MonoBehaviour
    {
        [Header("---弾の設定---")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;

        [Header("---攻撃設定---")]
        [SerializeField] private float fireRate;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float attackRange;

        [Header("---攻撃の角度範囲---")]
        [SerializeField] private float attackAngle; // 攻撃の角度範囲

        private float nextFireTime;

        private void Update()
        {
            GameObject target = FindNearestEnemy();

            if (target != null && Time.time >= nextFireTime)
            {
                Fire(target.transform.position);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        private void Fire(Vector3 targetPosition)
        {
            Vector2 direction = transform.localScale.x < 0 ? Vector2.right : Vector2.left;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;

            bullet.transform.right = direction;
        }

        private GameObject FindNearestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject target = null;

            // プレイヤーの向きに基づいて探す方向を決める
            float directionMultiplier = transform.localScale.x > 0 ? -1f : 1f;

            // プレイヤーの向いている方向の角度範囲
            float angleRange = attackAngle;  // 設定したい角度

            foreach (var enemy in enemies)
            {
                // プレイヤーから敵への方向ベクトルを計算
                Vector2 toEnemy = enemy.transform.position - transform.position;

                // プレイヤーが向いている方向ベクトル
                Vector2 playerDirection = transform.localScale.x > 0 ? Vector2.left : Vector2.right;

                // プレイヤーの向いている方向と敵の位置ベクトルとの角度を計算
                float angle = Vector2.Angle(playerDirection, toEnemy);

                // プレイヤーの向いている方向に対して角度が範囲内であれば、発射対象にする
                if (angle <= angleRange / 2f)
                {
                    float dist = Vector2.Distance(transform.position, enemy.transform.position);
                    if (dist <= attackRange)
                    {
                        target = enemy;
                        break;
                    }
                }
            }
            return target;
        }

        //弾を撃つ範囲を可視化するためのGizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

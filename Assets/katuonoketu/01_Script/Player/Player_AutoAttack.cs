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

            foreach (var enemy in enemies)
            {
                // プレイヤーの向きに応じて、敵が前方にいるかどうかをチェック
                float distance = enemy.transform.position.x - transform.position.x;

                // プレイヤーが右向きなら正の方向、左向きなら負の方向に敵がいる場合にのみヒット
                if ((directionMultiplier < 0 && distance < 0) || (directionMultiplier > 0 && distance >0))
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

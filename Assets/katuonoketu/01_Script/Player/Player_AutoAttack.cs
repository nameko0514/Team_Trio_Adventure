using matumoto;
using UnityEngine;

namespace Takato
{
    public class Player_AutoAttack : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float attackRange = 5f;

        
        private float nextFireTime;

        //キャラの向き取得用
        private float direction = 1f;

        private void Update()
        {
            //キャラの向き取得(右向きなら1, 左向きなら - 1)
            direction = transform.localScale.x > 0 ? 1f : -1f;

            GameObject nearestEnemy = FindNerestEnemyWithTag("Enemy");

            //範囲内の敵を探す
            if (nearestEnemy != null)
            {
                float distance = Vector2.Distance(transform.position, nearestEnemy.transform.position);
                if (distance <= attackRange && Time.time >= nextFireTime)
                {
                    Fire();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }

        //弾の発射の処理
        private void Fire()
         {
            SoundManager.Instance.PlaySE(SESoundData.SE.None);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // firePoint が向いている方向に弾を飛ばす
            rb.linearVelocity = firePoint.right * 10f;

            // 弾の見た目も向き合わせたいなら
            bullet.transform.right = firePoint.right;
          }

        //タグを検知するところ
        GameObject FindNerestEnemyWithTag(string tag)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
            GameObject nearest = null;
            float minDist = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                float dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = enemy;
                }
            }

            return nearest;
        }

        //Playerのアタック範囲の表示
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

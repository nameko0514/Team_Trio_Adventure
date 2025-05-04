using matumoto;
using UnityEngine;

namespace Takato
{
    public class Player_AutoAttack : MonoBehaviour
    {
        [Header("---�e�̐ݒ�---")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;

        [Header("---�U���ݒ�---")]
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
            Vector2 direction = transform.right; // �L�����N�^�[�̉E����(�������Ă����)�Ɍ���

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;

            bullet.transform.right = direction; // �e�̌����ڂ̌��������킹��
        }

        private GameObject FindNearestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearest = null;
            float minDist = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                float dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist < minDist && dist <= attackRange)
                {
                    minDist = dist;
                    nearest = enemy;
                }
            }

            return nearest;
        }

        //�e�����͈͂��������邽�߂�Gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

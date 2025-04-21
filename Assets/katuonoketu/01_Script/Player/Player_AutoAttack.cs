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

        //�L�����̌����擾�p
        private float direction = 1f;

        private void Update()
        {
            //�L�����̌����擾(�E�����Ȃ�1, �������Ȃ� - 1)
            direction = transform.localScale.x > 0 ? 1f : -1f;

            GameObject nearestEnemy = FindNerestEnemyWithTag("Enemy");

            //�͈͓��̓G��T��
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

        //�e�̔��˂̏���
        private void Fire()
         {
            SoundManager.Instance.PlaySE(SESoundData.SE.None);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // firePoint �������Ă�������ɒe���΂�
            rb.linearVelocity = firePoint.right * 10f;

            // �e�̌����ڂ��������킹�����Ȃ�
            bullet.transform.right = firePoint.right;
          }

        //�^�O�����m����Ƃ���
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

        //Player�̃A�^�b�N�͈͂̕\��
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

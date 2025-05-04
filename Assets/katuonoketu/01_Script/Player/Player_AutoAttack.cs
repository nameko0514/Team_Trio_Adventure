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

            // �v���C���[�̌����Ɋ�Â��ĒT�����������߂�
            float directionMultiplier = transform.localScale.x > 0 ? -1f : 1f;

            foreach (var enemy in enemies)
            {
                // �v���C���[�̌����ɉ����āA�G���O���ɂ��邩�ǂ������`�F�b�N
                float distance = enemy.transform.position.x - transform.position.x;

                // �v���C���[���E�����Ȃ琳�̕����A�������Ȃ畉�̕����ɓG������ꍇ�ɂ̂݃q�b�g
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

        //�e�����͈͂��������邽�߂�Gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

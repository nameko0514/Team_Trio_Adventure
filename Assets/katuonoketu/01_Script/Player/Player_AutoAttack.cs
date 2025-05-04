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

        [Header("---�U���̊p�x�͈�---")]
        [SerializeField] private float attackAngle; // �U���̊p�x�͈�

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

            // �v���C���[�̌����Ă�������̊p�x�͈�
            float angleRange = attackAngle;  // �ݒ肵�����p�x

            foreach (var enemy in enemies)
            {
                // �v���C���[����G�ւ̕����x�N�g�����v�Z
                Vector2 toEnemy = enemy.transform.position - transform.position;

                // �v���C���[�������Ă�������x�N�g��
                Vector2 playerDirection = transform.localScale.x > 0 ? Vector2.left : Vector2.right;

                // �v���C���[�̌����Ă�������ƓG�̈ʒu�x�N�g���Ƃ̊p�x���v�Z
                float angle = Vector2.Angle(playerDirection, toEnemy);

                // �v���C���[�̌����Ă�������ɑ΂��Ċp�x���͈͓��ł���΁A���ˑΏۂɂ���
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

        //�e�����͈͂��������邽�߂�Gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

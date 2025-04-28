using matumoto;
using UnityEngine;

namespace Takato
{
    public class Player_AutoAttack : MonoBehaviour
    {
        [Tooltip("----�e�̏ڍאݒ�----")]
        [Header("---�e������Ƃ���---")]
        [SerializeField] private GameObject bulletPrefab;  //�e��Prefab

        [Header("---�e�̔��ˏꏊ�����Ƃ���---")]
        [SerializeField] private Transform firePoint;�@�@ //�e�̔��ˈʒu

        [Header("---�A�ˑ��x---")]
        [SerializeField] private float fireRate;         //�e�̔��ˊԊu(�b)

        [Header("---�U���͈͎w��---")]
        [SerializeField] private float attackRange;      //�U���͈�(���a)


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
                Vector2 toEnemy = enemy.transform.position - transform.position;
                float dist = toEnemy.sqrMagnitude;   // �����̓����v�Z

                //�v���C���[�̌����ɓG�����邩�ǂ���
                float dot = Vector2.Dot(transform.right, (enemy.transform.position - transform.position).normalized);

                if (dot < 0) continue;    // �v���C���[�̌����Ƌt�����ɓG������ꍇ�̓X�L�b�v

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

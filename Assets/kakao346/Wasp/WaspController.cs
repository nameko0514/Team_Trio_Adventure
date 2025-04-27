using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class WaspController : MonoBehaviour
    {
        [Header("�U���ݒ�")]

        [SerializeField] private GameObject bulletPrefab; // ���˂���e

        [SerializeField] private float attackCooldown = 2f; // �N�[���_�E��

        [SerializeField] private float bulletSpeed = 5f; // �e�̃X�s�[�h

        [SerializeField] private float attackRange = 7f; // �v���C���[�����m���鋗��

        private Transform player; // �v���C���[�̈ʒu���L�^

        private float nextAttackTime; // ���ɍU���ł��鎞��

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
            // �v���C���[���������Ă��Ȃ���Ή������Ȃ�
            if (player == null) return;

            // �v���C���[�Ƃ̋������v�Z
            float distance = Vector2.Distance(transform.position, player.position);

            // ��苗���� & �N�[���_�E�����I����Ă���΍U��
            if (distance <= attackRange && Time.time >= nextAttackTime)
            {
                Shoot(); // �e�𔭎�

                nextAttackTime = Time.time + attackCooldown; // ���̍U���\�������X�V
            }
        }

        private void Shoot()
        {
            // �e�v���n�u�𐶐��i�ʒu�͌��݂̖I�̈ʒu�j
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // �v���C���[�ւ̕��������߂Đ��K���i�����x�N�g���j
            Vector2 direction = (player.position - transform.position).normalized;

            // �e�� Rigidbody2D �����Ă���Α��x��ݒ�
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
        }
    }

}




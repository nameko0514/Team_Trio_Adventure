using UnityEngine;
using fujiiYuma;
using System.Collections;

namespace Gishi
{
    public class BossController : MonoBehaviour
    {
        [Header("�{�X�ݒ�")]
        [SerializeField] private float moveSpeed = 3f; // �ړ����x
        [SerializeField] private float attackRange = 5f; // �ߋ����U���͈�
        [SerializeField] private float shootRange = 10f; // �������U���͈�
        [SerializeField] private float guardDuration = 2f; // �K�[�h
        [SerializeField] private float attackCooldown = 3f; // �U���̃N�[���_�E��
        [SerializeField] private float health = 20f; // �{�X�̗̑�

        [Header("�U���ݒ�")]
        [SerializeField] private GameObject bulletPrefab; // �e
        [SerializeField] private Transform bulletSpawnPoint; // �e�̔��ˈʒu
        [SerializeField] private float bulletSpeed = 5f; // �e�̈ړ����x


        private Transform player; // �v���C���[��Transform
        private float nextAttackTime; // ���̍U���\����
        private bool isGuarding = false; // �K�[�h��Ԃ��ǂ���


        private Rigidbody2D rb;

        private Animator animator;  // �A�j���[�V����

        [Header("�U���֘A")]
        [SerializeField] private GameObject projectilePrefab;  // �������U���̒e
        [SerializeField] private float shootCooldown = 2f;  // �e�̔��ˊԊu

        private float lastShootTime;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;  // �v���C���[���^�O�ŒT��
        }
       
        private void Update()
        {
            // �v���C���[�����݂��Ȃ��ꍇ�A�������Ȃ�
            if (player == null) return;

            // �v���C���[�Ƃ̋������v�Z
            float distance = Vector2.Distance(transform.position, player.position);

            // �ǔ����[�h
            ChasePlayer();

            // �v���C���[���ߋ����͈͓��Ȃ�ߐڍU��
            if (distance <= attackRange)
            {
                MeleeAttack();
            }
            // �v���C���[���������͈͓��Ȃ牓�����U��
            else if (distance <= shootRange && Time.time >= nextAttackTime)
            {
                ShootProjectile();
                nextAttackTime = Time.time + attackCooldown; // ���̍U���\����
            }

        }

        private void ChasePlayer()
        {
            if (isGuarding) return; // �K�[�h���͒ǔ����Ȃ�

            // �v���C���[�̈ʒu�����Ĉړ�����
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        private void MeleeAttack()
        {
            // �v���C���[�ɋ߂��ꍇ�A�ߐڍU��
            Debug.Log("�ߐڍU��");
        }

        private void ShootProjectile()
        {
            // �e���v���C���[�����ɔ���
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed; // �e���v���C���[�����ɔ���
            }

            Debug.Log("�������U��");
        }

        public void TakeDamage(float damage)
        {
            if (isGuarding) return; // �K�[�h���̓_���[�W���󂯂Ȃ�

            health -= damage;
            Debug.Log("�{�X�̗̑�: " + health);

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // ���S���̏���
            Debug.Log("�{�X�����S�����I");
            Destroy(gameObject);
        }

        public void StartGuard()
        {
            if (isGuarding) return;

            isGuarding = true;
            Debug.Log("�K�[�h�J�n");

            // ��莞�Ԍ�ɃK�[�h���I��
            Invoke("EndGuard", guardDuration);
        }

        private void EndGuard()
        {
            isGuarding = false;
            Debug.Log("�K�[�h�I��");
        }
    }

}

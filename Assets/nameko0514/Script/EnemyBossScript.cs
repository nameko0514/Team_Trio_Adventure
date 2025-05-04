using UnityEngine;

namespace fujiiYuma
{
    public class EnemyBossScript : MonoBehaviour
    {
        [Header("�{�X�ݒ�")]
        [SerializeField] private float moveSpeed = 3f; // �{�X�̈ړ����x
        [SerializeField] private float attackRange = 5f; // �ߋ����U���͈�
        [SerializeField] private float shootRange = 10f; // �������U���͈�
        [SerializeField] private float guardDuration = 2f; // �K�[�h�̎�������
        [SerializeField] private float attackCooldown = 1f; // �U���̃N�[���_�E��
        [SerializeField] private float health = 20f; // �{�X�̗̑�

        [Header("�U���ݒ�")]
        [SerializeField] private GameObject bulletPrefab; // �e
        [SerializeField] private Transform bulletSpawnPoint; // �e�̔��ˈʒu
        [SerializeField] private float bulletSpeed = 5f; // �e�̈ړ����x

        private Transform player; // �v���C���[��Transform
        private float nextAttackTime; // ���̍U���\����
        private bool isGuarding = false; // �K�[�h���f

        private Rigidbody2D rb;

        private Animator animator;  //�A�j���[�V��������Ȃ�

        private float lastShootTime;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;

            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        private void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            // �v���C���[�����݂��Ȃ��ꍇ�A�������Ȃ�
            if (player == null) return;

            if (!IsObjectInCameraView())
            {
                return;
            }

            PlayerRotation();

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
            // �v���C���[�ɋ߂��ꍇ�A�ߐڍU���i��: �v���C���[�ɏՓ˃_���[�W��^����j
            Debug.Log("�ߐڍU��");

            // �����ŋߐڍU���̏�����ǉ��i�Ⴆ�΃A�j���[�V�����̍Đ��Ȃǁj
        }

        private void ShootProjectile()
        {
            // �e���v���C���[�����ɔ���
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(transform.rotation.x,transform.rotation.y,transform.rotation.z));
            Vector2 direction = (player.position - transform.position).normalized;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = bulletSpawnPoint.right * bulletSpeed; // �e���v���C���[�����ɔ���
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�̒e�ɓ����������m�F
            Takato.BulletController bullet = collision.GetComponent<Takato.BulletController>();
            if (bullet != null)
            {
                // �e�̃_���[�W���擾���āA�����Ƀ_���[�W��^����
                float damage = bullet.GetDamage();

                TakeDamage(damage);

                // �e���폜�i�{�X���Ŕj��j
                Destroy(bullet.gameObject);

                Debug.Log("�{�X���e�ɓ��������I �_���[�W: " + damage);
            }
        }

        private bool IsObjectInCameraView()
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

            return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
        }

        private void PlayerRotation()
        {
            float rotationSpeed = 5f;

            Vector3 direction = player.position - transform.position;
            direction.z = 0;

            if(direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation,rotation,rotationSpeed * Time.deltaTime);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
                {
                    const int LethalDamage = 1;
                    player.TakeDamage(LethalDamage);
                }
            }
        }
    }
}

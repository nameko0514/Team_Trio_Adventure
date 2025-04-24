using fujiiYuma;
using UnityEngine;

namespace Gishi {
    public class GoburnContolor : MonoBehaviour
    {
        [Header("----�ǔ��̑��x----")]
        [SerializeField] float moveSpeed = 3f;

        [Header("----player�Ƃ̍ŏ�����----")]
        [SerializeField] private float minDistance = 0.1f;

        [Header("----�U���֘A----")]
        [SerializeField] private float attackRange = 1.5f; //�U������
        [SerializeField] private float attackCooldown = 1f; //�N�[���^�C��
        [SerializeField] private int damage = 1; //�U����


        [Header("----�S�u�����̗̑�----")]
        [SerializeField] private int maxHealth = 5;

        private int currentHealth;            // ���݂̗̑�
        private float lastAttackTime = 0f;    // �Ō�ɍU����������
        private Transform player;             // �v���C���[�̈ʒu���i�[

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform; // �v���C���[���^�O��
            currentHealth = maxHealth; // �����̗�
        }
        private void Update()
        {
            if (player == null) return;   //�v���C���[������Ȃ��Ȃ�

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                //�v���C���[�ֈړ�
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
            }

            else if (Time.time >= lastAttackTime + attackCooldown)
            {
                //�N�[���^�C�����I���ƍU���ĊJ
                Attack();
                lastAttackTime = Time.time; //�U�����ԋL�^
            }
        }

        private void Attack()
        {
            if (player != null)
            {
                fujiiYuma.SwitchPlayer playerController = player.GetComponent<fujiiYuma.SwitchPlayer>();
                if (playerController != null)
                {
                    playerController.TakeDamage(damage); //�v���C���[�Ƀ_���[�W
                    Debug.Log("�v���C���[�ɍU���I �_���[�W: " + damage);
                }
            }
        }

        public void TakeDamage(int amout)
        {
            currentHealth -= amout;
            Debug.Log("�S�u�����@�c���HP" + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);�@// ���S
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�U��������������
            if (collision.CompareTag("PlayerAttack"))
            {
                TakeDamage(1); //�󂯂��_���[�W
            }

        }
    }
   
}

using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class DevilController : MonoBehaviour
    {
        [Header("�X�e�[�^�X")]
        [SerializeField] private int maxHealth = 3;          // HP
        [SerializeField] private float moveSpeed = 2f;       // �X�s�[�h
        [SerializeField] private int damage = 1;  //�_���[�W
        [SerializeField] private float debuffTime = 3f; //�f�o�t����

        private int currentHealth;                           // ���݂�HP

        private Transform player;                 

        private Rigidbody2D rb;

        private void Start()
        {
            currentHealth = maxHealth;

            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            // �v���C���[��ǔ�
            if (player != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //layerController���p�������v���C���[�ɓ���������
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Devil: �v���C���[�Ƀq�b�g�I");

                player.TakeDamage(damage); // P�̗͂����炷

                // �v���C���[�����씽�]�������Ă��邩�m�F���ĕt�^
                if (player is IReverseControl1 reverseControl)�@�@�@//IReverseControl1��Scripts�t�H���_�[�ɒǉ����Ăق����ł�
                {
                    reverseControl.ApplyReverseControl(debuffTime);  // ���]�f�o�t��^����
                }
            }
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            Debug.Log("Devil: �_���[�W���󂯂��I�c��HP: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die(); //���S����
            }
        }

        private void Die()
        {
            Debug.Log("Devil: �|���ꂽ�I");
            Destroy(gameObject);
        }

    }

}


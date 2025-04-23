using System.Collections;
using UnityEngine;

namespace fujiiYuma{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PlayerController : MonoBehaviour
    {
        [Header("----�̗�----")]
        [SerializeField] protected int initialHealth = 3;
        protected int health;

        [Header("----����----")]
        [SerializeField] protected float speed = 3f;

        [Header("----EnemyLayer----")]
        [SerializeField] protected LayerMask enemyLayer;

        protected Rigidbody2D rb;

        private Vector2 moveInput; //�ړ����͂��i�[����ϐ�

        private bool isInvicible = false; //�_���[�W���󂯂Ă��΂炭�̊Ԃ̓_���[�W���ʂ�Ȃ��悤�ɂ���ϐ�

        private SpriteRenderer spriteRenderer;

        private Color originalColor;

        private Color flashColor = new Color(1f, 0.5f, 0.5f, 0.4f);  //�t���b�V�����邽�߂̕ϐ�

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            health = initialHealth;

            spriteRenderer = GetComponent<SpriteRenderer>();

            originalColor = spriteRenderer.color;
        }

        protected virtual void Update()
        {
            moveInput.x = Input.GetAxis("Horizontal"); //���E�̓���
            moveInput.y = Input.GetAxis("Vertical"); //�㉺�̓���

        }

        private void FixedUpdate()
        {
            //Rigidbody2D���g���ăL�����N�^�[���ړ�
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�Փ˂����I�u�W�F�N�g��Layer���m�F
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("Player��Enemy�ƏՓ˂��܂����B");
            }
        }

        public void TakeDamage(int damage)
        {
            if (isInvicible) { return; }

            health = Mathf.Max(health - damage, 0);

            StartCoroutine(FlashCoroutine());

            if (health == 0)
            {
                //�̗�(�n�[�g)���Ȃ��Ȃ������̏���

                Destroy(gameObject);
            }
        }

        private IEnumerator FlashCoroutine(float flashDuration = 1f)
        {
            isInvicible = true;

            float elapsedTime = 0f;  //�o�ߎ���
            float flashInterval = 0.02f;  //�t���b�V������Ԋu

            while (elapsedTime < flashDuration)
            {
                //�F��؂�ւ�
                spriteRenderer.color = (spriteRenderer.color == originalColor) ? flashColor : originalColor;

                yield return new WaitForSeconds(flashInterval);

                elapsedTime += flashInterval;
            }

            spriteRenderer.color = originalColor;
            isInvicible = false;
        }
    }
}

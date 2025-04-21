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

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            health = initialHealth;
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
    }
}

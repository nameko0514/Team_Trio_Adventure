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

        protected Rigidbody2D rb;

        protected Vector2 moveInput; //�ړ����͂��i�[����ϐ�

        private bool isInvicible = false; //�_���[�W���󂯂Ă��΂炭�̊Ԃ̓_���[�W���ʂ�Ȃ��悤�ɂ���ϐ�

        private SpriteRenderer spriteRenderer;

        private Color originalColor;

        private Color flashColor = new Color(1f, 0.5f, 0.5f, 0.4f);  //�t���b�V�����邽�߂̕ϐ�

        Vector2 rightStart;
        Vector2 rightEnd;
        Vector2 leftStart;
        Vector2 leftEnd;
        Vector2 upStart;
        Vector2 upEnd;
        Vector2 downStart;
        Vector2 downEnd;
        RaycastHit2D leftHit;
        RaycastHit2D rightHit;
        RaycastHit2D upHit;
        RaycastHit2D downHit;
        [Header("����")]
        [SerializeField] LayerMask wall;

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

            WallLineCast();
            IsWallHit();

            PlayerDirection();
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

        private void PlayerDirection()
        {
            if(moveInput.x == 1)
            {
                transform.localEulerAngles = Vector3.zero; 
            }else if(moveInput.x == -1)
            {
                transform.localEulerAngles = new Vector3(0,0,180);
            }

            if(moveInput.y == 1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
            }else if(moveInput.y == -1)
            {
                transform.localEulerAngles = new Vector3(0, 0, 270);
            }
        }

        private void WallLineCast()
        {
            float lineSize = 0.5f;
            float lineSizeB = 0.05f;
            rightStart = transform.position + (transform.right * 0.66f) + (transform.up * lineSizeB);
            rightEnd = transform.position + (transform.right * 0.66f) - (transform.up * lineSizeB);

            leftStart = transform.position + (-transform.right * 0.66f) + (transform.up * lineSizeB);
            leftEnd = transform.position + (-transform.right * 0.66f) - (transform.up * lineSizeB);

            upStart = transform.position + (transform.right * lineSizeB) + (transform.up * 0.66f);
            upEnd = transform.position + (-transform.right * lineSizeB) + (transform.up * 0.66f);

            downStart = transform.position + (transform.right * lineSizeB) - (transform.up * 0.66f);
            downEnd = transform.position + (-transform.right * lineSizeB) - (transform.up * 0.66f);


            rightHit = Physics2D.Linecast(rightStart, rightEnd, wall);
            leftHit = Physics2D.Linecast(leftStart, leftEnd, wall);
            upHit = Physics2D.Linecast(upStart, upEnd, wall);
            downHit = Physics2D.Linecast(downStart, downEnd, wall);

            Debug.DrawLine(rightStart, rightEnd, Color.red);
            Debug.DrawLine(leftStart, leftEnd, Color.red);
            Debug.DrawLine(upStart, upEnd, Color.red);
            Debug.DrawLine(downStart, downEnd, Color.red);

        }
        private void IsWallHit()
        {
            float knockback = 1.2f;
            if (rightHit)
            {
                // nowSpeed = Mathf.Abs(nowSpeed) * -1;
                transform.position = new Vector2(transform.position.x - knockback, transform.position.y);
            }
            else if (leftHit)
            {
                transform.position = new Vector2(transform.position.x + knockback, transform.position.y);
            }
            else if (upHit)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - knockback);

            }
            else if (downHit)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + knockback);

            }
            //else
            //{
            //    nowSpeed = Mathf.Abs(nowSpeed);
            //}
        }
    }
}

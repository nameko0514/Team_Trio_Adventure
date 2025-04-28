using System.Collections;
using UnityEngine;

namespace fujiiYuma{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PlayerController : MonoBehaviour
    {
        [Header("----体力----")]
        [SerializeField] protected int initialHealth = 3;
        protected int health;

        [Header("----速さ----")]
        [SerializeField] protected float speed = 3f;

        protected Rigidbody2D rb;

        protected Vector2 moveInput; //移動入力を格納する変数

        private bool isInvicible = false; //ダメージを受けてしばらくの間はダメージが通らないようにする変数

        private SpriteRenderer spriteRenderer;

        private Color originalColor;

        private Color flashColor = new Color(1f, 0.5f, 0.5f, 0.4f);  //フラッシュするための変数

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
        [Header("かべ")]
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
            moveInput.x = Input.GetAxis("Horizontal"); //左右の入力
            moveInput.y = Input.GetAxis("Vertical"); //上下の入力

            WallLineCast();
            IsWallHit();

            PlayerDirection();
        }

        private void FixedUpdate()
        {
            //Rigidbody2Dを使ってキャラクターを移動
            rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //衝突したオブジェクトのLayerを確認
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("PlayerがEnemyと衝突しました。");
            }

        }

        public void TakeDamage(int damage)
        {
            if (isInvicible) { return; }

            health = Mathf.Max(health - damage, 0);

            StartCoroutine(FlashCoroutine());

            if (health == 0)
            {
                //体力(ハート)がなくなった時の処理

                Destroy(gameObject);
            }
        }

        private IEnumerator FlashCoroutine(float flashDuration = 1f)
        {
            isInvicible = true;

            float elapsedTime = 0f;  //経過時間
            float flashInterval = 0.02f;  //フラッシュする間隔

            while (elapsedTime < flashDuration)
            {
                //色を切り替え
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

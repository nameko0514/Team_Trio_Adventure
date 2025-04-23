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

        [Header("----EnemyLayer----")]
        [SerializeField] protected LayerMask enemyLayer;

        protected Rigidbody2D rb;

        private Vector2 moveInput; //移動入力を格納する変数

        private bool isInvicible = false; //ダメージを受けてしばらくの間はダメージが通らないようにする変数

        private SpriteRenderer spriteRenderer;

        private Color originalColor;

        private Color flashColor = new Color(1f, 0.5f, 0.5f, 0.4f);  //フラッシュするための変数

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
    }
}

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
    }
}

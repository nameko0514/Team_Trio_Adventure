using fujiiYuma;
using UnityEngine;

public class EnemyBossBullet : MonoBehaviour
{
    [Tooltip("弾の処理")]
    [Header("---弾のスピード---")]
    [SerializeField] private float speed = 15;
    [Header("---弾のダメージ---")]
    [SerializeField] private float damage = 1;
    [Header("---弾のライフタイム---")]
    [SerializeField] private float lifetime = 1.5f;

    private Rigidbody2D rb;

    private Transform player;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
        Debug.Log("弾消滅");

        //Rigitbody2Dの取得    
        rb = GetComponent<Rigidbody2D>();
    }

    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        //弾のスピードを設定
        rb.linearVelocity = direction * speed;
    }

    //ダメージ処理
    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                const int LethalDamage = 1;
                player.TakeDamage(LethalDamage);
            }
        }
    }
}

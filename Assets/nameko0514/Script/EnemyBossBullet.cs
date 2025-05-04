using fujiiYuma;
using UnityEngine;

public class EnemyBossBullet : MonoBehaviour
{
    [Tooltip("�e�̏���")]
    [Header("---�e�̃X�s�[�h---")]
    [SerializeField] private float speed = 15;
    [Header("---�e�̃_���[�W---")]
    [SerializeField] private float damage = 1;
    [Header("---�e�̃��C�t�^�C��---")]
    [SerializeField] private float lifetime = 1.5f;

    private Rigidbody2D rb;

    private Transform player;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
        Debug.Log("�e����");

        //Rigitbody2D�̎擾    
        rb = GetComponent<Rigidbody2D>();
    }

    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        //�e�̃X�s�[�h��ݒ�
        rb.linearVelocity = direction * speed;
    }

    //�_���[�W����
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

using UnityEngine;

namespace Takato
{
    public class BulletController : MonoBehaviour
    {
        [Header("---弾のスピード---")]
        [SerializeField] private float speed;
        [Header("---弾のダメージ---")]
        [SerializeField] private float damage;
        [Header("---弾のライフタイム---")]
        [SerializeField] private float lifetime;

        private Rigidbody2D rb;

        private void Awake()
        {
            Destroy(gameObject, lifetime);
            Debug.Log("弾消えた");

            //Rigitbody2Dの取得    
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            //弾のスピードを設定
            rb.linearVelocity = transform.right * speed;
        }

        //ダメージ処理
        public float GetDamage()
        {
            return damage;
        }   
    }
}


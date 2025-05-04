using UnityEngine;

namespace Takato
{
    public class BulletController : MonoBehaviour
    {
        [Tooltip("弾の処理")]
        [Header("---弾のダメージ---")]
        [SerializeField] private float damage;
        [Header("---弾のライフタイム---")]
        [SerializeField] private float lifetime;

        private Rigidbody2D rb;

        private void Awake()
        {
            Destroy(gameObject, lifetime);
            Debug.Log("弾消滅");

            //Rigitbody2Dの取得    
            rb = GetComponent<Rigidbody2D>();
        }

        //ダメージ処理
        public float GetDamage()
        {
            return damage;
        }   
    }
}


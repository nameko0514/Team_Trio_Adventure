using UnityEngine;

namespace Takato
{
    public class BulletController : MonoBehaviour
    {
        [Header("---�e�̃X�s�[�h---")]
        [SerializeField] private float speed;
        [Header("---�e�̃_���[�W---")]
        [SerializeField] private float damage;
        [Header("---�e�̃��C�t�^�C��---")]
        [SerializeField] private float lifetime;

        private Rigidbody2D rb;

        private void Awake()
        {
            Destroy(gameObject, lifetime);
            Debug.Log("�e������");

            //Rigitbody2D�̎擾    
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            //�e�̃X�s�[�h��ݒ�
            rb.linearVelocity = transform.right * speed;
        }

        //�_���[�W����
        public float GetDamage()
        {
            return damage;
        }   
    }
}


using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public int Health = 10;
    private Rigidbody2D rb;

    public void TakeDamage(int Damage)
    {
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        Health -= Damage;
        if (Health <= 0)
        {
            Debug.Log("�v���C���[�����S���܂���");
            // �v���C���[���S���̏���
            Destroy(gameObject);
        }
    }

}

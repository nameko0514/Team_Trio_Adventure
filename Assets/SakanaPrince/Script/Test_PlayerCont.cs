using matumoto;
using UnityEngine;

namespace matumoto
{
    public class Test_PlayerCont : MonoBehaviour
    {
        protected Rigidbody2D rb;

        private Vector2 moveInput; //�ړ����͂��i�[����ϐ�

        float speed = 1.0f;

        float nowSpeed = 1;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {

            moveInput.x = Input.GetAxis("Horizontal"); //���E�̓���
            moveInput.y = Input.GetAxis("Vertical"); //�㉺�̓���
        }
        // Update is called once per frame
        private void FixedUpdate()
        {
            //Rigidbody2D���g���ăL�����N�^�[���ړ�
            rb.MovePosition(rb.position + moveInput * nowSpeed * Time.fixedDeltaTime);

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
                Debug.Log("fs");
           
            if (other.CompareTag("Wall"))
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.Example);
                if(moveInput.x > 0)
                {
                   moveInput.x = 0;
                }

            }

        }




    }
}
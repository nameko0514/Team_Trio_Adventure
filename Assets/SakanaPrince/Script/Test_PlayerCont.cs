using matumoto;
using UnityEngine;

namespace matumoto
{
    public class Test_PlayerCont : MonoBehaviour
    {
        protected Rigidbody2D rb;

        private Vector2 moveInput; //移動入力を格納する変数

        float speed = 1.0f;

        float nowSpeed = 1;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {

            moveInput.x = Input.GetAxis("Horizontal"); //左右の入力
            moveInput.y = Input.GetAxis("Vertical"); //上下の入力
        }
        // Update is called once per frame
        private void FixedUpdate()
        {
            //Rigidbody2Dを使ってキャラクターを移動
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
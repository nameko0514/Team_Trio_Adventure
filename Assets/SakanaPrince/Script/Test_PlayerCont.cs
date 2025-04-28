using matumoto;
using UnityEngine;

namespace matumoto
{
    public class Test_PlayerCont : MonoBehaviour
    {
        protected Rigidbody2D rb;

        private Vector2 moveInput; //移動入力を格納する変数

        float speed = 1.0f;

        float nowSpeed = 0.2f;

        Vector2 rightStart;
        Vector2 rightEnd;
        Vector2 leftStart;
        Vector2 leftEnd;
        Vector2 upStart;
        Vector2 upEnd;
        Vector2 downStart;
        Vector2 downEnd;
        RaycastHit2D leftHit;
        RaycastHit2D rightHit;
        RaycastHit2D upHit;
        RaycastHit2D downHit;

        [Header("かべ")]
        [SerializeField] LayerMask wall;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            WallLineCast();
            IsWallHit();
            moveInput.x = Input.GetAxis("Horizontal"); //左右の入力
            moveInput.y = Input.GetAxis("Vertical"); //上下の入力
        }
        // Update is called once per frame
        private void FixedUpdate()
        {
            //Rigidbody2Dを使ってキャラクターを移動
            rb.MovePosition(rb.position + moveInput * nowSpeed );

        }

        private void WallLineCast()
        {
            float lineSize = 0.5f; 
            float lineSizeB = 0.05f; 
            rightStart = transform.position + (transform.right * 0.66f) + (transform.up * lineSizeB);
            rightEnd = transform.position + (transform.right * 0.66f) - (transform.up *lineSizeB);

            leftStart = transform.position + (-transform.right * 0.66f) + (transform.up * lineSizeB);
            leftEnd = transform.position + (-transform.right * 0.66f) - (transform.up * lineSizeB);

            upStart = transform.position + (transform.right * lineSizeB) + (transform.up * 0.66f);
            upEnd = transform.position + (-transform.right * lineSizeB) + (transform.up * 0.66f);

            downStart = transform.position + (transform.right * lineSizeB) - (transform.up * 0.66f);
            downEnd = transform.position + (-transform.right * lineSizeB) - (transform.up * 0.66f);


            rightHit = Physics2D.Linecast(rightStart, rightEnd, wall);
            leftHit = Physics2D.Linecast (leftStart, leftEnd, wall);
            upHit = Physics2D.Linecast(upStart, upEnd, wall);
            downHit = Physics2D.Linecast(downStart, downEnd, wall);

            Debug.DrawLine(rightStart, rightEnd,Color.red);
            Debug.DrawLine(leftStart, leftEnd,Color.red);
            Debug.DrawLine(upStart, upEnd,Color.red);
            Debug.DrawLine(downStart, downEnd,Color.red);
         
        }
       private void IsWallHit()
        {
            float knockback = 1.2f;
            if (rightHit)
            {
                // nowSpeed = Mathf.Abs(nowSpeed) * -1;
                transform.position = new Vector2(transform.position.x - knockback, transform.position.y);
            }
            else if(leftHit)
            {
                transform.position = new Vector2(transform.position.x +knockback, transform.position.y);
            }
            else if (upHit)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - knockback);

            }
            else if (downHit)
            {
                transform.position = new Vector2(transform.position.x , transform.position.y + knockback);

            }
            //else
            //{
            //    nowSpeed = Mathf.Abs(nowSpeed);
            //}
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
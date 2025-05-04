using fujiiYuma;
using UnityEngine;

namespace matumoto
{
    public class Test_DevilMove : MonoBehaviour
    {
        [SerializeField] private int damage = 1;  //ダメージ
        [SerializeField] private float debuffTime = 3f; //デバフ時間


        Transform player;

        Animator animator;

        float moveSpeed = 2;



        private void Awake()
        {
            animator = GetComponent<Animator>();
         
        }
        private void FixedUpdate()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            //Debug.Log(player);

            //playerが設定されていないときはなにもしない
            if (player == null) return;

            //playerとの距離の計算
            float distanceToplayer = Vector2.Distance(transform.position, player.position);

            //playerとの距離が最小距離以上の場合のみ移動
            if (distanceToplayer > 0.1f)
            {
                //現在の位置からPlayerの位置に向かってLerpで移動
                transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            //アニメーション遷移
            SetAnime();

            //左右の見た目反転
            if(player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

        }
        private void SetAnime()
        {
            if (player.transform.position.x - transform.position.x >
                player.transform.position.y - transform.position.y )
            {
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                animator.SetBool("hori", true);
            }
            else
            {

                if (player.transform.position.y > transform.position.y)
                {
                    animator.SetBool("up", true);
                    animator.SetBool("down", false);
                    animator.SetBool("hori", false);
                }
                else
                {
                    animator.SetBool("up", false);
                    animator.SetBool("down", true);
                    animator.SetBool("hori", false);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //layerControllerを継承したプレイヤーに当たったか
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Devil: プレイヤーにヒット！");

                player.TakeDamage(damage); // P体力を減らす

                // プレイヤーが操作反転を持っているか確認して付与
                if (player is IReverseControl1 reverseControl)　　　//IReverseControl1をScriptsフォルダーに追加してほしいです
                {
                    reverseControl.ApplyReverseControl(debuffTime);  // 反転デバフを与える
                }
            }
        }
    }
}
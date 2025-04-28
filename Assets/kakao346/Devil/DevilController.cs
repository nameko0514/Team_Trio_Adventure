using UnityEngine;
using fujiiYuma;

namespace Gishi
{
    public class DevilController : MonoBehaviour
    {
        [SerializeField] private int damage = 1;  //ダメージ
        [SerializeField] private float debuffTime = 3f; //デバフ時間

        private void OnTriggerEnter2D(Collider2D other)
        {
            //layerControllerを継承したプレイヤーに当たったか
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Devil: プレイヤーにヒット！");

                player.TakeDamage(damage); // P体力を減らす

                //// プレイヤーが操作反転を持っているか確認して付与する
                //if (player is IReverseControl reverseControl)　　　//IReverseControlをScriptsフォルダーに追加してほしいです
                //{
                //    reverseControl.ApplyReverseControl(debuffTime);  // 反転デバフを与える
                //}
            }
        }
       
    }

}


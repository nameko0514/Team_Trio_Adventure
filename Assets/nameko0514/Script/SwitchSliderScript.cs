using matumoto;
using UnityEngine;

public class SwitchSliderScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider slider;
    SwitchPlayer switchPlayer;

    float pressStartTime;
    float holdTime;
    float lastSwitchTime;
    bool isHolding;
    float holdThreshold = 1;
    float switchInterval = 1;
    private void Awake()
    {
        switchPlayer = GetComponent<SwitchPlayer>();

    }
    private void Update()
    {

        if (slider == null)
        {
            return;
        }
        else
        {
            Debug.Log(holdTime + " " + switchInterval);
            slider.value = holdTime / switchInterval;
            //   enemyComingSlider.value = currentTime / initialWaveDirey;
        }


        if (Input.GetKeyDown(KeyCode.Space)) // キーを押した瞬間
        {
            pressStartTime = Time.time; // 押した時間を記録
            isHolding = false; // 長押しフラグをリセット
        }

        if (Input.GetKey(KeyCode.Space)) // キーを押している間
        {
            holdTime = Time.time - pressStartTime;

            // 長押し判定（holdThreshold秒以上押している場合）
            if (holdTime > holdThreshold)
            {
                isHolding = true;

                // 一定間隔（switchInterval秒）でプレイヤーを切り替え
                if (Time.time - lastSwitchTime >= switchInterval)
                {
                    //ここですか
                    SoundManager.Instance.PlaySE(SESoundData.SE.PlayerChange);//これですか


                    lastSwitchTime = Time.time;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) // キーを離した瞬間
        {

            holdTime = 0;
            // 長押しでない場合（単押しの場合）にアクションを実行
            if (!isHolding)
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.EnemyKnockOut);
            }
        }
    }
}

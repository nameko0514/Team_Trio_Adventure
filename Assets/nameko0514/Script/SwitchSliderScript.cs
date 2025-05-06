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


        if (Input.GetKeyDown(KeyCode.Space)) // �L�[���������u��
        {
            pressStartTime = Time.time; // ���������Ԃ��L�^
            isHolding = false; // �������t���O�����Z�b�g
        }

        if (Input.GetKey(KeyCode.Space)) // �L�[�������Ă����
        {
            holdTime = Time.time - pressStartTime;

            // ����������iholdThreshold�b�ȏ㉟���Ă���ꍇ�j
            if (holdTime > holdThreshold)
            {
                isHolding = true;

                // ���Ԋu�iswitchInterval�b�j�Ńv���C���[��؂�ւ�
                if (Time.time - lastSwitchTime >= switchInterval)
                {
                    //�����ł���
                    SoundManager.Instance.PlaySE(SESoundData.SE.PlayerChange);//����ł���


                    lastSwitchTime = Time.time;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) // �L�[�𗣂����u��
        {

            holdTime = 0;
            // �������łȂ��ꍇ�i�P�����̏ꍇ�j�ɃA�N�V���������s
            if (!isHolding)
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.EnemyKnockOut);
            }
        }
    }
}

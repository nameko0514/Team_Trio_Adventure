using fujiiYuma;
using matumoto;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchPlayer : MonoBehaviour
{
    // �v���C���[��GameObject�̔z��
    public GameObject[] players;
    private int currentPlayerIndex = 0; // ���݃A�N�e�B�u�ȃv���C���[�̃C���f�b�N�X

    // ���������o�p�̕ϐ�
    private float pressStartTime; // �L�[�������n�߂�����
    private float holdThreshold = 1f; // �������Ƃ݂Ȃ����ԁi�b�j
    private bool isHolding = false; // �����������ǂ���
    private float switchInterval = 1f; // ���������̃v���C���[�؂�ւ��Ԋu�i�b�j
    private float lastSwitchTime; // �Ō�ɐ؂�ւ�������

    // �O�̃v���C���[�̃|�W�V�������擾���邽�߂̕ϐ�
    private Vector2 playerPos;

    private Vector2 playerSwitchPos;

    public bool isTrigger { get; private set; } = false;

    private GameObject player;

    [Header("----�t�F�[�h�C������ׂ̃C���[�W----")]
    [SerializeField] private Image fadeImage;

    private bool isSceneTrigger = true;

    void Start()
    {
        isSceneTrigger = true;

        // ������: �ŏ��̃v���C���[�����A�N�e�B�u�ɂ��A�c��͔�A�N�e�B�u
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                players[i].SetActive(i == currentPlayerIndex);
            }
        }

        playerPos = Vector2.zero;
    }

    void Update()
    {
        if (PlayersAllNull())
        {
            Debug.Log("�Q�[���I�[�o�[");
            //Player���S�ł�����
            if (!isSceneTrigger) return;
            isSceneTrigger = false;
            if (fadeImage == null) return;
            StartCoroutine(FadeIn(0, 1, 2, null));
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player")?.gameObject;

        if (players[currentPlayerIndex] == null)
        {
            DeathSwitchPlayers();
        }
        else
        {
            playerSwitchPos = players[currentPlayerIndex].transform.position;
        }

        // Enter�L�[�̓��͏���
        if (Input.GetKeyDown(KeyCode.Space)) // �L�[���������u��
        {
            pressStartTime = Time.time; // ���������Ԃ��L�^
            isHolding = false; // �������t���O�����Z�b�g
        }

        if (Input.GetKey(KeyCode.Space)) // �L�[�������Ă����
        {
            float holdTime = Time.time - pressStartTime;

            // ����������iholdThreshold�b�ȏ㉟���Ă���ꍇ�j
            if (holdTime > holdThreshold)
            {
                isHolding = true;

                // ���Ԋu�iswitchInterval�b�j�Ńv���C���[��؂�ւ�
                if (Time.time - lastSwitchTime >= switchInterval)
                {
                    //�����ł���
                    SoundManager.Instance.PlaySE(SESoundData.SE.PlayerChange);//����ł���

                    SwitchPlayers();
                    lastSwitchTime = Time.time;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) // �L�[�𗣂����u��
        {
            // �������łȂ��ꍇ�i�P�����̏ꍇ�j�ɃA�N�V���������s
            if (!isHolding)
            {
                PerformSinglePressAction();
            }
        }
    }

    // �v���C���[��؂�ւ���֐�
    void SwitchPlayers()
    {
        if(player != null)
        {
            if (player.CompareTag("Player"))
            {
                if(player.TryGetComponent<PlayerController>(out var playerController))
                {
                    playerController.ResetIsInvicible();
                }
            }
        }

        // ���݂̃v���C���[�̃|�W�V�����擾
        playerPos = players[currentPlayerIndex].transform.position;

        // ���݂̃v���C���[���A�N�e�B�u��
        players[currentPlayerIndex].SetActive(false);

        // ���̃v���C���[�̃C���f�b�N�X���v�Z
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        // ���̃v���C���[������ł����炻�̎��̃v���C���[�ɐ؂�ւ���
        while (players[currentPlayerIndex] == null)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        // ���̃v���C���[�̃|�W�V�������O�̃v���C���[�̃|�W�V�����Ɉړ�
        players[currentPlayerIndex].transform.position = playerPos;

        // ���̃v���C���[���A�N�e�B�u��
        players[currentPlayerIndex].SetActive(true);

        Debug.Log($"Switched to Player {currentPlayerIndex + 1}");
    }

    private void DeathSwitchPlayers()
    {
        // ���݂̃v���C���[�̃|�W�V�����擾
        playerPos = playerSwitchPos;

        // ���̃v���C���[�̃C���f�b�N�X���v�Z
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        // ���̃v���C���[������ł����炻�̎��̃v���C���[�ɐ؂�ւ���
        while (players[currentPlayerIndex] == null)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        // ���̃v���C���[�̃|�W�V�������O�̃v���C���[�̃|�W�V�����Ɉړ�
        players[currentPlayerIndex].transform.position = playerPos;

        // ���̃v���C���[���A�N�e�B�u��
        players[currentPlayerIndex].SetActive(true);
    }

    // �P�������̃A�N�V����
    void PerformSinglePressAction()
    {
        isTrigger = true;
        Debug.Log($"{currentPlayerIndex + 1}�F �X�L������");
    }

    public void ResetTrigger()
    {
        isTrigger = false;
    }

    private bool PlayersAllNull()
    {
        bool allNull = true;

        foreach (var pla in players)
        {
            if(pla != null)
            {
                allNull = false;
                break;
            }
        }

        return allNull;
    }

    private IEnumerator FadeIn(float startAlpha, float endAlpha, float duration, Action onComplete)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;

        SceneManager.LoadScene("TitleScene_00");

        onComplete?.Invoke();
    }
}

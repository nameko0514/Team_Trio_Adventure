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
    // プレイヤーのGameObjectの配列
    public GameObject[] players;
    private int currentPlayerIndex = 0; // 現在アクティブなプレイヤーのインデックス

    // 長押し検出用の変数
    private float pressStartTime; // キーを押し始めた時間
    private float holdThreshold = 1f; // 長押しとみなす時間（秒）
    private bool isHolding = false; // 長押し中かどうか
    private float switchInterval = 1f; // 長押し中のプレイヤー切り替え間隔（秒）
    private float lastSwitchTime; // 最後に切り替えた時間

    // 前のプレイヤーのポジションを取得するための変数
    private Vector2 playerPos;

    private Vector2 playerSwitchPos;

    public bool isTrigger { get; private set; } = false;

    private GameObject player;

    [Header("----フェードインする為のイメージ----")]
    [SerializeField] private Image fadeImage;

    private bool isSceneTrigger = true;

    void Start()
    {
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Satge1);

        isSceneTrigger = true;

        // 初期化: 最初のプレイヤーだけアクティブにし、残りは非アクティブ
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
            Debug.Log("ゲームオーバー");
            //Playerが全滅した時
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

        // Enterキーの入力処理
        if (Input.GetKeyDown(KeyCode.Space)) // キーを押した瞬間
        {
            pressStartTime = Time.time; // 押した時間を記録
            isHolding = false; // 長押しフラグをリセット
        }

        if (Input.GetKey(KeyCode.Space)) // キーを押している間
        {
            float holdTime = Time.time - pressStartTime;

            // 長押し判定（holdThreshold秒以上押している場合）
            if (holdTime > holdThreshold)
            {
                isHolding = true;

                // 一定間隔（switchInterval秒）でプレイヤーを切り替え
                if (Time.time - lastSwitchTime >= switchInterval)
                {
                    //ここですか
                    SoundManager.Instance.PlaySE(SESoundData.SE.PlayerChange);//これですか

                    SwitchPlayers();
                    lastSwitchTime = Time.time;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) // キーを離した瞬間
        {
            // 長押しでない場合（単押しの場合）にアクションを実行
            if (!isHolding)
            {
                PerformSinglePressAction();
            }
        }
    }

    // プレイヤーを切り替える関数
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

        // 現在のプレイヤーのポジション取得
        playerPos = players[currentPlayerIndex].transform.position;

        // 現在のプレイヤーを非アクティブに
        players[currentPlayerIndex].SetActive(false);

        // 次のプレイヤーのインデックスを計算
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        // 次のプレイヤーが死んでいたらその次のプレイヤーに切り替える
        while (players[currentPlayerIndex] == null)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        // 次のプレイヤーのポジション取を前のプレイヤーのポジションに移動
        players[currentPlayerIndex].transform.position = playerPos;

        // 次のプレイヤーをアクティブに
        players[currentPlayerIndex].SetActive(true);

        Debug.Log($"Switched to Player {currentPlayerIndex + 1}");
    }

    private void DeathSwitchPlayers()
    {
        // 現在のプレイヤーのポジション取得
        playerPos = playerSwitchPos;

        // 次のプレイヤーのインデックスを計算
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        // 次のプレイヤーが死んでいたらその次のプレイヤーに切り替える
        while (players[currentPlayerIndex] == null)
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        // 次のプレイヤーのポジション取を前のプレイヤーのポジションに移動
        players[currentPlayerIndex].transform.position = playerPos;

        // 次のプレイヤーをアクティブに
        players[currentPlayerIndex].SetActive(true);
    }

    // 単押し時のアクション
    void PerformSinglePressAction()
    {
        isTrigger = true;
        Debug.Log($"{currentPlayerIndex + 1}： スキル発動");
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

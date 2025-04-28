using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    [Header("----追従のスムーズさ----")]
    [SerializeField] private float smoothSpeed = 0.125f;

    private Transform player;

    private Vector3 offset; //カメラのオフセット

    private void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        //playerが設定されていないときはなにもしない
        if (player == null) return;

        //ターゲットの位置にオフセットを加える
        Vector3 desiredPosition = player.position + offset;

        //現在のカメラの位置をスムーズに移動させる
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x,smoothedPosition.y,-10);
    }
}

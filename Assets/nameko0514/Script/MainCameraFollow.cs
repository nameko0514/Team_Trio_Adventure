using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    private Transform player;

    private void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;


    }
}

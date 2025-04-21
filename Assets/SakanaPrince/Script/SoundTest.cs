using matumoto;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Example);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace matumoto
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField]  private Slider slider;
        SwitchPlayer switchPlayer;

        private void Update()
        {
          switchPlayer = GetComponent<SwitchPlayer>();

            if(slider == null || switchPlayer == null)
            {
                return;
            }

           
        }
    }

}
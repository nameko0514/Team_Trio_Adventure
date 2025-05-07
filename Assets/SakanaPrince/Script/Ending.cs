using System;
using UnityEngine;
using UnityEngine.UI;


namespace matumoto
{
    public class Ending : MonoBehaviour
    {
        float startPosX;
        float currentTime;
        const float stopTime = 3;

        int currentWord = 0;
        [SerializeField] Text myText;
        [SerializeField] String[] staffNames;

        private void Start()
        {
            startPosX = transform.position.x;

            currentTime = 0;

            currentWord = 0;
        }
        private void Update()
        {
            myText.text = staffNames[currentWord];

            Debug.Log(transform.position.x);

            if (transform.position.x < -250) //‰æ–ÊƒAƒEƒg‚µ‚½
            {
                transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);

                //currentWord++;

                currentWord = (int)MathF.Min(currentWord + 1, 5);


            }


            if (1000 < transform.position.x)
            {
                currentTime = 0;
                transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
            }
            else if (currentTime > stopTime &&  -250 < transform.position.x) 
            {
                
                transform.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
            }
            else
            {
                currentTime += Time.deltaTime;
            }

        }
    }

}
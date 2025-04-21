using UnityEngine;
using System.Collections.Generic;

namespace matumoto
{
    public class SoundManager : MonoBehaviour
    {

        [Header("BGMの方はループにチェックが入ってるよ")]
        [SerializeField] AudioSource bgmAudioSource;
        [SerializeField] AudioSource seAudioSource;

        [SerializeField] List<BGMSoundData> bgmSoundDatas;
        [SerializeField] List<SESoundData> seSoundDatas;

        public float masterVolume = 1;
        public float bgmMasterVolume = 1;
        public float seMasterVolume = 1;

        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayBGM(BGMSoundData.BGM bgm)
        {
            BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
            bgmAudioSource.clip = data.audioClip;
            bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
            bgmAudioSource.Play();
        }


        public void PlaySE(SESoundData.SE se)
        {
            SESoundData data = seSoundDatas.Find(data => data.se == se);
            seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
            seAudioSource.PlayOneShot(data.audioClip);
        }

    }

    [System.Serializable]
    public class BGMSoundData
    {
        public enum BGM
        {
            Title,
            Satge1,
            None,
        }

        public BGM bgm;
        public AudioClip audioClip;
        [Range(0, 1.2f)]
        public float volume = 1;
    }

    [System.Serializable]
    public class SESoundData
    {
        //鳴らしたいサウンドは？
        public enum SE
        {
            Example,
            None,
        }

        public SE se;
        public AudioClip audioClip;
        [Range(0, 1)]
        public float volume = 1;
    }
}

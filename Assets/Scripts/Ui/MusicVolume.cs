using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class MusicVolume : MonoBehaviour
    {
        private void Start()
        {
            var slider = GetComponent<Slider>();
            var musicAudioSoucre = Camera.main.GetComponent<AudioSource>();
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = musicAudioSoucre.volume;

            slider.onValueChanged.AddListener(f => musicAudioSoucre.volume = f);
        }
    }
}

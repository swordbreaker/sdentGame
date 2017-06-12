using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class SoundVolume : MonoBehaviour
    {
        private Slider _slider;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.minValue = 0f;
            _slider.maxValue = 1f;
            _slider.value = AudioListener.volume;

            _slider.onValueChanged.AddListener(f => AudioListener.volume = f);
        }
    }
}

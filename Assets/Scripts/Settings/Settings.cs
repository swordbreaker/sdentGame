using Assets.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Settings
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Slider _mouseXSlider;
        [SerializeField] private Slider _mouseYSlider;
        [SerializeField] private TMP_InputField _mouseXInputField;
        [SerializeField] private TMP_InputField _mouseYInputField;
        [SerializeField] private GameSettings _gameSettings;

        private void Start()
        {
            _mouseXSlider.onValueChanged.AddListener(OnValueChangedSilderMouseX);
            _mouseYSlider.onValueChanged.AddListener(OnValueChangedSilderMouseY);
            _mouseXInputField.onValueChanged.AddListener(OnValueChangedInputFieldMouseX);
            _mouseYInputField.onValueChanged.AddListener(OnValueChangedInputFieldMouseY);

            _mouseXSlider.value = _gameSettings.MouseSensitivtySettings.X;
            _mouseYSlider.value = _gameSettings.MouseSensitivtySettings.Y;

            _mouseXInputField.text = _gameSettings.MouseSensitivtySettings.X.ToString("F");
            _mouseYInputField.text = _gameSettings.MouseSensitivtySettings.Y.ToString("F");
        }

        private void OnValueChangedInputFieldMouseY(string newVal)
        {
            float fValue = 0f;
            if (float.TryParse(newVal, out fValue))
            {
                _mouseYSlider.value = fValue;
                _gameSettings.MouseSensitivtySettings.Y = fValue;
            }
            else
            {
                _mouseYInputField.text = _mouseYSlider.value.ToString("F");
            }
        }

        private void OnValueChangedInputFieldMouseX(string newVal)
        {
            float fValue = 0f;
            if (float.TryParse(newVal, out fValue))
            {
                _mouseXSlider.value = fValue;
                _gameSettings.MouseSensitivtySettings.X = fValue;
            }
            else
            {
                _mouseXInputField.text = _mouseYSlider.value.ToString("F");
            }
        }

        private void OnValueChangedSilderMouseY(float newVal)
        {
            _mouseYInputField.text = newVal.ToString("F");
            _gameSettings.MouseSensitivtySettings.Y = newVal;
        }

        private void OnValueChangedSilderMouseX(float newVal)
        {
            _mouseXInputField.text = newVal.ToString("F");
            _gameSettings.MouseSensitivtySettings.X = newVal;
        }
    }
}

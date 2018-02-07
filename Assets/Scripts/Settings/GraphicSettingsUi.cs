using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Settings
{
    public class GraphicSettingsUi : MonoBehaviour
    {
        [SerializeField]
        private Toggle _depthOfFieldToggle;

        [SerializeField]
        private Toggle _bloomToggle;

        [SerializeField]
        private Toggle _ambientIcclusionToggle;

        [SerializeField]
        private Toggle _colorGradingToggle;

        [SerializeField]
        private Toggle _motionBlurToggle;

        [SerializeField]
        private TMPro.TMP_Dropdown _qualityLevelDropdown;

        [SerializeField]
        private Button _goBackButton;

        public void Start()
        {
            _depthOfFieldToggle.isOn = GraphicSettings.Instance.UseDepthOfFile;
            _bloomToggle.isOn = GraphicSettings.Instance.UseBloom;
            _ambientIcclusionToggle.isOn = GraphicSettings.Instance.UseAmbientOccultion;
            _colorGradingToggle.isOn = GraphicSettings.Instance.UseColorGrading;
            _motionBlurToggle.isOn = GraphicSettings.Instance.UseMotionBlur;

            _depthOfFieldToggle.onValueChanged.AddListener(value => GraphicSettings.Instance.UseDepthOfFile = value);
            _bloomToggle.onValueChanged.AddListener(value => GraphicSettings.Instance.UseBloom = value);
            _ambientIcclusionToggle.onValueChanged.AddListener(value => GraphicSettings.Instance.UseAmbientOccultion = value);
            _colorGradingToggle.onValueChanged.AddListener(value => GraphicSettings.Instance.UseColorGrading = value);
            _motionBlurToggle.onValueChanged.AddListener(value => GraphicSettings.Instance.UseMotionBlur = value);

            for (int i = 0; i < QualitySettings.names.Length; i++)
            {
                _qualityLevelDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(QualitySettings.names[i]));
            }

            _qualityLevelDropdown.value = GraphicSettings.Instance.QualityLevel;
            _qualityLevelDropdown.onValueChanged.AddListener(i => GraphicSettings.Instance.QualityLevel = i);

            _goBackButton.onClick.AddListener(() => gameObject.SetActive(false));
        }
    }
}

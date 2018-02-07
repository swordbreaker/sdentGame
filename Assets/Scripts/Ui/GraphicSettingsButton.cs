using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    class GraphicSettingsButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject _graphicSettingsPanel;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => _graphicSettingsPanel.SetActive(true));
        }
    }
}

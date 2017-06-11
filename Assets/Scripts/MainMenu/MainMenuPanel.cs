using DG.Tweening;
using ItsHarshdeep.LoadingScene.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainMenu
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private Ease _ease;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        private RectTransform _rectTransform;

        private void Start ()
        {
            _rectTransform = GetComponent<RectTransform>();
            Invoke("Show", _delay);

            _playButton.onClick.AddListener(() => SceneController.LoadLevel("Main"));
            _exitButton.onClick.AddListener(Application.Quit);
        }

        private void Show()
        {
            _rectTransform.DOLocalMoveX(620f, 2f).SetEase(_ease);
        }
    }
}

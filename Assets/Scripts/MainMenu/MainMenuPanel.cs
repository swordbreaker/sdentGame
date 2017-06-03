using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ItsHarshdeep.LoadingScene.Controller;

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

using Assets.Scripts.Movement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MouseSensitivity : MonoBehaviour
{

    [SerializeField]
    private MoveController _moveController;

    [SerializeField]
    private Slider _xSlider;

    [SerializeField]
    private Slider _ySlider;

    [SerializeField]
    private TextMeshProUGUI _xText;

    [SerializeField]
    private TextMeshProUGUI _yText;

    private void Start()
    {
        var mouseLook = _moveController.MouseLook;

        _xSlider.minValue = 0.1f;
        _xSlider.maxValue = 10f;
        _xSlider.value = mouseLook.XSensitivity;
        _xText.text = mouseLook.XSensitivity.ToString();

        _xSlider.onValueChanged.AddListener(f =>
        {
            mouseLook.XSensitivity = f;
            _xText.text = f.ToString();
        });

        _ySlider.minValue = 0.1f;
        _ySlider.maxValue = 10f;
        _ySlider.value = mouseLook.YSensitivity;
        _yText.text = mouseLook.YSensitivity.ToString();

        _ySlider.onValueChanged.AddListener(f =>
        {
            mouseLook.YSensitivity = f;
            _yText.text = f.ToString();
        });
    }
}

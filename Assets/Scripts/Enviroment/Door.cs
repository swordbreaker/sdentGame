using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    

    [SerializeField] private Color _closedColor = Color.red;
    [SerializeField] private Color _openColor = Color.green;

    private Transform _leftTransform;
    private Transform _rightTransform;
    private Material _rightLightMaterial;
    private Material _leftLightMaterial;
    private Light _leftLight;
    private Light _rightLight;

    private float _xLeftStart;
    private float _xRightStart;
    [SerializeField] private bool _isOpen;

    public bool IsOpen
    {
        get { return _isOpen; }
        set
        {
            _isOpen = value;
            if(value) Activate();
            else Deactivate();
        }
    }


    private void Start ()
	{
	    _leftTransform = transform.Find("Left");
	    _rightTransform = transform.Find("Right");

	    _rightLightMaterial = _rightTransform.Find("Light").gameObject.GetComponent<Renderer>().materials[1];
	    _leftLightMaterial = _leftTransform.Find("Light").gameObject.GetComponent<Renderer>().materials[1];

	    _leftLight = _leftTransform.Find("Light").Find("Spotlight").GetComponent<Light>();
	    _rightLight = _rightTransform.Find("Light").Find("Spotlight").GetComponent<Light>();

	    _xLeftStart = _leftTransform.position.x;
	    _xRightStart = _rightTransform.position.x;

        IsOpen = _isOpen;
	}
	
	private void Activate()
    {
		_leftLightMaterial.SetColor("_EmissionColor", _openColor);
		_leftLightMaterial.SetColor("_Color", _openColor);
        _rightLightMaterial.SetColor("_EmissionColor", _openColor);
        _rightLightMaterial.SetColor("_Color", _openColor);

        _leftLight.color = _openColor;
        _rightLight.color = _openColor;
    }

    private void Deactivate()
    {
        _leftLightMaterial.SetColor("_EmissionColor", _closedColor);
        _leftLightMaterial.SetColor("_Color", _closedColor);
        _rightLightMaterial.SetColor("_EmissionColor", _closedColor);
        _rightLightMaterial.SetColor("_Color", _closedColor);

        _leftLight.color = _closedColor;
        _rightLight.color = _closedColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_isOpen) return;
        if (other.tag == "Player")
        {
            print("open");
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isOpen) return;
        if (other.tag == "Player")
        {
            Close();
        }
    }

    private void Open()
    {
        _leftTransform.DOLocalMoveX(-1.95f, 1f);
        _rightTransform.DOLocalMoveX(0.9f, 1f);
    }

    private void Close()
    {
        _leftTransform.DOLocalMoveX(_xLeftStart, 1f);
        _rightTransform.DOLocalMoveX(_xRightStart, 1f);
    }

}

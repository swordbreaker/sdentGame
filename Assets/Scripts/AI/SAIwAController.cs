using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAIwAController : MonoBehaviour
{
	public Transform Player;
	private Transform _lookAtTarget;

	[SerializeField]
	private Color _alarmColor = Color.red;

	[SerializeField]
	private Light _spotLight;

    [SerializeField]
    private Light _pointLight;

	[SerializeField]
	private GameObject _core;

	private Color _spotLightStartColor;
	private Color _coreStartColor;

	private Renderer _coreRenderer;

	void Start() 
	{
		this._spotLightStartColor = _spotLight.GetComponent<Light> ().color;
		this._coreRenderer = _core.GetComponent<Renderer> ();
		this._coreStartColor = _coreRenderer.material.color;
	}

	void Update () 
	{
		var newRot = Quaternion.LookRotation(LookAt.position - this.transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
	}

	public void UpdateAlarmColor(float timePassed) 
	{
        var color = Color.Lerp(_spotLightStartColor, _alarmColor, timePassed);
        _spotLight.color = color;
	    _pointLight.color = color;

        _coreRenderer.material.color = Color.Lerp(_coreStartColor, _alarmColor, timePassed);
	}

	public Transform LookAt 
	{
		set
		{
			_lookAtTarget = value;
		}

		get 
		{
			return _lookAtTarget ?? Player; 
		}
	}

}

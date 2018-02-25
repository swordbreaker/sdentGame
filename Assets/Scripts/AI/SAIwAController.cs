using UnityEngine;
using Assets.Scripts.Helpers;

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
    private Light[] _lights;

	[SerializeField]
	private GameObject _core;

	public Color SpotLightStartColor;
	private Color _coreStartColor;

	private Renderer _coreRenderer;

	private LerpHelper<Color> _turnOnLights;

	void Start() 
	{
		SpotLightStartColor = _spotLight.GetComponent<Light> ().color;
		_coreRenderer = _core.GetComponent<Renderer> ();
		_coreStartColor = _coreRenderer.material.color;
		_turnOnLights = new LerpHelper<Color> (Color.black, _pointLight.color, 2f, false);
		_pointLight.color = Color.black;

        foreach (var light in _lights)
        {
            light.color = Color.black;  
        }

    }

	void Update () 
	{
		UpdateTurnOnLights ();
		var newRot = Quaternion.LookRotation(LookAt.position - this.transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
	}

	private void UpdateTurnOnLights() 
	{
		if (_turnOnLights == null) return;

		bool done;
        var current = _turnOnLights.CurrentValue(out done);
        _pointLight.color = current;

        foreach (var light in _lights)
        {
            light.color = current;
        }

        if (done) _turnOnLights = null;
	}

	public void UpdateAlarmColor(float timePassed)
	{
        var color = Color.Lerp(SpotLightStartColor, _alarmColor, timePassed);
        _spotLight.color = color;
	    _pointLight.color = color;
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

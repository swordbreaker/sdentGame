using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlarmController : MonoBehaviour 
{

	[SerializeField]
	private SAIwAController _saiwa;

	[SerializeField]
	private Color _alarmColor = Color.red;
	private Color _startColor;

	private float _timePassed = 0;

	public bool Alarm { set; get; }

	private List<Renderer> _wallRenderers;

	private bool _up = true;

	void Start () 
	{
		_wallRenderers = GameObject.FindGameObjectsWithTag("ShipWall").Select (x => x.GetComponent<Renderer> ()).ToList();
		_startColor = _wallRenderers[0].material.GetColor("_EmissionColor");
	}
	
	void Update () 
	{
		if (Alarm) 
		{
			if (_up) _timePassed += Time.deltaTime;
			else _timePassed -= Time.deltaTime;

			_wallRenderers.ForEach (x => x.material.SetColor ("_EmissionColor", Color.Lerp (_startColor, _alarmColor, _timePassed)));
			_saiwa.UpdateAlarmColor (_timePassed);

			if (_timePassed <= 0 || 1 <= _timePassed) 
			{
				_up = !_up;
			}
		}
	}
}

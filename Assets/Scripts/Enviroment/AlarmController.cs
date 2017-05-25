using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour 
{

	[SerializeField]
	private Material material;

	[SerializeField]
	private Color _alarmColor = Color.red;
	private Color _startColor;
	private Color _fromColor;
	private Color _toColor;

	private float _timePassed = 0;

	public bool Alarm { set; get; }

	void Start () 
	{
		_startColor = material.color;
		_fromColor = _startColor;
		_toColor = _alarmColor;
	}
	
	void Update () 
	{
		if (Alarm) 
		{
			// #0967FF	
			/*_timePassed += Time.deltaTime;
			material.SetColor("_EmissionColor", Color.Lerp(_fromColor, _toColor, _timePassed));
			if (material.GetColor("_EmissionColor") == _toColor) 
			{
				_timePassed = 0;
				var temp = _toColor;
				_toColor = _fromColor;
				_fromColor = temp;
			}*/
			Debug.Log ("Alarm");
		}
	}
}

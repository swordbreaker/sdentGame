using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CrewMemberController : MonoBehaviour 
{

	[SerializeField]
	private Color _deathColor = Color.red;

	private Color? _color = null;

	private AudioSource _deathAudio;

	private FungusTriggerInteraction _deathTrigger;

	public void Start() 
	{
		_deathAudio = GetComponent<AudioSource> ();
		_deathTrigger = GetComponent<FungusTriggerInteraction> ();
		Assert.IsFalse (_deathTrigger.Interactable);
	}

	public void Update() 
	{
		if (_color.HasValue) 
		{
			foreach (var rend in GetComponentsInChildren<Renderer>()) 
			{
				rend.material.color = Color.Lerp (rend.material.color, _color.Value, Time.deltaTime);
			}
		}
	}

	public void Kill() 
	{
		_color = _deathColor;
		this._deathAudio.Play ();
		_deathTrigger.Interactable = true;
	}

}

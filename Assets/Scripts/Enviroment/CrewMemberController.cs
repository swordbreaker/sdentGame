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

	private FungusTriggerInteraction deathTrigger;

	public void Start() 
	{
		_deathAudio = GetComponent<AudioSource> ();
		deathTrigger = GetComponent<FungusTriggerInteraction> ();
		Assert.IsFalse (deathTrigger.Interactable);
	}

	public void Update() 
	{
		if (_color.HasValue) 
		{
			foreach (var rend in GetComponentsInChildren<Renderer>()) 
			{
                var color = Color.Lerp(rend.material.color, _color.Value, Time.deltaTime);
                rend.material.color = color;
                rend.material.SetColor("_EmissionColor", color);
			}
		}
	}

	public void Kill() 
	{
		_color = _deathColor;
		this._deathAudio.Play ();
		deathTrigger.Interactable = true;
	}

}

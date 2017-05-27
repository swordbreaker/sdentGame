using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMemberController : MonoBehaviour 
{

	[SerializeField]
	private Color _deathColor = Color.red;

	private Color? _color = null;

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
		var deathTrigger = this.gameObject.AddComponent<FungusTriggerInteraction>() as FungusTriggerInteraction;
		deathTrigger.SetName("Untersuchen");
		deathTrigger.Message = "Death_Start";
	}

}

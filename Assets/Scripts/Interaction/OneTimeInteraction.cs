using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OneTimeInteraction : IInteraction 
{

	private bool _alreadyInteracted;

	public override bool Interactable 
	{
		get 
		{
			return !_alreadyInteracted && base.Interactable;
		}
	}

	public override void Interact (GameObject interacter) 
	{
		_alreadyInteracted = true;
	}

}

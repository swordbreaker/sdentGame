using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour, IInteraction
{

	[SerializeField]
	private bool _interactable = true;

	public abstract string Name {
		get;
	}

	public virtual bool Interactable 
	{
		get 
		{
			return _interactable;
		}
		set 
		{
			_interactable = value;
		}
	}

	public abstract void Interact (GameObject interacter);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusTriggerInteraction : AbstractInteraction 
{

	[SerializeField]
	private string _name;

	[SerializeField]
	private string _message;

	[SerializeField]
	private bool once = false;

	private bool _interacted = false;

	public override bool Interactable
	{
		get 
		{
			return !(once && _interacted) && base.Interactable;
		}
		set 
		{
			base.Interactable = value;
		}
	}

	public string Message 
	{
		get 
		{
			return _message;
		}
		set
		{
			_message = value;
		}
	}

	public override string Name 
	{
		get 
		{
			return _name;
		}
	}

	public void SetName(string value) 
	{
		this._name = value;
	}

	public override void Interact (GameObject interacter)
	{
		_interacted = true;
		Fungus.Flowchart.BroadcastFungusMessage (_message);
	}


}

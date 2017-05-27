using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusTriggerInteraction : IInteraction 
{

	[SerializeField]
	private string _name;

	[SerializeField]
	private string _message;

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
		Fungus.Flowchart.BroadcastFungusMessage (_message);
	}


}

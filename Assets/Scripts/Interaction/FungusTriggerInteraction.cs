using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusTriggerInteraction : IInteraction 
{

	[SerializeField]
	private string _name;

	[SerializeField]
	private string _message;

	public override string Name 
	{
		get 
		{
			return _name;
		}
	}

	public override void Interact (GameObject interacter)
	{
		Fungus.Flowchart.BroadcastFungusMessage (_message);
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectInteraction : OneTimeInteraction 
{
	
	public override string Name 
	{
		get 
		{
			return null;
		}
	}

	public override void Interact (GameObject interacter)
	{
		base.Interact (interacter);
		GetComponent<Renderer>().enabled = false;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectInteraction : AbstractInteraction 
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
		this.gameObject.SetActive(false);
	}

}

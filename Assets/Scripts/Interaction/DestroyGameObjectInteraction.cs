using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectInteraction : OneTimeInteraction 
{

	[SerializeField]
	private Renderer _renderer;

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
		_renderer.enabled = false;
	}

}

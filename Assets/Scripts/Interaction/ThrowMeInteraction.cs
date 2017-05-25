using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMeInteraction : IInteraction {

	public override string Name 
	{
		get 
		{
			return "Throw me";
		}
	}

	public override void Interact (GameObject interacter) 
	{
		this.GetComponent<Rigidbody>().AddForce(transform.up * 10,ForceMode.Impulse);
	}

}

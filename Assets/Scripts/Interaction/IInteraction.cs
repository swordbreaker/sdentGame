using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteraction : MonoBehaviour {

	public abstract string Name 
	{
		get;
	}

	public abstract void Interact (GameObject interacter);

}

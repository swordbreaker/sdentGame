using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEventManagement : MonoBehaviour {

	[SerializeField]
	private SAIwAController _SAIwAController;

	[SerializeField]
	private ShutterTrigger _shutter;

	[SerializeField]
	private Transform LookOutEntree;

	public void LookAtLookOutEntree() 
	{
		_SAIwAController.LookAt = LookOutEntree;
	}

	public void CloseLookOutShutters() 
	{
		_shutter.CloseAllShutters();
	}

}

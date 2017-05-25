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

	[SerializeField]
	private Transform CaptainEntree;

	public void LookAtLookOutEntree() 
	{
		_SAIwAController.LookAt = LookOutEntree;
	}

	public void LookAtCaptainEntree() 
	{
		_SAIwAController.LookAt = CaptainEntree;
	}

	public void LookAtLookOutReset() 
	{
		_SAIwAController.LookAt = null;
	}

	public void CloseLookOutShutters() 
	{
		_shutter.CloseAllShutters();
	}

	public void AddLookOutExitTrigger()
	{
		var trigger = LookOutEntree.gameObject.AddComponent<FungusTrigger>() as FungusTrigger;
		trigger.Message = "Captain_Start";
	}

}

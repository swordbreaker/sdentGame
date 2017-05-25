using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Movement;

public class DialogEventManagement : MonoBehaviour {

	[SerializeField]
	private SAIwAController _SAIwAController;

	[SerializeField]
	private MoveController _player;

	[SerializeField]
	private ShutterTrigger _shutter;

	[SerializeField]
	private Transform LookOutEntree;

	[SerializeField]
	private Transform CaptainEntree;

	[SerializeField]
	private Transform CaptainExit;

	[SerializeField]
	private Transform _engineRoomEntree;

	[SerializeField]
	private GameObject _captainIdCard;

	[SerializeField]
	private Door _captainDoor;

	[SerializeField]
	private Door _engineRoomDoor;

	[SerializeField]
	private BrokenEngineRoom _brokenEngineRoom;

	[SerializeField]
	private AlarmController _alarm;

	public void LookAtLookOutEntree() 
	{
		_SAIwAController.LookAt = LookOutEntree;
	}

	public void LookAtCaptainEntree() 
	{
		_SAIwAController.LookAt = CaptainEntree;
	}

	public void LookAtReset() 
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

	public void OpenCaptainDoor() 
	{
		_captainDoor.IsOpen = true;
	}

	public void AddCaptainExitTrigger()
	{
		var trigger = CaptainExit.gameObject.AddComponent<FungusTrigger>() as FungusTrigger;
		trigger.Message = "Engineroom_Start";
	}

	public void MakeCaptainIDInteractable() 
	{
		foreach (var interaction in _captainIdCard.GetComponents<IInteraction>()) 
		{
			interaction.Interactable = true;
		}
	}

	public void LookAtEngineRoom() 
	{
		_SAIwAController.LookAt = _engineRoomEntree;
	}

	public void AddEngineRoomEntreeTrigger() 
	{
		var trigger = CaptainExit.gameObject.AddComponent<FungusTrigger>() as FungusTrigger;
		trigger.Message = "Engineroom_Entree";
	}

	public void OpenEngineRoomDoor() 
	{
		_engineRoomDoor.IsOpen = true;
	}

	public void RepairEngineFinished() 
	{
		this._brokenEngineRoom.RepairEngine ();
		this.AddEngineRoomExitTrigger ();
	}

	public void AddEngineRoomExitTrigger() 
	{
		var trigger = this._engineRoomEntree.gameObject.AddComponent<FungusTrigger>() as FungusTrigger;
		trigger.Message = "Engineroom_Exit";
	}

	public void DisablePlayerMovement() 
	{
		this._player.CanMove = false;
	}

	public void EnablePlayerMovement() 
	{
		this._player.CanMove = true;
	}

	public void StartAlarm() 
	{
		this._alarm.Alarm = true;
	}

}

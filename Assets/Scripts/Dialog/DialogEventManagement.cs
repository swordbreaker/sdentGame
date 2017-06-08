using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Movement;
using System.Linq;
using Fungus;

public class DialogEventManagement : MonoBehaviour {

	[SerializeField]
	private Flowchart _flowchart;

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
	private AlarmController _alarm;

	[SerializeField]
	private CrewMemberController[] _crewMembersToKill;
	private int _nextCrewMemberToKillIndex = 0;

	[SerializeField]
	private GameObject _playerBed;

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

	void CaptainStart() 
	{
		this.CaptainEntree.GetComponent<FungusTrigger> ().Active = true;
		this.OpenCaptainDoor ();
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
		foreach (var interaction in _captainIdCard.GetComponents<AbstractInteraction>()) 
		{
			interaction.Interactable = true;
		}
	}

	public void LookAtEngineRoom() 
	{
		_SAIwAController.LookAt = _engineRoomEntree;
	}

	public void StartEngineroom() 
	{
		this._engineRoomEntree.GetComponent<FungusTrigger> ().Active = true;
		this.OpenEngineRoomDoor ();
	}

	public void OpenEngineRoomDoor() 
	{
		_engineRoomDoor.IsOpen = true;
	}

	public void RepairEngineFinished() 
	{
		this.AddEngineRoomExitTrigger ();
		this.AddDeepSleepEnding ();
	}

	public void AddEngineRoomExitTrigger() 
	{
		var trigger = this._engineRoomEntree.gameObject.AddComponent<FungusTrigger>() as FungusTrigger;
		trigger.Message = "Engineroom_Exit";
	}

	public void DisablePlayerMovement() 
	{
		this._player.CanMove = false;
		this._player.CanJump = false;
	}

	public void EnablePlayerMovement() 
	{
		this._player.CanMove = true;
		this._player.CanJump = true;
	}

	public void StartAlarm() 
	{
		this._alarm.Alarm = true;
	}

	public void KillNextCrewMember() 
	{
		if (_nextCrewMemberToKillIndex == _crewMembersToKill.Count()) 
		{
			_flowchart.SetBooleanVariable ("CrewMembersKilled", true);
		} else 
		{
			_crewMembersToKill [_nextCrewMemberToKillIndex++].Kill ();
		}
	}

	public void AddDeepSleepEnding() 
	{
		var endingTrigger = this.gameObject.AddComponent<FungusTriggerInteraction>() as FungusTriggerInteraction;
		endingTrigger.SetName("Zurück zum Tiefschlaf");
		endingTrigger.Message = "Ending_DeepSleep";
	}

}

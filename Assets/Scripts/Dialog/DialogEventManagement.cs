using UnityEngine;
using Assets.Scripts.Movement;
using System.Linq;
using Assets.Scripts.Console;
using Assets.Scripts.Console.Attributes;
using Fungus;
using UnityEngine.UI;
using Assets.Scripts.Helpers;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class DialogEventManagement : MonoBehaviour {

    [SerializeField]
    private ScreenSettings _screenInfoSettings;

    [SerializeField]
    private ScreenSettings _screenWarnSettings;

    [SerializeField]
    private ScreenSettings _screenDangerSettings;

    [SerializeField] 
    private ScreensController _screens;

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

	[SerializeField]
	private Image _deepSleepEndingImage;

	private LerpHelper<Color> _deepSleepLerp;
	private LerpHelper<float> _globalSoundLerp;

    private void Update() 
	{
		if (_deepSleepLerp != null && _globalSoundLerp != null) 
		{	
			bool imageGoalReached, audioGoalReached;
			_deepSleepEndingImage.color = _deepSleepLerp.CurrentValue (out imageGoalReached);
			Debug.Log (AudioListener.volume);
			AudioListener.volume = _globalSoundLerp.CurrentValue (out audioGoalReached);
			if (imageGoalReached && audioGoalReached) 
			{
				SceneManager.LoadScene ("Credits");
			}
		}
	}

    private void Start()
    {
        Console.Instance.RegisterClass<DialogEventManagement>(this);
        _screens.ScreenSmallShipEntree.ChangeText("Kein Zutritt!", _screenDangerSettings);
    }

    [ConsoleCommand]
	public void AddEndingDeepSleepPossibility()
	{
		_playerBed.GetComponent<FungusTriggerInteraction> ().Interactable = true;
	}

    [ConsoleCommand]
    public void DeepSleepEndingPre() 
	{
		_playerBed.GetComponent<FungusTriggerInteraction> ().enabled = false;
	}

    [ConsoleCommand]
    public void RepairIntroductionEnded() 
    {
        _screens.ChangeTextOnInfoScreens("Gehen Sie zum Ausguck", _screenWarnSettings);
        _screens.ScreenLookOutEntree.ChangeText("Hier ist der Ausguck", _screenInfoSettings);
        LookAtLookOutEntree();
        AddEndingDeepSleepPossibility();
        EnablePlayerMovement();
    }

    [ConsoleCommand]
    public void LookAtLookOutEntree() 
	{
		_SAIwAController.LookAt = LookOutEntree;
	}

    [ConsoleCommand]
    public void LookAtCaptainEntree() 
	{
		_SAIwAController.LookAt = CaptainEntree;
	}

    [ConsoleCommand]
    public void LookAtReset() 
	{
		_SAIwAController.LookAt = null;
	}

    [ConsoleCommand]
    public void CloseLookOutShutters() 
	{
		_shutter.CloseAllShutters();
	}

    [ConsoleCommand]
    public void AddLookOutExitTrigger()
	{
		var trigger = LookOutEntree.gameObject.AddComponent<FungusTrigger>() as FungusTrigger;
		trigger.Message = "Captain_Start";
	}

    [ConsoleCommand]
    public void CaptainStart() 
	{
		CaptainEntree.GetComponent<FungusTrigger> ().Active = true;
        OpenCaptainDoor ();
        _screens.ChangeTextOnInfoScreens("Gehen Sie zum Kapitanszimmer", _screenWarnSettings);
        _screens.ScreenCaptainEntree.ChangeText("Hier ist das\n\rKapitanszimmer", _screenInfoSettings);
	}

    [ConsoleCommand]
    public void Captain_Entree_Ended() 
    {
        _screens.ChangeTextOnInfoScreens("Finden Sie die ID-Karte des Kapitan", _screenInfoSettings);
        _screens.ScreenCaptainEntree.ChangeText("Hier ist das\n\rKapitanszimmer", _screenInfoSettings);
    }

    [ConsoleCommand]
    public void OpenCaptainDoor() 
	{
		_captainDoor.IsOpen = true;
	}

    [ConsoleCommand]
    public void AddCaptainExitTrigger()
	{
		var trigger = CaptainExit.gameObject.AddComponent<FungusTrigger>() as FungusTrigger;
		trigger.Message = "Engineroom_Start";
	}

    [ConsoleCommand]
    public void MakeCaptainIDInteractable() 
	{
		foreach (var interaction in _captainIdCard.GetComponents<AbstractInteraction>()) 
		{
			interaction.Interactable = true;
		}
	}

    [ConsoleCommand]
    public void LookAtEngineRoom() 
	{
		_SAIwAController.LookAt = _engineRoomEntree;
	}

    [ConsoleCommand]
    public void StartEngineroom() 
	{
		_engineRoomEntree.GetComponent<FungusTrigger> ().Active = true;
		OpenEngineRoomDoor ();
        _screens.ChangeTextOnInfoScreens("Gehen Sie zum Motorraum", _screenWarnSettings);
	}

    public IEnumerator EndGameDisplayText() 
    {
        while (true) 
        {
            _screens.ChangeTextOnInfoScreens("Haben Sie keine Angst", _screenDangerSettings);
            yield return new WaitForSeconds(4.0F);
            _screens.ChangeTextOnInfoScreens("Sauerstoff ist knapp", _screenDangerSettings);
            yield return new WaitForSeconds(4.0F);
            _screens.ChangeTextOnInfoScreens("Gehen Sie zurück in den Tiefschlaf", _screenDangerSettings);
            yield return new WaitForSeconds(4.0F);
        }
    }

    [ConsoleCommand]
    public void Engineroom_Exit_Start()
    {
        StartCoroutine("EndGameDisplayText");
    }

    [ConsoleCommand]
    public void OpenEngineRoomDoor() 
	{
		_engineRoomDoor.IsOpen = true;
	}

    [ConsoleCommand]
    public void RepairEngineFinished() 
	{
		this.AddEngineRoomExitTrigger ();
		this.AddDeepSleepEnding ();
	}

    [ConsoleCommand]
    public void AddEngineRoomExitTrigger() 
	{
		var trigger = this._engineRoomEntree.gameObject.AddComponent<FungusTrigger>() as FungusTrigger;
		trigger.Message = "Engineroom_Exit";
	}

    [ConsoleCommand]
    public void DisablePlayerMovement() 
	{
		this._player.CanMove = false;
		this._player.CanJump = false;
	}

    [ConsoleCommand]
    public void EnablePlayerMovement() 
	{
		this._player.CanMove = true;
		this._player.CanJump = true;
	}

    [ConsoleCommand]
    public void StartAlarm() 
	{
		this._alarm.Alarm = true;
	}

    [ConsoleCommand]
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

    [ConsoleCommand]
    public void AddDeepSleepEnding() 
	{
		var endingTrigger = this.gameObject.AddComponent<FungusTriggerInteraction>() as FungusTriggerInteraction;
		endingTrigger.SetName("Zurück zum Tiefschlaf");
		endingTrigger.Message = "Ending_DeepSleep";
	}

    [ConsoleCommand]
    public void DeepSleepEnding() 
	{
		_deepSleepLerp = new LerpHelper<Color> (_deepSleepEndingImage.color, Color.black, 5, false);
		_globalSoundLerp = new LerpHelper<float> (AudioListener.volume, 0, 9, false);
	}

    private void OnDestroy()
    {
        Console.Instance.DeregisterClass<DialogEventManagement>(this);
    }
}

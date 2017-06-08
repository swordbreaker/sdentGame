using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairInteraction : OneTimeInteraction {

	private Animator _cylinderAnimator;
	private AudioSource _sound;

	[SerializeField]
	private AudioClip _repairSound;

	public void Start() 
	{
		_cylinderAnimator = GetComponent<Animator>();
		_sound = GetComponent<AudioSource> ();
		_cylinderAnimator.enabled = false;
		_sound.enabled = false;
	}

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
		Invoke ("Repair", _repairSound.length); 
	}

	private void Repair() 
	{
		_sound.enabled = true;
		_cylinderAnimator.enabled = true;
	}

}

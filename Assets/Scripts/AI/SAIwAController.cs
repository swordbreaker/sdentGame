using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAIwAController : MonoBehaviour
{
	public Transform Player;
	private Transform _lookAtTarget;

	void Update () 
	{
		var newRot = Quaternion.LookRotation(LookAt.position - this.transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
	}

	public Transform LookAt 
	{
		set
		{
			_lookAtTarget = value;
		}

		get 
		{
			return _lookAtTarget ?? Player; 
		}
	}

}

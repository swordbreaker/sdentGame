using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusTrigger : MonoBehaviour 
{

	[SerializeField]
	private string _message;

	[SerializeField]
	private bool _once = true;

	public void OnTriggerEnter(Collider other) 
	{
		if (other.tag != "Player") return;
		Fungus.Flowchart.BroadcastFungusMessage (_message);
		if (_once) 
		{
			Debug.Log (string.Format("Disable FungerTrigger {0} for {1}", _message, gameObject));
			Destroy (this);
		}
	}

}

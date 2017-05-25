using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractionController : MonoBehaviour {

	void Start () 
	{
		
	}
	
	void Update () 
	{
		Debug.DrawRay (transform.position, transform.forward, Color.yellow);
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, transform.forward, out hitInfo, 10, LayerMask.GetMask ("PlayerInteraction")))
		{
			IEnumerable<IInteraction> interactions = hitInfo.transform.gameObject.GetComponents<IInteraction> ().Where(x => x.Interactable);
			if (interactions.Any())
			{
				Debug.Log (interactions.First().Name);
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					foreach (var x in interactions)
						x.Interact (this.gameObject);
				}
			}
		}
	}
}

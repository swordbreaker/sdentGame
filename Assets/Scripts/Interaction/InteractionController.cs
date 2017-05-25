using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			var interaction = hitInfo.transform.gameObject.GetComponent<IInteraction> ();
			if (interaction != null) 
			{
				Debug.Log (interaction.Name);
				if (Input.GetKeyDown (KeyCode.E)) {
					interaction.Interact (this.gameObject);
				}
			}
		}
	}
}

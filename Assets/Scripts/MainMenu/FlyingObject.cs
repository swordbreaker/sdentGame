using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class FlyingObject: MonoBehaviour
{

    [SerializeField] private Transform _forcePos;

	// Use this for initialization
	private void Start () {
		//GetComponent<Rigidbody>().AddForce(Vector3.forward * 10);
        GetComponent<Rigidbody>().AddForceAtPosition(Vector3.forward * 10, _forcePos.position);
		GetComponent<Rigidbody>().AddTorque(new Vector3(0,50,0));
	    //transform.DORotate(new Vector3(180, 180, 180), 1f);
	}
	
}

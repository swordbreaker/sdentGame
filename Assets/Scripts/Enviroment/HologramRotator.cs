using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramRotator : MonoBehaviour
{

    [SerializeField] private Vector3 angularVelocity;

	public void Update ()
	{
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + angularVelocity*Time.deltaTime);
        transform.Rotate(angularVelocity * Time.deltaTime);

    }
}

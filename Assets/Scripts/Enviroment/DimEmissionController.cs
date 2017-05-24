using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DimEmissionController : MonoBehaviour {

	[SerializeField]
	private Color finalColor = Color.white;

	[SerializeField]
	private float transitionTime = 10;

	[SerializeField]
	private float waitFor = 3;

	private List<Material> childMaterials;

	[SerializeField]
	private Color startColor = Color.black;

	void Start () 
	{
		this.childMaterials = this.GetComponentsInChildren<MeshRenderer> ().Select(x => x.material).ToList();
	}

	void UpdateColor(Material material)
	{
		Color color = Color.Lerp (startColor, finalColor, (Time.time - waitFor) / transitionTime);
		material.SetColor ("_EmissionColor", color);
	}	

	void Update () 
	{
		this.childMaterials.ForEach (UpdateColor);
	}

}

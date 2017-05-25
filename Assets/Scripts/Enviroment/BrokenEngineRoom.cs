using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenEngineRoom : MonoBehaviour
{
    [SerializeField] private Animator[] _cylinderAnimator;
	[SerializeField] private GameObject[] RemoveWhenRepaired;

	private void Start ()
    {
        DisableAnimations();
	}

    public void RepairEngine()
    {
        EnableAnimations();
		foreach (var go in RemoveWhenRepaired)
			GameObject.Destroy (go);
    }

    private void DisableAnimations()
    {
        foreach (var animator in _cylinderAnimator)
        {
            animator.enabled = false;
        }
    }

    private void EnableAnimations()
    {
        foreach (var animator in _cylinderAnimator)
        {
            animator.enabled = true;
        }
    }
}

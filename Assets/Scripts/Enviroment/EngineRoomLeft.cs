using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineRoomLeft : MonoBehaviour
{
    [SerializeField] private Animator[] _cylinderAnimator;

	private void Start ()
    {
        DisableAnimations();
	}

    public void RepairEngine()
    {
        EnableAnimations();
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

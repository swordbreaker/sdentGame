using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenEngineRoom : MonoBehaviour
{
    [SerializeField] private Animator[] _cylinderAnimator;
	[SerializeField] private GameObject[] RemoveWhenRepaired;
    [SerializeField] private GameObject _spark;
    [SerializeField] private AudioSource _audioSource;

	private void Start ()
    {
        DisableAnimations();
        _audioSource.enabled = false;
    }

    public void RepairEngine()
    {
        _audioSource.enabled = true;
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

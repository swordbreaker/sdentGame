using System;
using System.Collections;
using System.Runtime.InteropServices;
using Assets.Scripts.Movement;
using DG.Tweening;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private float _startZPost;

    private void Start()
    {
        _startZPost = transform.localPosition.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var moveController = other.GetComponent<MoveController>();
            other.transform.SetParent(transform);
            other.transform.DOMove(transform.position + (moveController.transform.position - moveController.FootPosition), 1f);
            moveController.CanMove = false;

            StartCoroutine(Move(other));
        }
    }

    private IEnumerator Move(Collider other)
    {
        yield return new WaitForSeconds(1f);
        //Elevator is at the start position
        if (Math.Abs(transform.localPosition.z - _startZPost) < 0.01f)
        {
            yield return transform.DOLocalMoveZ(-12f, 5f).WaitForCompletion();
        }
        else
        {
            yield return transform.DOLocalMoveZ(_startZPost, 5f).WaitForCompletion();
        }
        other.transform.SetParent(null);
        other.GetComponent<MoveController>().CanMove = true;

    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shutter : MonoBehaviour
{

    public float MoveSpeed = 1f;
    public Ease MoveEase = Ease.Linear;

    private Transform _top;
    private Transform _bottom;

    private float _startYTop;
    private float _startYBottom;

	private void Start ()
	{
	    _bottom = transform.Find("Bottom");
	    _top = transform.Find("Top");

	    _startYBottom = _bottom.localPosition.y;
	    _startYTop = _top.localPosition.y;
	}

    public void Close()
    {
        _bottom.DOLocalMoveY(0.5f, MoveSpeed).SetEase(MoveEase);
        _top.DOLocalMoveY(2.1f, MoveSpeed).SetEase(MoveEase);
    }

    public void Open()
    {
        _bottom.DOLocalMoveY(_startYBottom, MoveSpeed).SetEase(MoveEase);
        _top.DOLocalMoveY(_startYTop, MoveSpeed).SetEase(MoveEase);
    }
}

using Assets.Scripts.Console;
using Assets.Scripts.Console.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPreviewBall : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private Transform _start;
    [SerializeField]
    private Transform _end;

    private bool _toStart = false;
    private Vector3 _force;

    public void Awake()
    {
        Console.Instance.RegisterClass<GravityPreviewBall>(this);
    }

    private void OnDestroy()
    {
        Console.Instance.DeregisterClass<GravityPreviewBall>(this);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        ToggleDirection();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_force, ForceMode.Acceleration);
        _rigidbody.AddTorque(new Vector3(0.2f, 0.2f, 0.2f), ForceMode.Acceleration);
    }

    [ConsoleCommand]
    public void ToggleDirection()
    {
        _toStart = !_toStart;

        if(_toStart)
        {
            _force = (_start.position - transform.position).normalized * 9.81f;
        }
        else
        {
            _force = (_end.position - transform.position).normalized * 9.81f;
        }
    }
}

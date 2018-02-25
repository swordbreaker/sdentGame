using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Helpers;

public class AlarmController : MonoBehaviour
{

    [SerializeField]
    private SAIwAController _saiwa;

    [SerializeField]
    private Color _alarmColor = Color.red;
    private Color _startColor;

    private float _timePassed = 0;

    private LerpHelper<float> _lerpHelper;

    private LerpHelper<Color> _lerpHelperToAlarm;
    private LerpHelper<Color> _lerpHelperToStart;

    public bool Alarm { set; get; }

    private List<Renderer> _wallRenderers;

    private bool _up = true;

    private void Start()
    {
        _wallRenderers = GameObject.FindGameObjectsWithTag("ShipWall")
            .Select(x => x.GetComponent<Renderer>())
            .Where(r => r != null)
            .ToList();
        _startColor = _wallRenderers[0].material.GetColor("_EmissionColor");

        _lerpHelper = new LerpHelper<float>(0, 1, 1f, false);
    }

    private void Update()
    {
        if (Alarm)
        {
            bool goalReached;
            var t = _lerpHelper.CurrentValue(out goalReached);
            var color = Color.Lerp(_startColor, _alarmColor, t);

            _wallRenderers.ForEach(x => x.material.SetColor("_EmissionColor", color));
            _saiwa.UpdateAlarmColor(t);

            if (goalReached) _lerpHelper = _lerpHelper.Reverse();
        }
    }
}

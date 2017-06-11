using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Helpers;
using UnityEngine;

public class HologramDangerBlink : MonoBehaviour
{

    private Material holograMaterial;

    [SerializeField] private int materialIndex;

    [SerializeField] private Color alarmAlbeldo;

    //[SerializeField] private Color alarmEmission;

    private Color defaultAlbeldo;

    private Color defaultEmission;

    private bool alarmActive;

    private float elapsed;

    [SerializeField] private float interval;

    private LerpHelper<Color> _lerpHelper;

    public void Start()
    {
        holograMaterial = gameObject.GetComponent<Renderer>().materials[materialIndex];
        defaultAlbeldo = holograMaterial.color;
        defaultEmission = holograMaterial.GetColor("_EmissionColor");

        _lerpHelper = new LerpHelper<Color>(defaultAlbeldo, alarmAlbeldo, interval, false);
    }

    public void Update ()
    {
        if (_lerpHelper != null)
        {
            var finished = false;
            var color = _lerpHelper.CurrentValue(out finished);
            holograMaterial.color = color;
            holograMaterial.SetColor("_EmissionColor", color);

            if (finished)
            {
                _lerpHelper = new LerpHelper<Color>(_lerpHelper.EndValue, _lerpHelper.StartValue, interval, false);
            }
            
        }

        //elapsed += Time.deltaTime;
        //if (elapsed >= interval)
        //{
        //    if (alarmActive)
        //    {
        //        holograMaterial.color = defaultAlbeldo;
        //        holograMaterial.SetColor("_EmissionColor", defaultEmission);
        //    }
        //    else
        //    {
        //        holograMaterial.color = alarmAlbeldo;
        //        holograMaterial.SetColor("_EmissionColor", alarmEmission);
        //    }
        //    alarmActive = !alarmActive;
        //    elapsed = 0;
        //}
    }
}

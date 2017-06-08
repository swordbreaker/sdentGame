using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HologramDangerBlink : MonoBehaviour
{

    [SerializeField] private Material holograMaterial;

    [SerializeField] private Color alarmAlbeldo;

    [SerializeField] private Color alarmEmission;

    private Color defaultAlbeldo;

    private Color defaultEmission;

    private bool alarmActive;

    private float elapsed;

    [SerializeField] private float interval;

    public void Start()
    {
        defaultAlbeldo = holograMaterial.color;
        defaultEmission = holograMaterial.GetColor("_EmissionColor");
    }

    public void Update ()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= interval)
        {
            if (alarmActive)
            {
                holograMaterial.color = defaultAlbeldo;
                holograMaterial.SetColor("_EmissionColor", defaultEmission);
            }
            else
            {
                holograMaterial.color = alarmAlbeldo;
                holograMaterial.SetColor("_EmissionColor", alarmEmission);
            }
            alarmActive = !alarmActive;
            elapsed = 0;
        }
    }
}

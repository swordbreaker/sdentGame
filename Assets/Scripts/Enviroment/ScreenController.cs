using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class ScreenController : MonoBehaviour 
{
    private const float _maxSpeed = 1;
    private const float _maxChange = 0.1F;

    private TMP_Text _text;

    [SerializeField] private bool _inAir = true;
    private Vector3 _startLocalPosition;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _startLocalPosition = transform.localPosition;
    }

    private void StyleText(ScreenSettings settings) 
    {
        _text.color = settings.TextColor;
        GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", settings.BackgroundColor);
        GetComponent<Renderer>().materials[0].SetColor("_Color", settings.BackgroundColor);
    }

    public void ChangeText(string text, ScreenSettings settings) 
    {
        StyleText(settings);
        _text.text = text.ToLower();
    }

    void Update() 
    {
        if (_inAir)
            transform.localPosition = new Vector3(_startLocalPosition.x + Mathf.Sin(Time.time * _maxSpeed) * _maxChange, transform.localPosition.y, transform.localPosition.z); 
    }

}

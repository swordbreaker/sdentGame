using UnityEngine;

[CreateAssetMenu(menuName = "Screen/Settings")]
public class ScreenSettings : ScriptableObject
{

    [SerializeField] private Color _textColor;
    public Color TextColor { get { return _textColor; } }

    [SerializeField] private Color _backgroundColor;
    public Color BackgroundColor { get { return _backgroundColor; } }

}

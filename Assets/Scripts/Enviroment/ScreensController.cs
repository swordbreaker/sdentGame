using System.Collections.Generic;
using UnityEngine;

public class ScreensController : MonoBehaviour {

    [SerializeField] private ScreenController _screenLookOutEntree;
    public ScreenController ScreenLookOutEntree { get { return _screenLookOutEntree;  } }

    [SerializeField] private ScreenController _screenConference;
    public ScreenController ScreenConference { get { return _screenConference; } }

    [SerializeField] private ScreenController _screenSmallShipEntree;
    public ScreenController ScreenSmallShipEntree { get { return _screenSmallShipEntree; } }

    [SerializeField] private ScreenController _screenCaptainEntree;
    public ScreenController ScreenCaptainEntree { get { return _screenCaptainEntree; } }

    public void ChangeTextOnInfoScreens(string text, ScreenSettings settings) 
    {
        foreach (var screen in InfoScreens)
            screen.ChangeText(text, settings);
    }

    private IEnumerable<ScreenController> InfoScreens {
        get 
        {
            yield return ScreenLookOutEntree;
            yield return ScreenConference;
            yield return ScreenCaptainEntree;
        }
    }

}

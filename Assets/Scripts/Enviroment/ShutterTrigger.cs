using UnityEngine;

public class ShutterTrigger : MonoBehaviour
{
    public Shutter[] Shutters;

    public void CloseAllShutters()
    {
        foreach (var shutter in Shutters)
        {
            shutter.Close();
        }
    }
}

using UnityEngine;

public class Vibration 
{
    public void IsVibration(bool isOn)
    {
        if (isOn == true)
        {
            Handheld.Vibrate();
        }
    }
}

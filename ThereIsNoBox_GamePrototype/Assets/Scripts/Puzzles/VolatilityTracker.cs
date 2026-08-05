using UnityEngine;

public class VolatilityTracker : MonoBehaviour
{
   
    [SerializeField] TrackerLight[] trackerLights;

    public void init()
    {
        foreach (TrackerLight trackerLight in trackerLights)
        {
            trackerLight.TurnOff();
        }
        trackerLights[0].TurnOn();
    }

    public void TrackerUpdate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            trackerLights[i].TurnOn();
        }
    }
    
}

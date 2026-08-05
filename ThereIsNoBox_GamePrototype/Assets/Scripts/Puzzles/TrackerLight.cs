using UnityEngine;
using UnityEngine.UI;

public class TrackerLight : MonoBehaviour
{
    [SerializeField] Image lightImg;
    
    public void TurnOn()
    {
        lightImg.gameObject.SetActive(true);  
    }
     
    public void TurnOff()
    {
      lightImg.gameObject.SetActive(false);     
    }
    
}

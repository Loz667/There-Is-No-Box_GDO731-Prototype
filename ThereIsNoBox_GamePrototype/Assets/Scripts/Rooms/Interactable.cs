using UnityEngine;

public class Interactable : MonoBehaviour, IRaycastable
{
    
    public void HandleRaycast()
    {
        Debug.Log("Clicked on object: " + this.gameObject.name);
    }
}

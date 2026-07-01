using UnityEngine;
using TMPro;

public class DebugGridObject : MonoBehaviour
{
    [SerializeField] TextMeshPro debugText;
    FacilityGridObject gridObject;

    public void SetGridObject(FacilityGridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        debugText.text = gridObject.ToString();
    }
}

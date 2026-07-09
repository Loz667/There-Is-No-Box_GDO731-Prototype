using TMPro;
using UnityEngine;

public class FacilityDebugGrid : MonoBehaviour
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

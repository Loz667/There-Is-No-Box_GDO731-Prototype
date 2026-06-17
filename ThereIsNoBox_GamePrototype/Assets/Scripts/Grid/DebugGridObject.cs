using UnityEngine;
using TMPro;

public class DebugGridObject : MonoBehaviour
{
    [SerializeField] TextMeshPro debugText;
    GridObject gridObject;

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        debugText.text = gridObject.ToString();
    }
}

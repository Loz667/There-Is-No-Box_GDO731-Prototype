using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] Unit unit;

    MeshRenderer myRenderer;

    void Awake()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;

        UpdateVisual();
    }

    private void OnSelectedUnitChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            myRenderer.enabled = true;
        }
        else
        {
            myRenderer.enabled = false;
        }
    }
}

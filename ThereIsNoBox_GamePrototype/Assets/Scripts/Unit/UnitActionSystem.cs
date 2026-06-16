using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    public Unit GetSelectedUnit()
    {
        return currentSelected;
    }

    [SerializeField] LayerMask unitLayer;
    [SerializeField] Unit currentSelected;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
            currentSelected.MoveTo(MouseWorldPosition.GetCurrentPosition());
        }
    }

    bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, unitLayer))
        {
            if (hit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    void SetSelectedUnit(Unit unit)
    {
        currentSelected = unit;

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
}

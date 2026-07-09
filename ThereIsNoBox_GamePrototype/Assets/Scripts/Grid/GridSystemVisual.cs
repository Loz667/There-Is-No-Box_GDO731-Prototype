using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    public enum GridVisualColour { White, Blue, DarkRed, LightRed, Green }

    [Serializable]
    public struct GridVisualForAction
    {
        public GridVisualColour gridVisualColour;
        public Material material;
    }

    [SerializeField] Transform gridSystemVisual;
    [SerializeField] List<GridVisualForAction> gridVisualForActionList;

    GridSystemVisualSingle[,] gridSystemVisualArray;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gridSystemVisualArray = new GridSystemVisualSingle[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()
            ];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform gridSystemVisualTransform = Instantiate(gridSystemVisual, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridSystemVisualArray[x, z] = gridSystemVisualTransform.GetComponent<GridSystemVisualSingle>();
            }
        }

        UnitActionSystem.Instance.OnSelectedActionChanged += OnSelectedActionChanged;
        LevelGrid.Instance.OnAnyUnitMovedGridPosition += OnAnyUnitMovedGridPosition;

        UpdateGridVisual();
    }

    public void UpdateGridVisual()
    {
        HideAll();

        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        GridVisualColour gridVisualColour;

        //switch (selectedAction)
        //{
        //    default:
        //    case MoveAction moveAction:
        //        gridVisualColour = GridVisualColour.White;
        //        break;
        //    case SpinAction spinAction:
        //        gridVisualColour = GridVisualColour.Blue;
        //        break;
        //    case ShootAction shootAction:
        //        gridVisualColour = GridVisualColour.DarkRed;

        //        ShowAvailableRange(selectedUnit.GetGridPosition(), shootAction.GetShootRange(), GridVisualColour.LightRed);
        //        break;
        //}

        //ShowOnlyAvailable(selectedAction.GetValidGridPositionList(), gridVisualColour);
    }

    public void HideAll()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridSystemVisualArray[x, z].Hide();
            }
        }
    }

    public void ShowOnlyAvailable(List<GridPosition> gridPositions, GridVisualColour gridVisualColour)
    {
        foreach (GridPosition position in gridPositions)
        {
            gridSystemVisualArray[position.x, position.z].Show(GetGridVisualForAction(gridVisualColour));
        }
    }

    void ShowAvailableRange(GridPosition gridPosition, int range, GridVisualColour gridVisualColour)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition rangeGridPosition = gridPosition + new GridPosition(x, z);

                if (!LevelGrid.Instance.IsValidGridPosition(rangeGridPosition)) continue;

                int rangeDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (rangeDistance > range) continue;

                gridPositionList.Add(rangeGridPosition);
            }
        }

        ShowOnlyAvailable(gridPositionList, gridVisualColour);
    }

    void OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    void OnAnyUnitMovedGridPosition(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    Material GetGridVisualForAction(GridVisualColour gridVisualColour)
    {
        foreach (GridVisualForAction gridVisual in gridVisualForActionList)
        {
            if (gridVisual.gridVisualColour == gridVisualColour)
            {
                return gridVisual.material;
            }
        }

        Debug.LogError("Not able to find corresponding GridVisualForAction for GridVisualColour " + gridVisualColour);
        return null;
    }
}

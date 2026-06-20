using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    [SerializeField] Transform gridSystemVisual;

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
    }

    void Update()
    {
        UpdateGridVisual();
    }

    public void UpdateGridVisual()
    {
        HideAll();

        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        ShowOnlyAvailable(selectedAction.GetValidGridPositionList());
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

    public void ShowOnlyAvailable(List<GridPosition> gridPositions)
    {
        foreach (GridPosition position in gridPositions)
        {
            gridSystemVisualArray[position.x, position.z].Show();
        }
    }
}

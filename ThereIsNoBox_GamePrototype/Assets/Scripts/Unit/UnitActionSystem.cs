using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;

    public Unit GetSelectedUnit()
    {
        return currentSelected;
    }

    public void GetNextActiveUnit()
    {
        int currentIndex = UnitManager.Instance.GetPlayerUnitsList().IndexOf(currentSelected);
        int nextIndex = (currentIndex + 1) % UnitManager.Instance.GetPlayerUnitsList().Count;

        currentSelected = UnitManager.Instance.GetPlayerUnitsList()[nextIndex];

        room = FacilityGrid.Instance.GetRoomAtGridPosition(currentSelected.GetGridPosition());
        DoCameraTransition(room);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;

        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }

    [SerializeField] LayerMask unitLayer;
    [SerializeField] Unit currentSelected;

    BaseAction selectedAction;
    bool isBusy;

    RoomManager room;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetSelectedUnit(currentSelected);
    }

    void Update()
    {
        if (isBusy) return;

        if (!TurnSystem.Instance.IsPlayerTurn()) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (TryHandleUnitSelection()) return;

        //HandleSelectedAction();
    }

    public void HandleSelectedAction()
    {
        if (!currentSelected.TryUsePointsToTakeAction(selectedAction)) return;

        SetBusy();

        selectedAction.TakeAction(ClearBusy);

        OnActionStarted?.Invoke(this, EventArgs.Empty);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorldPosition.GetCurrentPosition());

        //    if (!selectedAction.IsValidGridPositionForAction(mouseGridPosition)) return;            
        //}
    }

    void SetBusy()
    {
        isBusy = true;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    void ClearBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, unitLayer))
            {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    //Unit is already selected so do not select again
                    if (unit == currentSelected) return false;

                    //Clicked on enemy unit
                    if (unit.IsEnemy()) return false;

                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    }

    void SetSelectedUnit(Unit unit)
    {
        currentSelected = unit;

        SetSelectedAction(unit.GetBaseActions()[0]);

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    async void DoCameraTransition(RoomManager targetRoom)
    {
        await ScreenFader.Instance.FadeOut();
        targetRoom.SetActiveRoomCamera(true);
        await Task.Delay(150);
        await ScreenFader.Instance.FadeIn();
    }
}

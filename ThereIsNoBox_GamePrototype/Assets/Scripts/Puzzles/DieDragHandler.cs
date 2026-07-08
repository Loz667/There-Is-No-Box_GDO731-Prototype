using UnityEngine;
using UnityEngine.EventSystems;

public class DieDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 _initialPosition;
    private Transform _originalParent;
    private DicePoolSlotUI _source;

    //private Transform _newParent;
    //Original slot?
    private Canvas parentCanvas;
    private CanvasGroup canvasGroup;
    
    void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        _source = GetComponentInParent<DicePoolSlotUI>();
        //TODO Set interactable to false? 
    }
    
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.position;
        _originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        transform.SetParent(parentCanvas.transform, true); //Canvas object
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Return gameObject to initial position
        transform.position = _initialPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetParent(_originalParent, true);
        
        //Get DropTarget - could be taskViewSlot, disposalSlot, focusSlot?
        //TODO call the process event on the dropTarget

        IDropTarget dropTarget = eventData.pointerEnter?.GetComponent<IDropTarget>();
        if (dropTarget != null)
        {
            Die dropDie = _source.GetDie();
            Debug.Log("Die value: " + dropDie.RollResult);
            if (dropTarget.isDropAllowed(dropDie))
            {
                _source.RemoveDie();
                dropTarget.DropDie(dropDie);
            }
        }
        
        /*
         * Die = source.GetDie
         * source.RemoveDie
         * target.AddDie
         * 
         */
        
    }
}

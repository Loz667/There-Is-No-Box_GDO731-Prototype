using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DieObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private bool isInteractable = false;
    [SerializeField] private DiceEnums.RollType rollType;
    
    private Transform source;
    CanvasGroup canvasGroup;
   
    //public event EventHandler<int> OnDieDragComplete;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = isInteractable;
        canvasGroup.blocksRaycasts = isInteractable;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Beginning OnBeginDrag");
        //if (!isInteractable) return; 
        source = transform.parent;
        transform.SetParent(transform.root); //Canvas object
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        TaskSlotView targetSlot = eventData.pointerEnter?.GetComponent<TaskSlotView>();
        //DieController sourceSlot = source.GetComponent<DieController>();

        if (targetSlot != null)
        {
            Debug.Log("Found targetSlot");
            if (targetSlot.currentDie != null)
            {
                Debug.Log("Already got a dice in that slot");
                transform.SetParent(source);
            }
            else
            {
                
                Debug.Log("Trying to add to a slot that wants " + targetSlot.RequiredType);
                //DiceRollManager.Instance.RemoveDieFromPool(sourceSlot);
                if (targetSlot.RequiredType == rollType)
                {

                    //sourceSlot.currentDie = null;
                    transform.SetParent(targetSlot.transform);
                    targetSlot.currentDie = gameObject;
                    targetSlot.MatchResult();
                }
                else
                {
                    Debug.Log("Invalid type - can't drag here");
                    transform.SetParent(source);
                }
                //targetSlot.UpdateTask(Value);
                //OnDieDragComplete?.Invoke(this, Value);
                
                //Destroy(source.gameObject);
            }
        }
        else
        {
            Debug.Log("Don't have a targetSlot");
            GameObject dropItem = eventData.pointerEnter;
            Debug.Log("Trying to drop onto: " + dropItem?.name);
            transform.SetParent(source);
        }
        
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}

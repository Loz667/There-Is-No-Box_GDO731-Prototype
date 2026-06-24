using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DieObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform source;
    CanvasGroup canvasGroup;
    [field: SerializeField] public int Value { get; private set; }
    
    public event EventHandler<int> OnDieDragComplete;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        source = transform.parent;
        transform.SetParent(transform.root); //Canvas?
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        ;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        DieTarget targetSlot = eventData.pointerEnter?.GetComponent<DieTarget>();
        DieController sourceSlot = source.GetComponent<DieController>();

        if (targetSlot != null)
        {
            if (targetSlot.currentDie != null)
            {
                Debug.Log("Already got a dice in that slot");
                transform.SetParent(source);
            }
            else
            {
                DiceRollManager.Instance.RemoveDieFromPool(sourceSlot);
                
                sourceSlot.currentDie = null;
                transform.SetParent(targetSlot.transform);
                targetSlot.currentDie = gameObject;
                targetSlot.UpdateTask(Value);
                //OnDieDragComplete?.Invoke(this, Value);
                
                Destroy(source.gameObject);
            }
        }
        else
        {
            transform.SetParent(source);
        }
        
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}

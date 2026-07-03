using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Interactable : MonoBehaviour, IRaycastable, IPointerEnterHandler, IPointerExitHandler
{
    
    [SerializeField] private Material highlightMaterial;
    
    private Renderer objectRenderer;
    private Material[] originalMaterials;
    private Material[] highlightedMaterials;
    
    private bool isHighlighted = false;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterials = objectRenderer.sharedMaterials;
        
        List<Material> materials = new List<Material>(originalMaterials);
        materials.Add(highlightMaterial);
        highlightedMaterials = materials.ToArray();
        
    }
    
    public void HandleRaycast()
    {
        Debug.Log("Clicked on object: " + this.gameObject.name);
        Game.UI.TogglePuzzleView();
    }
    
    public void Highlight(bool enable)
    {
        if (enable == isHighlighted) return;
        isHighlighted = enable;

        objectRenderer.materials = (isHighlighted) ?  highlightedMaterials : originalMaterials;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Highlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Highlight(false);
    }
    
    
}

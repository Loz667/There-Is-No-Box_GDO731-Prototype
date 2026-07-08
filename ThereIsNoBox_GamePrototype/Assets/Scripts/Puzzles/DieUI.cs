using UnityEngine;
using UnityEngine.UI;

public class DieUI : MonoBehaviour
{
    public GameObject currentDie;
    
    public DieFace CurrentFace { get; private set; }
    
    
    [SerializeField] private Image dieFaceImage;

    public void SetFace(DieFace newFace)
    {
        CurrentFace = newFace;
        if(dieFaceImage != null && newFace != null) dieFaceImage.sprite = newFace.image;
    }
    
}
